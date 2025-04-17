import serial
import serial.tools.list_ports
import time
import configparser
import os
import numpy as np
import struct
import cv2
from queue import Queue
import copy
from scipy import ndimage
from scipy.ndimage import median_filter

# 포트 찾기 함수
def find_tof_sensor_port():
    ports = serial.tools.list_ports.comports()
    sorted_ports = sorted(ports, key=lambda port: int(port.device.replace('COM', '')) if port.device.startswith('COM') else float('inf'))

    tof_ports = []

    for port in sorted_ports:
        try:
            with serial.Serial(port.device, 115200, timeout=0.5) as ser:
                ser.write(bytearray(b'AT+ISP=0\r'))  # TOF 센서에 데이터 요청
                time.sleep(0.2)  # 응답 대기
                response = ser.readline().strip()  # 응답 읽기
                
                if response:  # 응답이 있는 경우
                    tof_ports.append(port.device)
        except (serial.SerialException, ValueError):
            continue  # 포트가 열리지 않거나 오류 발생 시 무시

    return tof_ports[0] if tof_ports else None  # 가장 작은 COM 포트 반환

# ini 파일
CONFIG_FILE = "config.ini"

# CONFIG 파일 생성
def update_config(tof_port):
    """config.ini 파일을 업데이트하여 TOF 센서의 COM 포트를 저장, 파일이 없으면 새로 생성"""
    config = configparser.ConfigParser()
    
    # 파일이 없으면 새로 생성
    # if not os.path.exists(CONFIG_FILE):
    #     print("config.ini 파일이 존재하지 않습니다. 새로 생성합니다.")
    
    # 기존 설정 파일 읽기 (파일이 있으면 유지)
    config.read(CONFIG_FILE)

    if "Ports" not in config:
        config["Ports"] = {}

    config["Ports"]["tof_port"] = tof_port

    with open(CONFIG_FILE, "w") as configfile:
        config.write(configfile)
    # print(f"TOF 센서 포트 ({tof_port})가 config.ini에 저장되었습니다.")

# 설정 파일 읽기 함수
def load_config():
    config = configparser.ConfigParser()
    if not os.path.exists(CONFIG_FILE):
        raise FileNotFoundError(f"Configuration file '{CONFIG_FILE}' not found!")
    config.read(CONFIG_FILE)
    return config

# ini 읽기 함수
def get_required_setting(config, section, key):
    """INI 파일에서 반드시 필요한 설정을 가져옵니다. 없으면 예외를 발생시킵니다."""
    if section not in config or key not in config[section]:
        raise KeyError(f"Missing required setting: [{section}] {key}")
    return config[section][key]

# tof 초기화 함수
def initialize_tof_sensor():
    """TOF 센서를 초기화하고 연결하는 함수"""
    while True:  # 오류 발생 시 계속 재시도
        try:
            # 설정 파일 로드
            config = load_config()
            # 필수 설정값 가져오기
            tof_port = get_required_setting(config, "Ports", "tof_port")
            ser_tof = serial.Serial(tof_port, 115200, timeout=0.1)
            # print(f"TOF 센서 연결 성공: {tof_port}")
            return ser_tof  # 성공하면 시리얼 객체 반환

        except Exception as e:
            # print("*** 포트가 바뀌었습니다. 포트를 새로 탐색합니다. ***")
            
            # TOF 센서 포트 찾기
            find_port = find_tof_sensor_port()

            if find_port:
                # print(f"연결된 TOF 센서 포트: {find_port}")
                update_config(find_port)  # config.ini 업데이트
                
                # 잠시 대기 후 재시도
                time.sleep(0.5)
            else:
                # print("TOF 센서를 찾을 수 없습니다. 다시 시도합니다...")
                exit()

# tof 센서 설정
ser_tof = initialize_tof_sensor()

len_buffer = 45
tof_unit = 9
mtx_tof = Queue(maxsize=len_buffer)

hex_isp = bytearray(b'AT+ISP=1\r')
ser_tof.write(hex_isp)
hex_unit = bytearray(b'AT+UNIT=9\r')
ser_tof.write(hex_unit)
data_scan = bytearray(b'AT+DISP=3\r')
ser_tof.write(data_scan)
time.sleep(0.5)

# 높이 계산
def calculate_height(sensor_value):
    if sensor_value < 680 or sensor_value > 960:
        return "Error: 상자를 측정할 수 없습니다."
    for i in range(len(reference_table) - 1):
        x1, y1 = reference_table[i]["tof"], reference_table[i]["height_cm"]
        x2, y2 = reference_table[i + 1]["tof"], reference_table[i + 1]["height_cm"]
        if x2 <= sensor_value <= x1:
            height = y1 + (y2 - y1) * (x1 - sensor_value) / (x1 - x2)
            return round(height, 1)
    return "Error: 상자 높이를 계산할 수 없습니다."

# 픽셀당 cm 보간 함수
def get_pixel_to_cm_ratio(sensor_value):
    for i in range(len(reference_table) - 1):
        x1, r1 = reference_table[i]["tof"], reference_table[i]["pixel_ratio"]
        x2, r2 = reference_table[i + 1]["tof"], reference_table[i + 1]["pixel_ratio"]
        if x2 <= sensor_value <= x1:
            ratio = r1 + (r2 - r1) * (x1 - sensor_value) / (x1 - x2)
            return ratio * 0.1  # cm로 변환
    return None

    
# 필터링 함수
def filter_tof_data(data, invalid_value=2295, floor_value=970):
    """TOF 데이터를 필터링하여 유효한 값만 반환."""
    return np.where(
        # 2295 (측정 불가) 값, 바닥 값보다 낮은거 제외
        (data != invalid_value) & ((data < floor_value)),
        data,
        0 # 제외된 값은 0으로 설정
    )

# 상자 픽셀 계산 함수
def calculate_object_size(data):
    """
    필터링된 데이터에서 상자의 가로/세로 크기 계산.
    """
    object_indices = np.argwhere(data > 0)  # 유효한 값의 좌표 추출
    if object_indices.size > 0:
        x_min, y_min = np.min(object_indices, axis=0)
        x_max, y_max = np.max(object_indices, axis=0)
        width = x_max - x_min + 1  # 가로 크기
        length = y_max - y_min + 1  # 세로 크기
        return width, length
    else:
        return 0, 0  # 탐지된 물체가 없는 경우

    
# 노이즈 제거 함수
def remove_noise_centered(data, min_size=20):
    labeled_array, num_features = ndimage.label(data > 0)
    sizes = ndimage.sum(data > 0, labeled_array, range(num_features + 1))
    center = np.array(data.shape) // 2
    center_label = labeled_array[center[0], center[1]]

    keep_labels = {center_label} | set(np.where(sizes >= min_size)[0])
    return np.where(np.isin(labeled_array, list(keep_labels)), data, 0)

def keep_largest_centered_object(data):
    labeled_array, num_features = ndimage.label(data > 0)
    sizes = ndimage.sum(data > 0, labeled_array, range(num_features + 1))
    center = np.array(data.shape) // 2
    center_label = labeled_array[center[0], center[1]]

    if center_label == 0:
        return np.zeros_like(data)  # 중심이 아무 객체도 아니면 그냥 무시

    return np.where(labeled_array == center_label, data, 0)



def calculate_box_angle_and_size(data):
    """
    OpenCV를 이용하여 박스의 기울어진 각도를 계산하고, 크기를 측정하는 함수.
    """
    object_indices = np.argwhere(data > 0)  # 유효한 픽셀 위치 찾기
    if object_indices.size == 0:
        return 0, 0, 0  # 박스가 없는 경우

    # Convex Hull을 이용하여 박스의 경계를 찾음
    hull = cv2.convexHull(object_indices)

    # 최소 외접 사각형을 찾음
    rect = cv2.minAreaRect(hull)
    width, height = rect[1]  # 사각형의 너비와 높이
    angle = rect[-1]  # 회전 각도

    # OpenCV는 -90 ~ 0 범위의 각도를 반환하므로 보정
    if angle < -45:
        angle += 90  

    return round(angle, 1), max(width, height), min(width, height)

def scale_corrected_dimensions(min_area_width, min_area_length, pixel_to_cm_ratio):
    """
    픽셀 단위의 박스 크기를 실제 cm 단위로 변환.
    """
    true_width = min_area_width * pixel_to_cm_ratio
    true_length = min_area_length * pixel_to_cm_ratio

    return round(true_width, 1), round(true_length, 1)


    
if ser_tof:
    f_step = 0
    temp = Queue(maxsize=2)
    while mtx_tof.qsize() < 45:
        if f_step == 0:
            if temp.qsize() >= 2:
                temp.get()
            temp.put(ser_tof.read(1))
        if f_step == 0 and len(temp.queue) == 2 and temp.queue[0] == b'\x00' and temp.queue[1] == b'\xFF':
            f_step += 1
            data = ser_tof.read(10020)
            s_data = struct.unpack('6H2c2H10000c1c1c', data)  # mem unit is 1byte
            # print(s_data)
            # check
            length = int.from_bytes(s_data[6], byteorder='little')
            width = int.from_bytes(s_data[7], byteorder='little')
            if s_data[-1] == b'\xDD':
                packet = np.array([int.from_bytes(x, byteorder='little') for x in s_data[10:-2]], dtype=np.int32) * tof_unit
                if mtx_tof.full():
                    # print('full')
                    mtx_tof.get()
                mtx_tof.put(packet.reshape((length, width)))
            f_step -= 1

    # print(mtx_tof.qsize())
    output = copy.deepcopy(mtx_tof.queue)
    stack = np.stack(output)
    
    if not output:
        # print("Error: No data available in the queue.")
        exit()


# 데이터 필터링
# 중앙값 + 평균 혼합
mean = np.median(stack, axis=0) * 0.4 + np.mean(stack, axis=0) * 0.6
filtered_mean = filter_tof_data(mean)

# 튀는 점 제거: 평활화 (median filter)
from scipy.ndimage import median_filter
mean = median_filter(mean, size=3)

# 비율 기반으로 제거 영역 설정
# top_margin = int(100 * 0.1) # 상단 10%
bottom_margin = int(100 * 0.21)  # 하단 27%
left_margin = int(100 * 0.13)   # 좌 15% 
#right_margin = int(100 * 0.1) # 우 10%

# 상단 영역 제거
#filtered_mean[:top_margin, :] = 0
# 하단 영역 제거
filtered_mean[-bottom_margin:, :] = 0
# 좌측 영역 제거
filtered_mean[:, :left_margin] = 0
# 우측 영역 제거
#filtered_mean[:, -right_margin:] = 0

# 노이즈 제거
# Step 1: 필터링된 데이터 기준 중앙 근처 객체만 남기고
centered_filtered = remove_noise_centered(filtered_mean, min_size=10)

# Step 2: 그중 가장 큰 덩어리만 남긴다
cleaned_data = keep_largest_centered_object(centered_filtered)

object_width, object_length = calculate_object_size(cleaned_data)

# 결과 출력
# print(f"가로 픽셀: {object_width}")
# print(f"세로 픽셀: {object_length}")

# 현재 TOF 센서 평균 값
average_tof = np.mean(cleaned_data[cleaned_data > 0])
# print(f"tof 센서 평균 값 : {average_tof}")

# TOF 기준점 + 높이 + mm/픽셀 비율을 하나로 통합
reference_table = [
    {"tof": 955, "height_cm": 5, "pixel_ratio": 200 / 16},
    {"tof": 910, "height_cm": 10, "pixel_ratio": 200 / 21},
    {"tof": 870, "height_cm": 15, "pixel_ratio": 200 / 22},
    {"tof": 835, "height_cm": 20, "pixel_ratio": 200 / 25},
    {"tof": 800, "height_cm": 25, "pixel_ratio": 200 / 26},
    {"tof": 750, "height_cm": 30, "pixel_ratio": 200 / 28},
    {"tof": 710, "height_cm": 35, "pixel_ratio": 200 / 30},
    {"tof": 690, "height_cm": 40, "pixel_ratio": 200 / 33}
]


pixel_to_cm_ratio = get_pixel_to_cm_ratio(average_tof)

# 박스의 기울기 및 크기 계산
box_angle, min_area_width, min_area_length = calculate_box_angle_and_size(cleaned_data)

corrected_width, corrected_length = scale_corrected_dimensions(min_area_width, min_area_length, pixel_to_cm_ratio)

# 소숫점 1자리까지
width = round(corrected_width, 1)
length = round(corrected_length, 1)
height = round(calculate_height(average_tof), 1)
angle = round(box_angle, 1)


# 부피 계산
# volume = round(height * width * length)
# print(f"가로 : {width}cm, 세로 : {length}cm, 높이 : {height}cm, 각도 : {angle}")

# import matplotlib.pyplot as plt
# plt.imshow(cleaned_data, cmap='hot', interpolation='nearest')
# plt.colorbar()
# plt.show()    

import json
 
result = {
    "width_cm": width,
    "length_cm": length,
    "height_cm": height
}
print(json.dumps(result))  # JSON으로 출력

if ser_tof:
    
    close_isp = bytearray(b'AT+ISP=0\r')
    ser_tof.write(close_isp)
    close_disp = bytearray(b'AT+DISP=0\r')
    ser_tof.write(close_disp)
    
    ser_tof.close()
    #print("*** closed ***")
    
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
                time.sleep(1)
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
time.sleep(0.01)

# 높이 계산
def calculate_height(sensor_value):
    # 범위 확인
    if sensor_value < 900 or sensor_value >= 1430:
        return "Error: 상자를 측정할 수 없습니다."

    # 기준점 설정 (1150부터 650까지 10cm부터 60cm 사이 선형 변화)
    max_value = 1400
    min_value = 1000
    max_height = 5
    min_height = 60

    # 선형 보간 계산
    if sensor_value:
        height = max_height + (min_height - max_height) * (max_value - sensor_value) / (max_value - min_value)
        return round(height, 1)
    else:
        return "Error: 상자 높이를 계산할 수 없습니다."

    
# 필터링 함수
def filter_tof_data(data, invalid_value=2295, floor_value=1410):
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
def remove_noise(data, min_size=20):
    """
    작은 노이즈를 제거하여 상자만 남기는 함수.
    data: 입력 데이터 배열
    min_size: 노이즈로 간주할 픽셀의 최소 크기
    """
    # 8-방향 연결로 레이블링
    labeled_array, num_features = ndimage.label(data > 0)
    
    # 각 레이블의 픽셀 크기 계산
    sizes = np.bincount(labeled_array.flatten())
    
    # 작은 레이블(노이즈)에 해당하는 마스크
    noise_mask = sizes < min_size
    noise_mask[0] = False  # 배경(레이블 0)은 제외
    
    # 노이즈 제거
    noise_labels = np.where(noise_mask)[0]
    clean_data = np.where(np.isin(labeled_array, noise_labels), 0, data)
    
    return clean_data

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
    
    
    if not output:
        print("Error: No data available in the queue.")
        exit()


# 데이터 필터링
mean = np.stack(output).mean(axis=0)
filtered_mean = filter_tof_data(mean)

# 위치를 기준으로 영역 제거
#  +-------------------------+
#  |#########################|
#  |###|                 |###|
#  |###|                 |###|
#  |###|                 |###|
#  |###|                 |###|
#  |###|                 |###|
#  |###|                 |###| 
#  |#########################|
#  |#########################|
#  +-------------------------+
# 비율 기반으로 제거 영역 설정
top_margin = int(100 * 0.1) # 상단 10%
bottom_margin = int(100 * 0.3)  # 하단 30%
left_margin = int(100 * 0.26)   # 좌 26% 
right_margin = int(100 * 0.1) # 우 10%

# 상단 영역 제거
filtered_mean[:top_margin, :] = 0
# 하단 영역 제거
filtered_mean[-bottom_margin:, :] = 0
# 좌측 영역 제거
filtered_mean[:, :left_margin] = 0
# 우측 영역 제거
filtered_mean[:, -right_margin:] = 0

# 노이즈 제거
cleaned_data = remove_noise(filtered_mean)
object_width, object_length = calculate_object_size(cleaned_data)

# 결과 출력
# print(f"가로 픽셀: {object_width}")
# print(f"세로 픽셀: {object_length}")

# 현재 TOF 센서 평균 값
average_tof = np.mean(cleaned_data[cleaned_data > 0])
# print(f"tof 센서 평균 값 : {average_tof}")

# 기준 TOF 값과 대응하는 픽셀당 mm 비율
# 실제 높이cm :   10,   12,   18,   23,   28,   31,   35,   41,   47,   52,  57,  62,  65
# tof_reference = [1355, 1333, 1300, 1265, 1225, 1190, 1180, 1135, 1080, 1045, 995, 950, 930]

# 실제 높이cm :   5,    10,   15,   20,   25,   30,   35,   40,   45,   50,   55,   60
tof_reference = [1400, 1365, 1330, 1300, 1266, 1230, 1190, 1150, 1100, 1080, 1020, 990]


# mm / px
pixel_ratios = [200 / 18, # 1400
                200 / 20, # 1365
                200 / 22, # 1330
                200 / 24, # 1300
                200 / 26, # 1266
                200 / 28, # 1230
                200 / 30, # 1190
                200 / 34, # 1150
                200 / 37, # 1100
                200 / 40, # 1080
                200 / 47, # 1020
                200 / 55] # 990

# 가장 근접한 TOF 값 찾기   
closest_index = np.argmin([abs(average_tof - ref) for ref in tof_reference])

# 픽셀당 mm 비율 설정
pixel_to_cm_ratio = pixel_ratios[closest_index] * 0.1

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
    
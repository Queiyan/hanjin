# 부피 기반 가격 계산 모듈 사용 가이드

## 개요
이 모듈은 박스의 부피(가로 + 세로 + 높이)를 기반으로 운송비를 계산하는 기능을 제공합니다.

## 설치 방법
1. `Pricing` 폴더의 모든 파일을 프로젝트에 추가합니다.

## 사용 방법

### 1. 기본 사용법
```csharp
// PricingService 인스턴스 생성
var pricingService = new PricingService();

// 박스 크기 입력 (단위: cm)
double height = 30.0;
double width = 40.0;
double depth = 50.0;

// 가격 계산
int cost = pricingService.CalculateCost(height, width, depth);

// 박스 타입 확인
string boxType = pricingService.GetBoxType(height + width + depth);

// 크기 유효성 검사
bool isValid = pricingService.ValidateSize(height + width + depth);
```

### 2. 결과 객체 사용
```csharp
var pricingService = new PricingService();
double totalSize = height + width + depth;

var result = new PricingResult(
    totalSize: totalSize,
    cost: pricingService.CalculateCost(height, width, depth),
    boxType: pricingService.GetBoxType(totalSize),
    isValid: pricingService.ValidateSize(totalSize),
    errorMessage: !pricingService.ValidateSize(totalSize) ? "크기가 초과되었습니다." : null
);

if (result.IsValid)
{
    Console.WriteLine($"박스 타입: {result.BoxType}");
    Console.WriteLine($"가격: {result.Cost:N0}원");
}
else
{
    Console.WriteLine($"오류: {result.ErrorMessage}");
}
```

## 주요 기능

### 1. 가격 계산
- B타입 (100cm 이하): 3,000원
- C타입 (120cm 이하): 3,500원
- D타입 (140cm 이하): 4,500원
- E타입 (160cm 이하): 5,500원

### 2. 크기 검증
- 최대 허용 크기: 160cm (가로 + 세로 + 높이)
- 크기 초과 시 유효하지 않음으로 처리

### 3. 박스 타입 분류
- B, C, D, E 타입으로 자동 분류
- 크기 초과 시 X 타입으로 분류

## 주의사항
1. 모든 크기는 센티미터(cm) 단위로 입력해야 합니다.
2. 크기는 가로 + 세로 + 높이의 합으로 계산됩니다.
3. 160cm를 초과하는 크기는 유효하지 않습니다.

## 라이선스
이 모듈은 MIT 라이선스 하에 배포됩니다. 
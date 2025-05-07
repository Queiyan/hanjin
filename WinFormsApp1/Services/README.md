# 우편번호 검색 모듈 사용 가이드

## 개요
이 모듈은 행정안전부 우편번호 검색 API를 사용하여 주소를 검색하고 선택할 수 있는 기능을 제공합니다.

## 설치 방법
1. `Services` 폴더의 모든 파일을 프로젝트에 추가합니다.
2. Newtonsoft.Json NuGet 패키지를 설치합니다:
   ```
   Install-Package Newtonsoft.Json
   ```

## 사용 방법

### 1. 기본 사용법
```csharp
// API 키 설정 (실제 사용 시 환경 변수나 설정 파일에서 가져오는 것이 좋습니다)
string apiKey = "U01TX0FVVEgyMDI1MDIwNzE1Mzc1NTExNTQ1NTA=";

// PostcodeService 인스턴스 생성
var postcodeService = new PostcodeService(apiKey);

// PostcodeSearchForm 생성 및 표시
var form = new PostcodeSearchForm(postcodeService, (zipCode, address) =>
{
    // 주소가 선택되었을 때의 처리
    MessageBox.Show($"우편번호: {zipCode}\n주소: {address}", "주소 선택 완료");
});

form.ShowDialog();
```

### 2. 예제 클래스 사용
```csharp
// PostcodeSearchExample 클래스 사용
var postcodeSearch = new PostcodeSearchExample();
postcodeSearch.ShowPostcodeSearchForm();
```

### 3. 직접 API 호출
```csharp
// API 직접 호출
var (zipCode, address) = await postcodeService.SearchAddressAsync("검색어");

// 검색어 유효성 검사
if (postcodeService.ValidateSearchQuery("검색어", out string sanitizedQuery))
{
    // 유효한 검색어 처리
}
```

## 주요 기능

### 1. 주소 검색
- 도로명 주소 검색
- 우편번호 자동 매핑
- 검색 결과 자동 포맷팅

### 2. 입력 검증
- 특수문자 필터링
- SQL 인젝션 방지
- 유효한 검색어 검증

### 3. 에러 처리
- API 오류 메시지 표시
- 네트워크 오류 처리
- 사용자 친화적인 오류 메시지

## 주의사항
1. API 키는 반드시 안전하게 관리해야 합니다.
2. 검색 결과는 최대 10개까지만 표시됩니다.
3. API 호출 제한이 있을 수 있으므로 적절한 캐싱을 고려하세요.

## 라이선스
이 모듈은 MIT 라이선스 하에 배포됩니다. 
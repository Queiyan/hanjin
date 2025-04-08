using System.Text.Json.Serialization;

/// <summary>
/// 예약정보 조회 API 응답 모델 클래스
/// </summary>
public class ReservationResponse
{
    /// <summary>
    /// 결과 코드 (ex: "OK")
    /// </summary>
    [JsonPropertyName("resultCode")]
    public string ResultCode { get; set; }

    /// <summary>
    /// 결과 메시지 (ex: "SUCCESS")
    /// </summary>
    [JsonPropertyName("resultMessage")]
    public string ResultMessage { get; set; }

    /// <summary>
    /// 예약번호
    /// </summary>
    [JsonPropertyName("rsvNo")]
    public string RsvNo { get; set; }

    /// <summary>
    /// 예약작업코드 (ex: "RSV_CNCL" - 예약취소 등)
    /// </summary>
    [JsonPropertyName("rsvWrkCd")]
    public string RsvWrkCd { get; set; }

    /// <summary>
    /// 예약작업코드명 (ex: "예약취소")
    /// </summary>
    [JsonPropertyName("rsvWrkCdNm")]
    public string RsvWrkCdNm { get; set; }

    /// <summary>
    /// 예약접수일자 (ex: "20221107")
    /// </summary>
    [JsonPropertyName("rsvAceptanceDt")]
    public string RsvAceptanceDt { get; set; }

    /// <summary>
    /// 운송장번호
    /// </summary>
    [JsonPropertyName("wblNo")]
    public string WblNo { get; set; }

    /// <summary>
    /// 반품 운송장번호
    /// </summary>
    [JsonPropertyName("returnWblNo")]
    public string ReturnWblNo { get; set; }

    /// <summary>
    /// 송하인 계약번호
    /// </summary>
    [JsonPropertyName("sndrCntractNo")]
    public string SndrCntractNo { get; set; }

    /// <summary>
    /// 송하인명
    /// </summary>
    [JsonPropertyName("sndrNm")]
    public string SndrNm { get; set; }

    /// <summary>
    /// 송하인 전화번호
    /// </summary>
    [JsonPropertyName("sndrTelNo")]
    public string SndrTelNo { get; set; }

    /// <summary>
    /// 송하인 휴대전화번호
    /// </summary>
    [JsonPropertyName("sndrMobileNo")]
    public string SndrMobileNo { get; set; }

    /// <summary>
    /// 송하인 우편번호
    /// </summary>
    [JsonPropertyName("sndrZip")]
    public string SndrZip { get; set; }

    /// <summary>
    /// 송하인 기본주소
    /// </summary>
    [JsonPropertyName("sndrBaseAddr")]
    public string SndrBaseAddr { get; set; }

    /// <summary>
    /// 송하인 상세주소
    /// </summary>
    [JsonPropertyName("sndrDtlAddr")]
    public string SndrDtlAddr { get; set; }

    /// <summary>
    /// 수하인 계약번호
    /// </summary>
    [JsonPropertyName("rcvrCntractNo")]
    public string RcvrCntractNo { get; set; }

    /// <summary>
    /// 수하인명
    /// </summary>
    [JsonPropertyName("rcvrNm")]
    public string RcvrNm { get; set; }

    /// <summary>
    /// 수하인 우편번호
    /// </summary>
    [JsonPropertyName("rcvrZip")]
    public string RcvrZip { get; set; }

    /// <summary>
    /// 수하인 전화번호
    /// </summary>
    [JsonPropertyName("rcvrTelNo")]
    public string RcvrTelNo { get; set; }

    /// <summary>
    /// 수하인 휴대전화번호
    /// </summary>
    [JsonPropertyName("rcvrMobileNo")]
    public string RcvrMobileNo { get; set; }

    /// <summary>
    /// 수하인 기본주소
    /// </summary>
    [JsonPropertyName("rcvrBaseAddr")]
    public string RcvrBaseAddr { get; set; }

    /// <summary>
    /// 수하인 상세주소
    /// </summary>
    [JsonPropertyName("rcvrDtlAddr")]
    public string RcvrDtlAddr { get; set; }

    /// <summary>
    /// 물품분류 250401 추가
    /// </summary>
    [JsonPropertyName("comodityNm")]
    public string ComodityNm { get; set; }
    

    /// <summary>
    /// 모든 필드를 기본값(주로 빈 문자열)으로 초기화합니다.
    /// </summary>
    public void ClearData()
    {
        ResultCode = string.Empty;
        ResultMessage = string.Empty;
        RsvNo = string.Empty;
        RsvWrkCd = string.Empty;
        RsvWrkCdNm = string.Empty;
        RsvAceptanceDt = string.Empty;
        WblNo = string.Empty;
        ReturnWblNo = string.Empty;
        SndrCntractNo = string.Empty;
        SndrNm = string.Empty;
        SndrTelNo = string.Empty;
        SndrMobileNo = string.Empty;
        SndrZip = string.Empty;
        SndrBaseAddr = string.Empty;
        SndrDtlAddr = string.Empty;
        RcvrCntractNo = string.Empty;
        RcvrNm = string.Empty;
        RcvrZip = string.Empty;
        RcvrTelNo = string.Empty;
        RcvrMobileNo = string.Empty;
        RcvrBaseAddr = string.Empty;
        RcvrDtlAddr = string.Empty;
        ComodityNm = string.Empty;
    }
}

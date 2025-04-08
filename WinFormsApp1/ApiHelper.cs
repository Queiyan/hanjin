using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

public class ApiHelper
{
    private static readonly string clientId = "SMARTCU";
    private static readonly string secretKey = "G5SMSOPqKKcqBCumRHydAIQd13zN7GavkWqA8AcS9ATtB2ZgGGGoyhEmoRzQSzcp";
    private static readonly string apiKey = "EXAoyZ4VhU3FcFrIWVZWZjlHdpBfnlwkHPOkfWEzjMZ1iYwQ";

    public static async Task<string> SendRequestAsync(string reservNum)
    {
        string timestamp = CreateTimestamp();
        string encodedReservNum = Uri.EscapeDataString(reservNum);
        string apiUrl = $"https://api-dev.hanjin.com/parcel-delivery/v1/rsv/retrieve-rsvno/{encodedReservNum}";
        //string apiUrl = $"https://api-stg.hanjin.com/parcel-delivery/v1/rsv/retrieve-rsvno/{encodedReservNum}";

        // ✅ Postman과 동일한 message 생성
        string method = "GET";
        string queryString = ""; // GET 요청이므로 Query Parameter 없음
        string message = $"{timestamp}{method}{queryString}{secretKey}";

        // ✅ HMAC-SHA256 서명 생성 (HEX 변환 적용)
        string signature = ComputeHmacSHA256(message, secretKey);

        // ✅ Authorization 헤더 포맷 (공백 유지, 쉼표 제거)
        string authorization = $"client_id={clientId} timestamp={timestamp} signature={signature}";

        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorization);
            client.DefaultRequestHeaders.Add("x-api-key", apiKey);
            // Content-Type 헤더가 GET 요청에서는 일반적으로 필요 없으므로 생략해도 됩니다.

            HttpResponseMessage response = await client.GetAsync(apiUrl);
            string responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"API 요청 실패: {response.StatusCode}, 내용: {responseContent}");
            }

            return responseContent;
        }
    }

    private static string CreateTimestamp()
    {
        return DateTime.Now.ToString("yyyyMMddHHmmss");
    }

    private static string ComputeHmacSHA256(string message, string key)
    {
        using (HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
        {
            byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));

            // ✅ Postman과 동일한 HEX 문자열 변환
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }
}

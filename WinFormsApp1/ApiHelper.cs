using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

public class ApiHelper
{
    private static readonly string clientId = "SMARTCU";
    private static readonly string secretKey = "G5SMSOPqKKcqBCumRHydAIQd13zN7GavkWqA8AcS9ATtB2ZgGGGoyhEmoRzQSzcp";
    private static readonly string apiKey = "EXAoyZ4VhU3FcFrIWVZWZjlHdpBfnlwkHPOkfWEzjMZ1iYwQ"; // x-api-key 값

    public static async Task<string> SendRequestAsync(string url, HttpMethod method)
    {
        string timestamp = CreateTimestamp();
        string queryString = GetQueryString(url);
        string message = timestamp + method.Method + queryString + secretKey;
        string signature = ComputeHmacSHA256(message, secretKey);

        string authorization = $"client_id{clientId}&timestamp={timestamp}&signature={signature}";

        using (HttpClient client = new HttpClient())
        {

            client.DefaultRequestHeaders.Add("Authorization", authorization);
            client.DefaultRequestHeaders.Add("x-api-key", apiKey); // 한진 API에서 요구하는 키 추가

            HttpResponseMessage response = await client.SendAsync(new HttpRequestMessage(method, url));
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

        return DateTime.UtcNow.ToString("yyyyMMddHHmmss");
    }

    private static string ComputeHmacSHA256(string message, string key)
    {
        using (HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
        {
            byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }

    private static string GetQueryString(string url)
    {
        Uri uri = new Uri(url);
        return uri.Query.Length > 0 ? HttpUtility.UrlDecode(uri.Query.Substring(1)) : "";
    }
}

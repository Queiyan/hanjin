using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace WinFormsApp1.Services
{
    public class PostcodeService : IPostcodeService
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;

        public PostcodeService(string apiKey)
        {
            _apiKey = apiKey;
            _httpClient = new HttpClient();
        }

        public async Task<(string zipCode, string address)> SearchAddressAsync(string query)
        {
            try
            {
                string requestUrl = $"https://www.juso.go.kr/addrlink/addrLinkApi.do?confmKey={_apiKey}&currentPage=1&countPerPage=10&keyword={query}&resultType=json";

                HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(responseBody);

                if (json["results"]["common"]["errorCode"].ToString() == "0")
                {
                    var jusoList = json["results"]["juso"];
                    if (jusoList.HasValues)
                    {
                        var firstJuso = jusoList[0];
                        return (firstJuso["zipNo"].ToString(), firstJuso["roadAddr"].ToString());
                    }
                }
                else
                {
                    string errorMessage = json["results"]["common"]["errorMessage"].ToString();
                    throw new Exception($"API 오류: {errorMessage}");
                }

                return (null, null);
            }
            catch (Exception ex)
            {
                throw new Exception($"주소 검색 중 오류 발생: {ex.Message}");
            }
        }

        public bool ValidateSearchQuery(string query, out string sanitizedQuery)
        {
            sanitizedQuery = query;

            // 특수문자 제거
            string specialCharsPattern = @"[%=><]";
            if (Regex.IsMatch(sanitizedQuery, specialCharsPattern))
            {
                sanitizedQuery = Regex.Replace(sanitizedQuery, specialCharsPattern, "");
                return false;
            }

            // SQL 예약어 배열
            string[] sqlKeywords = new string[]
            {
                "OR", "SELECT", "INSERT", "DELETE", "UPDATE", "CREATE", "DROP", "EXEC",
                "UNION", "FETCH", "DECLARE", "TRUNCATE"
            };

            foreach (string keyword in sqlKeywords)
            {
                string pattern = $@"\b{keyword}\b";
                if (Regex.IsMatch(sanitizedQuery, pattern, RegexOptions.IgnoreCase))
                {
                    sanitizedQuery = Regex.Replace(sanitizedQuery, pattern, "", RegexOptions.IgnoreCase);
                    return false;
                }
            }

            return true;
        }
    }
} 
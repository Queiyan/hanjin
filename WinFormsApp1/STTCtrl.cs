using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using System.Security.Cryptography;
using System.Text;
using NAudio.Wave;
using System.IO;
using System.Collections.Generic;

namespace WinFormsApp1
{
    public class STTCtrl
    {
        private readonly Form loadingForm;
        private const string AccessKey = "^o'cs0'$6toltv_G(;h^B7KI$^~WOE+p";
        private const string SecretKey = "9^j0yV^8RyYc(SV,nz'qRLt-=322Xw.z";
        private const string PVGS = "https://pvgs.dev.aipando.com:443";

        private bool stateStart;
        private bool recordState;
        private string transcript = string.Empty;
        public string Message { get; private set; } = string.Empty;
        private WaveInEvent waveSource;
        private MemoryStream audioStream;
        private HubConnection connection;
        private readonly Dictionary<string, string> requiredParams = new()
        {
            { "chatbot", "courier" },
            { "chatbotType", "common/address" },
            { "kioskId", "pandojawsDemo" },
            { "storeId", "pandojawsDemo" },
            { "brandId", "pandojawsDemo" },
            { "storeName", "kiosk-demo" },
            { "callid", Guid.NewGuid().ToString() },
            { "requestId", Guid.NewGuid().ToString() },
        };

        public STTCtrl(Form loadingForm)
        {
            this.loadingForm = loadingForm;
        }

        public async Task<bool> StartSTTAsync()
        {
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
            var signature = ComputeHmacSHA256($"pando{AccessKey}{timestamp}", SecretKey);

            var auth = new Dictionary<string, string>
            {
                { "message", Message },
                { "timestamp", timestamp },
                { "key", SecretKey },
                { "signature", signature }
            };

            connection = new HubConnectionBuilder()
                .WithUrl(PVGS, options =>
                {
                    foreach (var entry in auth)
                    {
                        options.Headers.Add(entry.Key, entry.Value);
                    }
                })
                .WithAutomaticReconnect()
                .Build();

            try
            {
                await connection.StartAsync();
                Console.WriteLine("Connected to the server.");

                InitializeConnectionHandlers();
                connection.InvokeAsync("START", requiredParams);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error starting connection: " + ex.Message);
                return false;
            }
        }

        private void InitializeConnectionHandlers()
        {
            connection.On<string>("connect", res =>
            {
                Console.WriteLine("Connected: " + res);
                requiredParams["callid"] = Guid.NewGuid().ToString();
                connection.InvokeAsync("START", requiredParams);
            });

            connection.On<string>("START", async res =>
            {
                Console.WriteLine("START: " + res);
                stateStart = true;
                await SendMessageConfigAsync("ko");
            });

            connection.On<string>("MESSAGE_BI", res =>
            {
                transcript = res;
                Console.WriteLine("MESSAGE_BI: " + res);
                if (string.IsNullOrEmpty(transcript))
                {
                    StopRecording();
                }
            });

            connection.On<string>("MESSAGE", res =>
            {
                Console.WriteLine("MESSAGE: " + res);
                Message = res;
                StopRecording();
                loadingForm.Close();
            });

            connection.On<string>("MESSAGE_CONFIG_BI", res =>
            {
                Console.WriteLine("MESSAGE_CONFIG_BI: " + res);
                StartRecording();
            });
        }

        private void StartRecording()
        {
            waveSource = new WaveInEvent
            {
                WaveFormat = new WaveFormat(48000, 1)
            };

            waveSource.DataAvailable += (s, e) =>
            {
                if (recordState && connection.State == HubConnectionState.Connected)
                {
                    connection.InvokeAsync("MESSAGE_AUDIO", e.Buffer);
                }
            };

            waveSource.RecordingStopped += (s, e) => waveSource.Dispose();

            waveSource.StartRecording();
            recordState = true;
            Console.WriteLine("Recording started.");
        }

        public void StopRecording()
        {
            if (recordState)
            {
                waveSource.StopRecording();
                recordState = false;
                Console.WriteLine("Recording stopped.");
            }
        }

        private async Task SendMessageConfigAsync(string language)
        {
            if (stateStart)
            {
                var fullLang = new Dictionary<string, string>
                {
                    { "ko", "ko-KR" },
                    { "en", "en-US" },
                    { "ja", "ja-JP" },
                    { "zh", "zh-CN" },
                };

                var msgConfigData = new
                {
                    requiredParams = new Dictionary<string, string>(requiredParams)
                    {
                        ["requestId"] = Guid.NewGuid().ToString()
                    },
                    sttOptions = new
                    {
                        language_code = fullLang[language],
                        alternative_language_codes = new[] { "en-US", "ja-JP", "zh-CN" },
                        encoding = "WEBM_OPUS",
                        sampleRateHertz = 48000
                    }
                };

                await connection.InvokeAsync("MESSAGE_CONFIG_BI", msgConfigData);
            }
        }

        private static string ComputeHmacSHA256(string data, string key)
        {
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
            return Convert.ToBase64String(hash);
        }
    }
}

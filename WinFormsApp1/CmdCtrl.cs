using System;
using System.Diagnostics;
using System.Text;

namespace WinFormsApp1
{
    public static class CmdCtrl
    {
        public static string RunCMD(string command)
        {
            // Ensure encoding providers are registered
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            string response = string.Empty;

            // Configure process settings
            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/C chcp 65001 & {command}", // Set UTF-8 encoding for command output
                    StandardOutputEncoding = Encoding.UTF8,
                    StandardErrorEncoding = Encoding.UTF8,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            try
            {
                // Start the process
                process.Start();

                // Read output and error streams
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                // Log output and error
                if (!string.IsNullOrEmpty(output))
                {
                    Console.WriteLine("Output:");
                    Console.WriteLine(output);
                    response = output;
                }

                if (!string.IsNullOrEmpty(error))
                {
                    Console.WriteLine("Error:");
                    Console.WriteLine(error);
                    response = error;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
            finally
            {
                process.Dispose();
            }

            return response;
        }
    }
}

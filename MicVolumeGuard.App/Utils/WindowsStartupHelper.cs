using System;
using System.Diagnostics;
using Microsoft.Win32;

namespace MicVolumeGuard.App.Utils
{
    public static class WindowsStartupHelper
    {
        private const string RunKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Run";
        private const string RunValueName = "MicVolumeGuard";

        public static void EnsureStartupRegistration(bool enabled)
        {
            using var key = Registry.CurrentUser.OpenSubKey(RunKeyPath, writable: true);
            if (key == null)
            {
                return;
            }

            if (!enabled)
            {
                key.DeleteValue(RunValueName, throwOnMissingValue: false);
                return;
            }

            var executablePath = Process.GetCurrentProcess().MainModule?.FileName;
            if (string.IsNullOrWhiteSpace(executablePath))
            {
                return;
            }

            key.SetValue(RunValueName, QuotePath(executablePath));
        }

        public static bool IsStartupEnabled()
        {
            using var key = Registry.CurrentUser.OpenSubKey(RunKeyPath, writable: false);
            var current = key?.GetValue(RunValueName) as string;
            return !string.IsNullOrWhiteSpace(current);
        }

        private static string QuotePath(string path)
        {
            return $"\"{path}\"";
        }
    }
}

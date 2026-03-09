using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;

namespace MicVolumeGuard.App.Utils
{
    public static class PortableUninstallHelper
    {
        private const string UninstallKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Uninstall\MicVolumeGuard";

        public static void EnsureRegistration()
        {
            var executablePath = Process.GetCurrentProcess().MainModule?.FileName;
            if (string.IsNullOrWhiteSpace(executablePath))
            {
                return;
            }

            var installDirectory = Path.GetDirectoryName(executablePath);
            if (string.IsNullOrWhiteSpace(installDirectory))
            {
                return;
            }

            var uninstallScript = Path.Combine(installDirectory, "uninstall-micvolumeguard.cmd");

            using var key = Registry.CurrentUser.CreateSubKey(UninstallKeyPath, writable: true);
            if (key == null)
            {
                return;
            }

            key.SetValue("DisplayName", "Mic Volume Guard");
            key.SetValue("Publisher", "Rajan Jha");
            key.SetValue("DisplayVersion", "1.0.1");
            key.SetValue("InstallLocation", installDirectory);
            key.SetValue("DisplayIcon", executablePath);
            key.SetValue("NoModify", 1, RegistryValueKind.DWord);
            key.SetValue("NoRepair", 1, RegistryValueKind.DWord);

            var uninstallCommand = File.Exists(uninstallScript)
                ? QuotePath(uninstallScript)
                : QuotePath(executablePath);
            key.SetValue("UninstallString", uninstallCommand);
        }

        private static string QuotePath(string path)
        {
            return $"\"{path}\"";
        }
    }
}

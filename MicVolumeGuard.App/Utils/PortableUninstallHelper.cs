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

            // Create uninstall batch script
            CreateUninstallScript(uninstallScript, executablePath, installDirectory);

            var uninstallCommand = QuotePath(uninstallScript);
            key.SetValue("UninstallString", uninstallCommand);
        }

        private static string QuotePath(string path)
        {
            return $"\"{path}\"";
        }

        private static void CreateUninstallScript(string scriptPath, string exePath, string installDir)
        {
            try
            {
                // Create PowerShell uninstall script (more reliable)
                var psScriptPath = Path.Combine(installDir, "uninstall-micvolumeguard.ps1");
                var psScriptContent = $@"# MicVolumeGuard Uninstaller
Write-Host ""Uninstalling MicVolumeGuard..."" -ForegroundColor Cyan
Write-Host """"

# Stop any running instances
Write-Host ""Stopping MicVolumeGuard..."" -ForegroundColor Yellow
Get-Process -Name ""MicVolumeGuard"" -ErrorAction SilentlyContinue | Stop-Process -Force
Start-Sleep -Seconds 2

# Remove startup registry entry
Write-Host ""Removing startup entry..."" -ForegroundColor Yellow
Remove-ItemProperty -Path ""HKCU:\Software\Microsoft\Windows\CurrentVersion\Run"" -Name ""MicVolumeGuard"" -ErrorAction SilentlyContinue

# Remove uninstall registry entry
Write-Host ""Removing uninstall entry..."" -ForegroundColor Yellow
Remove-Item -Path ""HKCU:\Software\Microsoft\Windows\CurrentVersion\Uninstall\MicVolumeGuard"" -Recurse -Force -ErrorAction SilentlyContinue

# Delete settings directory
Write-Host ""Removing settings..."" -ForegroundColor Yellow
$settingsPath = ""$env:LOCALAPPDATA\MicVolumeGuard""
if (Test-Path $settingsPath) {{
    Remove-Item -Path $settingsPath -Recurse -Force -ErrorAction SilentlyContinue
}}

Write-Host """" -ForegroundColor Green
Write-Host ""MicVolumeGuard has been uninstalled successfully!"" -ForegroundColor Green
Write-Host """" -ForegroundColor Green
Write-Host ""The application folder can be deleted manually if needed:"" -ForegroundColor Cyan
Write-Host ""{installDir}"" -ForegroundColor White
Write-Host """"

# Keep window open
Read-Host ""Press Enter to close""
";
                File.WriteAllText(psScriptPath, psScriptContent);

                // Create batch wrapper that calls PowerShell script
                var batchContent = $@"@echo off
powershell.exe -ExecutionPolicy Bypass -NoProfile -File ""%~dp0uninstall-micvolumeguard.ps1""
";
                File.WriteAllText(scriptPath, batchContent);
            }
            catch
            {
                // Silently fail if we can't create the script
            }
        }
    }
}

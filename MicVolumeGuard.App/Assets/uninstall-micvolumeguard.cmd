@echo off
setlocal
set APPDIR=%~dp0

reg delete "HKCU\Software\Microsoft\Windows\CurrentVersion\Run" /v "MicVolumeGuard" /f >nul 2>nul
reg delete "HKCU\Software\Microsoft\Windows\CurrentVersion\Uninstall\MicVolumeGuard" /f >nul 2>nul
rmdir /s /q "%LOCALAPPDATA%\MicVolumeGuard" >nul 2>nul

powershell -NoProfile -WindowStyle Hidden -Command "Start-Sleep -Seconds 2; Remove-Item -LiteralPath '%APPDIR%' -Recurse -Force -ErrorAction SilentlyContinue"

echo Mic Volume Guard has been uninstalled.
echo If files are still open, close them and delete the folder manually.
pause

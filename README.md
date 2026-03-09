# Mic Volume Guard for Windows

Stop random mic auto-gain and auto-volume drops on Windows.

Mic Volume Guard locks and stabilizes your microphone endpoint volume so apps like Google Meet, Steam voice chat, Zoom, Teams, Discord, and recording tools cannot keep changing your mic level behind your back.

Created by Rajan Jha.

## Why this helps

Many Windows users face sudden mic boosts or decreases during calls and recordings.
This app monitors your mic volume and restores it to your selected level automatically.

## Features

- Locks default microphone volume to your chosen percentage.
- Detects and reverts unwanted mic volume changes in near real-time.
- Small always-on-top overlay with live mic level.
- Clickable `+` and `-` controls on the overlay to adjust volume quickly.
- Draggable overlay with saved position.
- System tray controls for settings, lock toggle, and exit.
- Startup with Windows option.
- Persistent settings at `%LOCALAPPDATA%\MicVolumeGuard\settings.json`.

## Direct Download

Download and extract:
- `MicVolumeGuard-Windows-win-x64.zip`

Run:
- `MicVolumeGuard.App.exe`

## Build from source

Requirements:
- Windows 10/11
- .NET 6 SDK or later

Build:

```powershell
dotnet build .\MicVolumeGuard.sln
```

Run:

```powershell
dotnet run --project .\MicVolumeGuard.App\MicVolumeGuard.App.csproj
```

## Support

If Mic Volume Guard improved your meetings or recordings, please star this repository.
Your support helps more Windows users discover the fix.

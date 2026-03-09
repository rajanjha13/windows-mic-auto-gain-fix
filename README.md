# Mic Volume Guard for Windows

[![CI](https://img.shields.io/github/actions/workflow/status/rajanjha13/windows-mic-auto-gain-fix/ci.yml?branch=main&label=build)](https://github.com/rajanjha13/windows-mic-auto-gain-fix/actions/workflows/ci.yml)
[![Release](https://img.shields.io/github/v/release/rajanjha13/windows-mic-auto-gain-fix)](https://github.com/rajanjha13/windows-mic-auto-gain-fix/releases)
[![License](https://img.shields.io/github/license/rajanjha13/windows-mic-auto-gain-fix)](LICENSE)

Stop Windows mic auto gain and auto decrease during meetings and recordings.

Mic Volume Guard is a Windows microphone auto-gain fix that locks your mic endpoint level and prevents random volume changes caused by apps such as Google Meet, Steam voice chat, Zoom, Teams, Discord, and recording tools.

Created by Rajan Jha.

## Keywords

Windows mic auto gain fix, microphone volume lock, Google Meet mic volume fix, Steam mic auto adjust fix, recording mic level stabilizer.

## Why this project exists

Many Windows users see sudden microphone boosts or drops mid-call. This app continuously watches the default mic endpoint and restores your chosen level in near real-time.

## Features

- Lock default Windows microphone volume to a target percentage.
- Detect and revert unwanted mic volume changes quickly.
- Always-on-top compact overlay with live mic level.
- Clickable `+` and `-` overlay buttons to adjust mic volume.
- Draggable overlay with persistent saved position.
- Tray controls for settings, lock toggle, and exit.
- Optional startup with Windows.
- Persistent local settings at `%LOCALAPPDATA%\MicVolumeGuard\settings.json`.

## Download

1. Open Releases: https://github.com/rajanjha13/windows-mic-auto-gain-fix/releases
2. Download `MicVolumeGuard-Windows-win-x64.zip` from the latest release.
3. Extract the zip.
4. Run `MicVolumeGuard.App.exe`.

## Build from source

Requirements:
- Windows 10/11
- .NET 6 SDK or later

```powershell
dotnet restore .\MicVolumeGuard.sln
dotnet build .\MicVolumeGuard.sln
dotnet run --project .\MicVolumeGuard.App\MicVolumeGuard.App.csproj
```

## Roadmap

- MSI installer with uninstall support.
- Optional per-app behavior presets.
- Additional diagnostics and logging for troubleshooting.

## Docs

- Changelog: `CHANGELOG.md`
- Release notes: `RELEASE_NOTES_v1.0.0.md`
- Contributing guide: `CONTRIBUTING.md`
- Security policy: `SECURITY.md`

## Support

If this project solved your mic auto-gain issue, please star the repository.
It helps more Windows users discover a working fix.

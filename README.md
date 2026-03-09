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
- Mic icon overlay for clearer, more reliable visual status.
- Compact overlay slider to adjust mic volume quickly.
- Draggable overlay with persistent saved position.
- Tray controls for settings, lock toggle, and exit.
- Noise cancellation toggle for supported app-level scenarios.
- Optional startup with Windows.
- Persistent local settings at `%LOCALAPPDATA%\MicVolumeGuard\settings.json`.

## Download

<p>
	<a href="https://github.com/rajanjha13/windows-mic-auto-gain-fix/raw/main/MicVolumeGuard-Windows-lite-win-x64.zip">
		<img src="https://img.shields.io/badge/Download-Lite_ZIP_(Small)-2ea44f?style=for-the-badge&logo=windows" alt="Download Mic Volume Guard Lite for Windows" />
	</a>
	<a href="https://github.com/rajanjha13/windows-mic-auto-gain-fix/raw/main/MicVolumeGuard-Windows-win-x64.zip">
		<img src="https://img.shields.io/badge/Download-Full_ZIP_(No_Runtime_Required)-1f6feb?style=for-the-badge&logo=windows" alt="Download Mic Volume Guard Full for Windows" />
	</a>
</p>

1. Click the download button above.
2. Lite package: `MicVolumeGuard-Windows-lite-win-x64.zip` (very small, requires .NET Desktop Runtime on the PC).
3. Full package: `MicVolumeGuard-Windows-win-x64.zip` (larger, runs without installing .NET runtime).
4. Extract ZIP and run `START-MicVolumeGuard.cmd` (recommended).
5. If Windows blocks launch, right-click `MicVolumeGuard.exe` > `Properties` > check `Unblock`.

## Uninstall

- ZIP install now registers in Windows after first launch.
- Uninstall from `Settings > Apps > Installed apps` by searching `Mic Volume Guard`.
- If a leftover folder remains, delete the extracted app folder manually.

## Usage Terms

- Official binaries can be used by end users.
- Source code reuse, redistribution, modification, and derivative apps are not allowed without written permission.
- See `LICENSE` and `NOTICE.md` for full terms.

## Roadmap

- MSI installer with uninstall support.
- Optional per-app behavior presets.
- Additional diagnostics and logging for troubleshooting.

## Docs

- Changelog: `CHANGELOG.md`
- Release notes: `RELEASE_NOTES_v1.0.0.md`
- Security policy: `SECURITY.md`
- Notice: `NOTICE.md`

## Support

If this project solved your mic auto-gain issue, please star the repository.
It helps more Windows users discover a working fix.

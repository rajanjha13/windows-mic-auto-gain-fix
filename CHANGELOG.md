# Changelog

All notable changes to this project will be documented in this file.

## [1.0.0] - 2026-03-09

### Added
- Initial Windows WPF application: Mic Volume Guard.
- Real-time microphone endpoint volume monitoring and lock enforcement.
- Compact always-on-top overlay with live mic percentage.
- Overlay `+` and `-` controls for quick mic volume adjustment.
- Draggable overlay with persisted screen position.
- Tray icon actions: open settings, toggle lock, exit.
- Settings window with lock toggle, target volume slider, and startup toggle.
- Windows startup registration support through `HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Run`.
- Persistent app settings in `%LOCALAPPDATA%\\MicVolumeGuard\\settings.json`.
- Self-contained Windows release package (`win-x64`) for direct distribution.

### Notes
- This app controls Windows microphone endpoint volume. Some applications may still apply internal AGC/DSP in their own audio processing pipeline.

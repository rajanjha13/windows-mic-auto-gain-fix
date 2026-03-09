using System;
using System.Windows;
using MicVolumeGuard.App.Models;
using MicVolumeGuard.App.Services;
using MicVolumeGuard.App.UI;
using MicVolumeGuard.App.Utils;

namespace MicVolumeGuard.App
{
    public partial class App : Application
    {
        private MainWindow? _mainWindow;
        private OverlayWindow? _overlayWindow;
        private MicVolumeService? _micVolumeService;
        private MicVolumeGuardService? _guardService;
        private NotifyIconService? _trayService;
        private readonly SettingsService _settingsService = new();
        private AppSettings _settings = AppSettings.Default();
        private bool _noiseCancellationEnabled;
        private bool _isMuted;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _settings = _settingsService.Load();
            _noiseCancellationEnabled = _settings.NoiseCancellationEnabled;
            WindowsStartupHelper.EnsureStartupRegistration(_settings.StartWithWindows);
            PortableUninstallHelper.EnsureRegistration();

            _micVolumeService = new MicVolumeService();
            _guardService = new MicVolumeGuardService(_micVolumeService)
            {
                LockedVolume = _settings.LockedVolume,
                IsLockEnabled = _settings.IsLockEnabled
            };
            _guardService.Start();

            // Enforce target level immediately on startup so first-run default is 70%.
            if (_guardService.IsLockEnabled)
            {
                _micVolumeService.SetVolume(_guardService.LockedVolume);
            }

            _overlayWindow = new OverlayWindow();
            _overlayWindow.Left = _settings.OverlayLeft;
            _overlayWindow.Top = _settings.OverlayTop;
            _overlayWindow.VolumeSetRequested += OnOverlayVolumeSetRequested;
            _overlayWindow.Show();

            _mainWindow = new MainWindow(_guardService, _micVolumeService, _overlayWindow);
            _isMuted = _micVolumeService.GetMute() == true;
            _mainWindow.SyncFromState(_guardService.IsLockEnabled, _guardService.LockedVolume, _settings.StartWithWindows, _noiseCancellationEnabled);
            _mainWindow.Hide();

            _trayService = new NotifyIconService();
            _trayService.OpenSettingsRequested += ShowSettings;
            _trayService.ToggleLockRequested += ToggleLock;
            _trayService.ExitRequested += Shutdown;
            _trayService.Show();

            _micVolumeService.VolumeChanged += UpdateOverlay;
            _micVolumeService.MuteChanged += UpdateMuteState;
            var currentVolume = _micVolumeService.GetVolume() ?? _settings.LockedVolume;
            _overlayWindow.UpdateVolume(currentVolume, _guardService.IsLockEnabled, _isMuted, _noiseCancellationEnabled);
            _trayService.SetLockState(_guardService.IsLockEnabled);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            if (_mainWindow != null)
            {
                _mainWindow.AllowClose = true;
                _mainWindow.Close();
            }

            _settings = new AppSettings
            {
                IsLockEnabled = _guardService?.IsLockEnabled ?? _settings.IsLockEnabled,
                LockedVolume = _guardService?.LockedVolume ?? _settings.LockedVolume,
                StartWithWindows = WindowsStartupHelper.IsStartupEnabled(),
                OverlayLeft = _overlayWindow?.Left ?? _settings.OverlayLeft,
                OverlayTop = _overlayWindow?.Top ?? _settings.OverlayTop,
                NoiseCancellationEnabled = _mainWindow?.NoiseCancellationEnabled ?? _noiseCancellationEnabled
            };
            _settingsService.Save(_settings);

            _trayService?.Dispose();
            _overlayWindow?.Close();
            _guardService?.Dispose();
            _micVolumeService?.Dispose();

            base.OnExit(e);
        }

        private void ShowSettings()
        {
            if (_mainWindow == null)
            {
                return;
            }

            _mainWindow.Show();
            _mainWindow.Activate();
        }

        private void ToggleLock()
        {
            if (_guardService == null || _overlayWindow == null || _trayService == null)
            {
                return;
            }

            _guardService.IsLockEnabled = !_guardService.IsLockEnabled;
            _trayService.SetLockState(_guardService.IsLockEnabled);
            _overlayWindow.UpdateVolume(_micVolumeService?.GetVolume() ?? _guardService.LockedVolume, _guardService.IsLockEnabled, _isMuted, _mainWindow?.NoiseCancellationEnabled ?? _noiseCancellationEnabled);
            _mainWindow?.SyncFromState(_guardService.IsLockEnabled, _guardService.LockedVolume, WindowsStartupHelper.IsStartupEnabled(), _mainWindow?.NoiseCancellationEnabled ?? _noiseCancellationEnabled);
        }

        private void UpdateOverlay(float value)
        {
            if (_overlayWindow == null || _guardService == null)
            {
                return;
            }

            Dispatcher.Invoke(() =>
            {
                _overlayWindow.UpdateVolume(value, _guardService.IsLockEnabled, _isMuted, _mainWindow?.NoiseCancellationEnabled ?? _noiseCancellationEnabled);
            });
        }

        private void OnOverlayVolumeSetRequested(float targetValue)
        {
            if (_guardService == null || _micVolumeService == null || _overlayWindow == null)
            {
                return;
            }

            var nextValue = Math.Clamp(targetValue, 0f, 1f);
            _micVolumeService.SetVolume(nextValue);
            _guardService.LockedVolume = nextValue;
            _overlayWindow.UpdateVolume(nextValue, _guardService.IsLockEnabled, _isMuted, _mainWindow?.NoiseCancellationEnabled ?? _noiseCancellationEnabled);
            _mainWindow?.SyncFromState(_guardService.IsLockEnabled, _guardService.LockedVolume, WindowsStartupHelper.IsStartupEnabled(), _mainWindow?.NoiseCancellationEnabled ?? _noiseCancellationEnabled);
        }

        private void UpdateMuteState(bool isMuted)
        {
            _isMuted = isMuted;

            if (_overlayWindow == null || _guardService == null)
            {
                return;
            }

            Dispatcher.Invoke(() =>
            {
                _overlayWindow.UpdateVolume(_micVolumeService?.GetVolume() ?? _guardService.LockedVolume, _guardService.IsLockEnabled, _isMuted, _mainWindow?.NoiseCancellationEnabled ?? _noiseCancellationEnabled);
            });
        }
    }
}

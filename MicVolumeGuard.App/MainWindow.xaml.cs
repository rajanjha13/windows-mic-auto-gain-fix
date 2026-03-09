using System.Windows;
using MicVolumeGuard.App.Services;
using MicVolumeGuard.App.UI;
using MicVolumeGuard.App.Utils;

namespace MicVolumeGuard.App
{
    public partial class MainWindow : Window
    {
        private readonly MicVolumeGuardService _guardService;
        private readonly MicVolumeService _micVolumeService;
        private readonly OverlayWindow _overlayWindow;
        private bool _initializing;

        public bool AllowClose { get; set; }

        public MainWindow(MicVolumeGuardService guardService, MicVolumeService micVolumeService, OverlayWindow overlayWindow)
        {
            InitializeComponent();

            _guardService = guardService;
            _micVolumeService = micVolumeService;
            _overlayWindow = overlayWindow;

            _initializing = true;
            LockToggle.IsChecked = _guardService.IsLockEnabled;
            StartupToggle.IsChecked = WindowsStartupHelper.IsStartupEnabled();
            VolumeSlider.Value = _guardService.LockedVolume * 100;
            VolumeLabel.Text = $"Target Volume: {VolumeSlider.Value:0}%";
            _initializing = false;
        }

        public void SyncFromState(bool isLockEnabled, float lockedVolume, bool startWithWindows)
        {
            _initializing = true;
            LockToggle.IsChecked = isLockEnabled;
            StartupToggle.IsChecked = startWithWindows;
            VolumeSlider.Value = lockedVolume * 100;
            VolumeLabel.Text = $"Target Volume: {VolumeSlider.Value:0}%";
            _initializing = false;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (!AllowClose)
            {
                e.Cancel = true;
                Hide();
                return;
            }

            base.OnClosing(e);
        }

        private void LockToggle_OnChanged(object sender, RoutedEventArgs e)
        {
            if (_initializing)
            {
                return;
            }

            _guardService.IsLockEnabled = LockToggle.IsChecked == true;
            _overlayWindow.UpdateVolume(_micVolumeService.GetVolume() ?? _guardService.LockedVolume, _guardService.IsLockEnabled);
        }

        private void VolumeSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_initializing)
            {
                return;
            }

            var normalized = (float)(VolumeSlider.Value / 100.0);
            _guardService.LockedVolume = normalized;
            VolumeLabel.Text = $"Target Volume: {VolumeSlider.Value:0}%";

            if (_guardService.IsLockEnabled)
            {
                _micVolumeService.SetVolume(normalized);
                _overlayWindow.UpdateVolume(normalized, true);
            }
        }

        private void HideToTray_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void StartupToggle_OnChanged(object sender, RoutedEventArgs e)
        {
            if (_initializing)
            {
                return;
            }

            WindowsStartupHelper.EnsureStartupRegistration(StartupToggle.IsChecked == true);
        }
    }
}

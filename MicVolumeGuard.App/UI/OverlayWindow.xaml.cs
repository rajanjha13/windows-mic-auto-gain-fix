using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace MicVolumeGuard.App.UI
{
    public partial class OverlayWindow : Window
    {
        private bool _suppressSliderEvents;

        public event Action<float>? VolumeSetRequested;

        public OverlayWindow()
        {
            InitializeComponent();
        }

        public void UpdateVolume(float value, bool isLocked, bool isMuted, bool noiseCancellationEnabled)
        {
            var percent = value * 100;
            VolumeText.Text = $"{percent:0}%";
            _suppressSliderEvents = true;
            VolumeSlider.Value = percent;
            _suppressSliderEvents = false;
            StatusText.Text = $"{(isLocked ? "LOCKED" : "UNLOCKED")} | {(noiseCancellationEnabled ? "NC ON" : "NC OFF")}";
            StatusText.Foreground = isLocked
                ? new SolidColorBrush(Color.FromRgb(132, 220, 103))
                : new SolidColorBrush(Color.FromRgb(242, 196, 92));

            MicGlyph.Fill = isMuted
                ? new SolidColorBrush(Color.FromRgb(255, 95, 95))
                : new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }

        private void OverlayRoot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (VolumeSlider.IsMouseOver)
            {
                return;
            }

            if (e.ButtonState == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void VolumeSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_suppressSliderEvents)
            {
                return;
            }

            VolumeSetRequested?.Invoke((float)(VolumeSlider.Value / 100.0));
        }
    }
}

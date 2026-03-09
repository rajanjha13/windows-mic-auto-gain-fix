using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace MicVolumeGuard.App.UI
{
    public partial class OverlayWindow : Window
    {
        public event Action<float>? VolumeStepRequested;
        public event Action? ToggleMuteRequested;

        public OverlayWindow()
        {
            InitializeComponent();
        }

        public void UpdateVolume(float value, bool isLocked, bool isMuted, bool noiseCancellationEnabled)
        {
            var percent = value * 100;
            VolumeText.Text = $"{percent:0}%";
            StatusText.Text = $"{(isLocked ? "LOCKED" : "UNLOCKED")} | {(noiseCancellationEnabled ? "NC ON" : "NC OFF")}";
            StatusText.Foreground = isLocked
                ? new SolidColorBrush(Color.FromRgb(132, 220, 103))
                : new SolidColorBrush(Color.FromRgb(242, 196, 92));

            MicGlyph.Fill = isMuted
                ? new SolidColorBrush(Color.FromRgb(255, 95, 95))
                : new SolidColorBrush(Color.FromRgb(255, 255, 255));
            MuteButton.Content = isMuted ? "U" : "M";
        }

        private void IncreaseButton_Click(object sender, RoutedEventArgs e)
        {
            VolumeStepRequested?.Invoke(0.05f);
        }

        private void DecreaseButton_Click(object sender, RoutedEventArgs e)
        {
            VolumeStepRequested?.Invoke(-0.05f);
        }

        private void MuteButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleMuteRequested?.Invoke();
        }

        private void OverlayRoot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (IncreaseButton.IsMouseOver || DecreaseButton.IsMouseOver || MuteButton.IsMouseOver)
            {
                return;
            }

            if (e.ButtonState == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}

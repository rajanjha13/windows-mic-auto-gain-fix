using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace MicVolumeGuard.App.UI
{
    public partial class OverlayWindow : Window
    {
        public event Action<float>? VolumeStepRequested;

        public OverlayWindow()
        {
            InitializeComponent();
        }

        public void UpdateVolume(float value, bool isLocked)
        {
            var percent = value * 100;
            VolumeText.Text = $"{percent:0}%";
            LockText.Text = isLocked ? "LOCKED" : "UNLOCKED";
            LockText.Foreground = isLocked
                ? new SolidColorBrush(Color.FromRgb(132, 220, 103))
                : new SolidColorBrush(Color.FromRgb(242, 196, 92));
        }

        private void IncreaseButton_Click(object sender, RoutedEventArgs e)
        {
            VolumeStepRequested?.Invoke(0.05f);
        }

        private void DecreaseButton_Click(object sender, RoutedEventArgs e)
        {
            VolumeStepRequested?.Invoke(-0.05f);
        }

        private void OverlayRoot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (IncreaseButton.IsMouseOver || DecreaseButton.IsMouseOver)
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

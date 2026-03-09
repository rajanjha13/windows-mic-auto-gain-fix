using System;

namespace MicVolumeGuard.App.Models
{
    public sealed class AppSettings
    {
        public bool IsLockEnabled { get; set; } = true;

        public float LockedVolume { get; set; } = 0.70f;

        public bool StartWithWindows { get; set; } = true;

        public bool NoiseCancellationEnabled { get; set; }

        public double OverlayLeft { get; set; } = 20;

        public double OverlayTop { get; set; } = 20;

        public AppSettings Normalize()
        {
            LockedVolume = Math.Clamp(LockedVolume, 0f, 1f);
            if (OverlayLeft < 0)
            {
                OverlayLeft = 0;
            }

            if (OverlayTop < 0)
            {
                OverlayTop = 0;
            }

            return this;
        }

        public static AppSettings Default()
        {
            return new AppSettings();
        }
    }
}

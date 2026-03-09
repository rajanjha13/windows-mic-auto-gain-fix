using System;
using System.Timers;

namespace MicVolumeGuard.App.Services
{
    public sealed class MicVolumeGuardService : IDisposable
    {
        private readonly MicVolumeService _micVolumeService;
        private readonly Timer _timer;
        private bool _isApplying;

        public bool IsLockEnabled { get; set; }

        public float LockedVolume { get; set; } = 0.70f;

        public MicVolumeGuardService(MicVolumeService micVolumeService)
        {
            _micVolumeService = micVolumeService;
            _timer = new Timer(250);
            _timer.Elapsed += OnTimerElapsed;
            _micVolumeService.VolumeChanged += OnMicVolumeChanged;
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        private void OnTimerElapsed(object? sender, ElapsedEventArgs e)
        {
            EnforceLock();
        }

        private void OnMicVolumeChanged(float value)
        {
            if (!IsLockEnabled || _isApplying)
            {
                return;
            }

            if (Math.Abs(value - LockedVolume) > 0.01f)
            {
                EnforceLock();
            }
        }

        private void EnforceLock()
        {
            if (!IsLockEnabled)
            {
                return;
            }

            var current = _micVolumeService.GetVolume();
            if (current == null)
            {
                return;
            }

            if (Math.Abs(current.Value - LockedVolume) < 0.01f)
            {
                return;
            }

            try
            {
                _isApplying = true;
                _micVolumeService.SetVolume(LockedVolume);
            }
            finally
            {
                _isApplying = false;
            }
        }

        public void Dispose()
        {
            Stop();
            _timer.Elapsed -= OnTimerElapsed;
            _timer.Dispose();
            _micVolumeService.VolumeChanged -= OnMicVolumeChanged;
        }
    }
}

using System;
using NAudio.CoreAudioApi;

namespace MicVolumeGuard.App.Services
{
    public sealed class MicVolumeService : IDisposable
    {
        private readonly MMDeviceEnumerator _enumerator;
        private MMDevice? _device;

        public event Action<float>? VolumeChanged;

        public MicVolumeService()
        {
            _enumerator = new MMDeviceEnumerator();
            RefreshDefaultDevice();
        }

        public float? GetVolume()
        {
            try
            {
                return _device?.AudioEndpointVolume.MasterVolumeLevelScalar;
            }
            catch
            {
                return null;
            }
        }

        public void SetVolume(float value)
        {
            if (_device == null)
            {
                return;
            }

            var clamped = Math.Clamp(value, 0f, 1f);
            _device.AudioEndpointVolume.MasterVolumeLevelScalar = clamped;
        }

        public void RefreshDefaultDevice()
        {
            Unsubscribe();

            try
            {
                _device = _enumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Multimedia);
                _device.AudioEndpointVolume.OnVolumeNotification += OnVolumeNotification;
            }
            catch
            {
                _device = null;
            }
        }

        private void OnVolumeNotification(AudioVolumeNotificationData data)
        {
            VolumeChanged?.Invoke(data.MasterVolume);
        }

        private void Unsubscribe()
        {
            if (_device != null)
            {
                _device.AudioEndpointVolume.OnVolumeNotification -= OnVolumeNotification;
                _device.Dispose();
                _device = null;
            }
        }

        public void Dispose()
        {
            Unsubscribe();
            _enumerator.Dispose();
        }
    }
}

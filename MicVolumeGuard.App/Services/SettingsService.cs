using System;
using System.IO;
using System.Text.Json;
using MicVolumeGuard.App.Models;

namespace MicVolumeGuard.App.Services
{
    public sealed class SettingsService
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true
        };

        private readonly string _settingsPath;

        public SettingsService()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var settingsFolder = Path.Combine(appData, "MicVolumeGuard");
            Directory.CreateDirectory(settingsFolder);
            _settingsPath = Path.Combine(settingsFolder, "settings.json");
        }

        public AppSettings Load()
        {
            if (!File.Exists(_settingsPath))
            {
                return AppSettings.Default();
            }

            try
            {
                var json = File.ReadAllText(_settingsPath);
                var parsed = JsonSerializer.Deserialize<AppSettings>(json);
                return parsed?.Normalize() ?? AppSettings.Default();
            }
            catch
            {
                return AppSettings.Default();
            }
        }

        public void Save(AppSettings settings)
        {
            var safe = settings.Normalize();
            var json = JsonSerializer.Serialize(safe, JsonOptions);
            File.WriteAllText(_settingsPath, json);
        }
    }
}

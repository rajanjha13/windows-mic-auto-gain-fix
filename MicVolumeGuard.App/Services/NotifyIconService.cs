using System;
using System.Drawing;
using System.Windows.Forms;

namespace MicVolumeGuard.App.Services
{
    public sealed class NotifyIconService : IDisposable
    {
        private readonly NotifyIcon _notifyIcon;
        private readonly ToolStripMenuItem _toggleLockItem;

        public event Action? OpenSettingsRequested;
        public event Action? ToggleLockRequested;
        public event Action? ExitRequested;

        public NotifyIconService()
        {
            _toggleLockItem = new ToolStripMenuItem("Disable Lock");
            _toggleLockItem.Click += (_, _) => ToggleLockRequested?.Invoke();

            var settingsItem = new ToolStripMenuItem("Open Settings");
            settingsItem.Click += (_, _) => OpenSettingsRequested?.Invoke();

            var exitItem = new ToolStripMenuItem("Exit");
            exitItem.Click += (_, _) => ExitRequested?.Invoke();

            var menu = new ContextMenuStrip();
            menu.Items.Add(settingsItem);
            menu.Items.Add(_toggleLockItem);
            menu.Items.Add(new ToolStripSeparator());
            menu.Items.Add(exitItem);

            _notifyIcon = new NotifyIcon
            {
                Icon = SystemIcons.Shield,
                Text = "Mic Volume Guard",
                ContextMenuStrip = menu,
                Visible = false
            };

            _notifyIcon.DoubleClick += (_, _) => OpenSettingsRequested?.Invoke();
        }

        public void SetLockState(bool isEnabled)
        {
            _toggleLockItem.Text = isEnabled ? "Disable Lock" : "Enable Lock";
        }

        public void Show()
        {
            _notifyIcon.Visible = true;
        }

        public void Dispose()
        {
            _notifyIcon.Visible = false;
            _notifyIcon.Dispose();
        }
    }
}

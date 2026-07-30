using WindowNotificationManager = Ursa.Controls.WindowNotificationManager;

namespace Kokkoro.ViewModels.Main.Settings;

public interface ISettingsNotificationAware
{
    void SetNotificationManager(WindowNotificationManager? notificationManager);
}

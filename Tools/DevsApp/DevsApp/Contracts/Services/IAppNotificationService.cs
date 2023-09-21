using System.Collections.Specialized;
using Microsoft.Windows.AppNotifications;

namespace DevsApp.Contracts.Services;

public interface IAppNotificationService
{
    void Initialize();

    bool Show(in string payload);

    bool Show(in AppNotification appNotification);

    NameValueCollection ParseArguments(in string arguments);

    void Unregister();
}


using Plugin.Fingerprint;

namespace ProPad;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
#if ANDROID
        CrossFingerprint.SetCurrentActivityResolver(() => Microsoft.Maui.ApplicationModel.Platform.CurrentActivity);
#endif

    }
}

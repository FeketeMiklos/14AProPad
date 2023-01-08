using Android.App;
using Plugin.Fingerprint;

namespace ProPad;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        CrossFingerprint.SetCurrentActivityResolver(() => Microsoft.Maui.ApplicationModel.Platform.CurrentActivity);

    }
}

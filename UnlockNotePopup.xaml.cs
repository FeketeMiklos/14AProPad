using CommunityToolkit.Maui.Views;
using Plugin.Fingerprint.Abstractions;
using Plugin.Fingerprint;
using Isopoh.Cryptography.Argon2;

namespace ProPad;

public partial class UnlockNotePopup : Popup
{
    private readonly Note note;

    private bool isClosed = false;

    public UnlockNotePopup(Note note)
    {
        var isFingerprintAvailable = CrossFingerprint.Current.IsAvailableAsync().Result;
        InitializeComponent();
        Size = new Size() { Height = 400 };
        BiometricBtn.IsVisible = isFingerprintAvailable;
        this.note = note;
    }



    private async void PasswordBtn_Clicked(object sender, EventArgs e)
    {
        ErrorText.Text = "";
        PasswordBtn.IsEnabled = false;
        LoadingText.Text = "Betöltés...";

        var isPasswordRight = !string.IsNullOrEmpty(PasswordInput.Text) && await Task.Run(() => Argon2.Verify(note.Password, PasswordInput.Text));


        if (isClosed)
        {
            return;
        }

        LoadingText.Text = "";

        if (isPasswordRight)
        {
            Close(true);
            return;
        }


        PasswordBtn.IsEnabled = true;
        ErrorText.Text = "Hibás jelszó!";
    }

    private async void BiometricBtn_Clicked(object sender, EventArgs e)
    {
        const string title = "Azonosítás";
        const string description = "Azonosítsd magad ujjlenyomat-olvasóval/arcfelismeréssel!";
        var config = new AuthenticationRequestConfiguration(title, description) { };
        var result = await CrossFingerprint.Current.AuthenticateAsync(config);

        if (result.Authenticated)
        {
            Close(true);
            return;
        }

        if (result.Status == FingerprintAuthenticationResultStatus.TooManyAttempts)
        {
            BiometricBtn.IsEnabled = false;
        }
    }

    private void Popup_Closed(object sender, CommunityToolkit.Maui.Core.PopupClosedEventArgs e)
    {
        isClosed = true;
    }
}
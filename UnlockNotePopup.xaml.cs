using CommunityToolkit.Maui.Views;
using Plugin.Fingerprint.Abstractions;
using Plugin.Fingerprint;
using Isopoh.Cryptography.Argon2;
using Xamarin.Google.Crypto.Tink.Subtle;

namespace ProPad;

public partial class UnlockNotePopup : Popup
{
    private readonly Note note;

    private bool isClosed = false;

    public string decryptionPassword = "";

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
        LoadingText.Text = "Bet�lt�s...";

        var isPasswordRight = !string.IsNullOrEmpty(PasswordInput.Text) && await Task.Run(() => Argon2.Verify(note.Password, PasswordInput.Text));

        if (isClosed)
        {
            return;
        }

        LoadingText.Text = "";

        if (isPasswordRight)
        {
            //password.DecodePassword = test;
            decryptionPassword = PasswordInput.Text.ToString();
            Close(true);
            return;
        }


        PasswordBtn.IsEnabled = true;
        ErrorText.Text = "Hib�s jelsz�!";
    }

    private async void BiometricBtn_Clicked(object sender, EventArgs e)
    {
        const string title = "Azonos�t�s";
        const string description = "Azonos�tsd magad ujjlenyomat-olvas�val/arcfelismer�ssel!";
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
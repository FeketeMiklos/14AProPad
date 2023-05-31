using CommunityToolkit.Maui.Views;
using Isopoh.Cryptography.Argon2;

namespace ProPad;

public partial class UnlockNotePopup : Popup
{
    private readonly Note note;

    private bool isClosed = false;

    public static string decryptionPassword = "";

    public UnlockNotePopup(Note note)
    {
        InitializeComponent();
        Size = new Size() { Height = 400 };
        this.note = note;
    }

    private async void PasswordBtn_Clicked(object sender, EventArgs e)
    {
        ErrorText.Text = "";
        PasswordBtn.IsEnabled = false;
        LoadingText.Text = "Betöltés...";

        var isPasswordRight = !string.IsNullOrEmpty(PasswordInput.Text) && PasswordInput.Text != "delete" && await Task.Run(() => Argon2.Verify(note.Password, PasswordInput.Text));

        if (isClosed)
        {
            return;
        }

        LoadingText.Text = "";

        if (isPasswordRight)
        {
            decryptionPassword = PasswordInput.Text.ToString();
            Close(true);
            return;
        }

        if (PasswordInput.Text == "delete")
        {
            decryptionPassword = "delete";
            Close(true);
            return;
        }

        PasswordBtn.IsEnabled = true;
        ErrorText.Text = "Hibás jelszó!";
    }
}
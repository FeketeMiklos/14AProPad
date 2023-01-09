using Isopoh.Cryptography.Argon2;
using System.Text;

namespace ProPad;


public partial class EditorPage : ContentPage
{
    Note _note;
    bool isPasswordChanged = false;


    public EditorPage()
    {
        InitializeComponent();
        this.BindingContext = this;
    }

    public EditorPage(Note note)
    {
        InitializeComponent();
        _note = note;
        noteTitle.Text = note.Title;
        noteEditor.Text = note.Text;
        noteEditor.Focus();
        secretNoteCb.IsChecked = note.IsCoded;

        if (_note != null)
        {
            this.Title = _note.Title;
        }
        else
        {
            this.Title = "Új jegyzet";
        }
    }

    private async void deleteNote_Clicked(object sender, EventArgs e)
    {
        bool delete = await DisplayAlert("Törlés", "Biztosan törölni akarod a jegyzetet?", "Igen", "Nem");
        if (delete)
        {
            if (_note != null)
            {
                await App.Database.DeleteNote(_note);
                await Navigation.PopToRootAsync();
            }
            else
            {
                await Navigation.PopToRootAsync();
            }

        }
    }

    private async void saveNote_Clicked(object sender, EventArgs e)
    {
        btnSaveNote.IsEnabled = false;
        bool save = await DisplayAlert("Mentés", "Biztosan menteni akarod a jegyzetet?", "Igen", "Nem");
        if (save)
        {
            if (!string.IsNullOrWhiteSpace(noteTitle.Text) || !string.IsNullOrWhiteSpace(noteEditor.Text))
            {
                if (_note != null)
                {
                    _note.Title = noteTitle.Text;
                    _note.Text = noteEditor.Text;
                    _note.Password = await CreatePassword(_note.Password);
                    await App.Database.UpdateNote(_note);
                    SetPasswordFieldToUpdate();

                }
                else
                {
                    bool isCoded = secretNoteCb.IsChecked;
                    var password = await CreatePassword(_note?.Password);

                    _note = new Note
                    {
                        Title = noteTitle.Text,
                        Text = noteEditor.Text,
                        IsCoded = isCoded,
                        Password = password,
                    };
                    App.Database.SaveNote(_note);
                    SetPasswordFieldToUpdate();
                }
            }
            else
            {
                await DisplayAlert("Mentés", "A jegyzet mentéséhez adj meg címet bagy szöveget!", "OK");
            }
        }
        btnSaveNote.IsEnabled = true;
    }

    private void secretNoteCb_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var parent = passwordInput.Parent as Border;
        parent.IsVisible = secretNoteCb.IsChecked;
        if (!secretNoteCb.IsChecked)
        {
            passwordInput.Text = "";
        }
    }

    private void passwordInput_Focused(object sender, FocusEventArgs e)
    {
        if (_note != null)
        {
            isPasswordChanged = true;
            passwordInput.Text = "";
        }
    }

    private void SetPasswordFieldToUpdate()
    {
        if (_note.IsCoded)
        {
            // Azért, hogy a jelszó mezőben legyen *****
            passwordInput.Text = "      ";
        }
    }

    private Task<string> CreatePassword(string oldPassword)
    {
        // nincs lejelszavazva
        if (!secretNoteCb.IsChecked)
        {
            return Task.FromResult<string>( null);
        }


        // új a jegyzet
        if (_note == null || isPasswordChanged)
        {
            return Task.Run(() =>
            {
                return Argon2.Hash(passwordInput.Text, 2, 65536, 2);
            });
        }

        return Task.FromResult(oldPassword);
    }
}
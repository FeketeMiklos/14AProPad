using CommunityToolkit.Maui.Views;
using Isopoh.Cryptography.Argon2;
using Microsoft.Maui.Graphics;
using System.Text;

namespace ProPad;


public partial class EditorPage : ContentPage
{
    Note _note;
    public readonly UnlockNotePopup NotePopup;
    bool isPasswordChanged = false;

    public EditorPage()
    {
        InitializeComponent();
        SetSettings();
        this.BindingContext = this;
    }

    public EditorPage(Note note)
    {
        InitializeComponent();
        SetSettings();

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

        if (note.IsCoded)
        {
            if (UnlockNotePopup.decryptionPassword == "delete")
            {
                btnSaveNote.IsEnabled = false;
                btnSaveNote.Opacity = 0.5;
                noteEditor.Text = note.Text;
            }
            else
            {
                noteEditor.Text = EncryptData.Decrypt(note.Text, UnlockNotePopup.decryptionPassword);
            }
        }
        else
        {
            noteEditor.Text = note.Text;
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
                if (secretNoteCb.IsChecked && passwordInput.Text == null)
                {
                    await DisplayAlert("Mentés", "A jegyzet mentéséhez adj meg jelszót", "OK");

                }
                else
                {
                    var popup = new LoadingPopup();
                    this.ShowPopupAsync(popup);

                    //meglévő note
                    if (_note != null)
                    {
                        _note.Title = noteTitle.Text;
                        //_note.Text = noteEditor.Text;
                        _note.IsCoded = secretNoteCb.IsChecked;
                        _note.Password = await CreatePassword(_note?.Password);
                        //_note.Password = await CreatePassword(passwordInput.Text);

                        if (_note.IsCoded)
                        {
                            _note.Text = EncryptData.Encrypt(noteEditor.Text, passwordInput.Text);
                        }
                        else
                        {
                            _note.Text = noteEditor.Text;
                        }

                        await App.Database.UpdateNote(_note);
                        SetPasswordFieldToUpdate();
                    }
                    else    //új note
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

                        if (isCoded)
                        {
                            _note.Text = EncryptData.Encrypt(noteEditor.Text, passwordInput.Text);
                        }

                        //_note = new Note
                        //{
                        //    Title = noteTitle.Text,                     
                        //    Text = noteEditor.Text,
                        //    IsCoded = isCoded,
                        //    Password = password,
                        //};
                        App.Database.SaveNote(_note);
                        SetPasswordFieldToUpdate();
                    }
                    popup.Close();
                }

            }
            else
            {
                await DisplayAlert("Mentés", "A jegyzet mentéséhez adj meg címet vagy szöveget!", "OK");
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


    private void showPwdCb_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        passwordInput.IsPassword = !showPwdCb.IsChecked;
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
            passwordInput.Text = passwordInput.Text;
        }
    }

    private Task<string> CreatePassword(string oldPassword)
    {
        // nincs lejelszavazva
        if (!secretNoteCb.IsChecked)
        {
            return Task.FromResult<string>(null);
        }


        // új a jegyzet
        if (_note == null || isPasswordChanged)
        {
            return Task.Run(() =>
            {
                return Argon2.Hash(passwordInput.Text, 1, 65536, 2);
            });
        }

        return Task.FromResult(oldPassword);
    }

    private void SetSettings()
    {

        int textSize = App.Database.GetSettings().FontSize;
        int uiTextSize = App.Database.GetSettings().UIFontSize;
        string fontStyle = App.Database.GetSettings().FontFamily;

        Color textColor = Color.FromArgb(App.Database.GetSettings().TextColor);

        titleLbl.FontSize = uiTextSize;
        noteTitle.FontSize = textSize;
        seretLbl.FontSize = uiTextSize;
        showPwdLbl.FontSize = uiTextSize;
        editorLbl.FontSize = uiTextSize;
        noteEditor.FontSize = textSize;

        titleLbl.FontFamily = fontStyle;
        noteTitle.FontFamily = fontStyle;
        seretLbl.FontFamily = fontStyle;
        passwordInput.FontFamily = fontStyle;
        editorLbl.FontFamily = fontStyle;
        noteEditor.FontFamily = fontStyle;

        titleLbl.TextColor = textColor;
        noteTitle.TextColor = textColor;
        seretLbl.TextColor = textColor;
        passwordInput.TextColor = textColor;
        editorLbl.TextColor = textColor;
        noteEditor.TextColor = textColor;
    }
}
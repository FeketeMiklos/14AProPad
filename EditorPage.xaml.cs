using Konscious.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;

namespace ProPad;


public partial class EditorPage : ContentPage
{
    public EditorPage()
    {
        InitializeComponent();
        this.BindingContext = this;
        isNew = true; 
    }
    Note _note;
    bool isNew;
    bool isPasswordChanged = false;

    public EditorPage(Note note)
    {
        InitializeComponent();
        _note = note;
        noteTitle.Text = note.Title;
        noteEditor.Text = note.Text;
        noteEditor.Focus();
        secretNoteCb.IsChecked = note.IsCoded;
        isNew = false;
        if (note.IsCoded)
        {
            passwordInput.Text = "aaaaaaaa";
        }

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
                    await App.Database.UpdateNote(_note);
                }
                else
                {
                    bool isCoded = secretNoteCb.IsChecked;
                    var password = !isCoded ? null : isNew ? "" : passwordInput.Text;

                    App.Database.SaveNote(new Note
                    {
                        Title = noteTitle.Text,
                        Text = noteEditor.Text,
                        IsCoded = isCoded,
                        Password = password,
                    });
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
    }

    private void passwordInput_Focused(object sender, FocusEventArgs e)
    {
        if (!isNew)
        {
            isPasswordChanged = true;
            passwordInput.Text = "";
        }
    }
}
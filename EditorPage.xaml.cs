using Konscious.Security.Cryptography;

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
    }

    private void MakeId()
    {
        int id;
        int count = 0;
        foreach (var i in App.Database.GetAllNotes())
        {
            count++;
        }
        id = count + 1;
        
        lblId.Text = id.ToString();
    }

    private async void deleteNewNote_Clicked(object sender, EventArgs e)
    {
        bool delete = await DisplayAlert("T�rl�s", "Biztosan t�r�lni akarod a jegyzetet?", "Igen", "Nem");
        if (delete)
        {
            await Navigation.PopToRootAsync();
        }
    }

    private async void deleteNote_Clicked(object sender, EventArgs e)
    {
        bool delete = await DisplayAlert("T�rl�s", "Biztosan t�r�lni akarod a jegyzetet?", "Igen", "Nem");
        if (delete)
        {
            if (!string.IsNullOrWhiteSpace(lblId.Text))
            {
                App.Database.DeleteNote(App.Database.GetNote(int.Parse(lblId.Text)));
                lblId.Text = "";
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
        bool save = await DisplayAlert("Ment�s", "Biztosan menteni akarod a jegyzetet?", "Igen", "Nem");
        if (save)
        {
            MakeId();
            if (!string.IsNullOrWhiteSpace(noteTitle.Text) || !string.IsNullOrWhiteSpace(noteEditor.Text))
            {
                bool isCoded = secretNoteCb.IsChecked;
                var password = !isCoded ? null : isNew ? "" : passwordInput.Text;
                App.Database.SaveNote(new Note
                {
                    ID = int.Parse(lblId.Text),
                    Title = noteTitle.Text,
                    Text = noteEditor.Text,
                    IsCoded = isCoded, 
                    Password = password,
                });

            }
            else
            {
                await DisplayAlert("Ment�s", "A jegyzet ment�s�hez adj meg c�met bagy sz�veget!", "OK");
                lblId.Text = "";
            }
        }
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
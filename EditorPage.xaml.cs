namespace ProPad;

public partial class EditorPage : ContentPage
{
    public EditorPage(Note note)
    {
        InitializeComponent();
        this.BindingContext = this;
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
        bool delete = await DisplayAlert("Törlés", "Biztosan törölni akarod a jegyzetet?", "Igen", "Nem");
        if (delete)
        {
            await Navigation.PopToRootAsync();
        }
    }

    private async void deleteNote_Clicked(object sender, EventArgs e)
    {
        bool delete = await DisplayAlert("Törlés", "Biztosan törölni akarod a jegyzetet?", "Igen", "Nem");
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
        bool save = await DisplayAlert("Mentés", "Biztosan menteni akarod a jegyzetet?", "Igen", "Nem");
        if (save)
        {
            MakeId();
            if (!string.IsNullOrWhiteSpace(noteTitle.Text) || !string.IsNullOrWhiteSpace(noteEditor.Text))
            {
                App.Database.SaveNote(new Note
                {
                    ID = int.Parse(lblId.Text),
                    Title = noteTitle.Text,
                    Text = noteEditor.Text,
                    IsCoded = false, //alapból legyen false
                });
                
            }
            else
            {
                await DisplayAlert("Mentés", "A jegyzet mentéséhez adj meg címet bagy szöveget!", "OK");
                lblId.Text = "";
            }
        }
    }
}
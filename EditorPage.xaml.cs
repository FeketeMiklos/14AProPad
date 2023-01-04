namespace ProPad;

public partial class EditorPage : ContentPage
{
    public EditorPage(Note note)
	{
		InitializeComponent();
        this.BindingContext = this;
    }

	private async void deleteNewNote_Clicked(object sender, EventArgs e)
	{
        bool delete = await DisplayAlert("Törlés", "Biztosan törölni akarod a jegyzetet?", "Igen", "Nem");
		if (delete)
		{
			await Navigation.PopToRootAsync();
		}
    }

	private async void saveNote_Clicked(object sender, EventArgs e)
	{
        bool save = await DisplayAlert("Mentés", "Biztosan menteni akarod a jegyzetet?", "Igen", "Nem");
        if (save)
        {
            if (!string.IsNullOrWhiteSpace(noteTitle.Text) || !string.IsNullOrWhiteSpace(noteEditor.Text))
            {
                App.Database.SaveNote(new Note
                {
                    Title = noteTitle.Text,
                    Text = noteEditor.Text,
                    IsCoded = false, //alapból legyen false
                });

                await Navigation.PopToRootAsync();
            }
            else
            {
                await DisplayAlert("Mentés", "A jegyzet mentéséhez adj meg címet bagy szöveget!", "OK");
            }
        }
    }
}
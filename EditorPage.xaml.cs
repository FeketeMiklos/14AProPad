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
        bool delete = await DisplayAlert("T�rl�s", "Biztosan t�r�lni akarod a jegyzetet?", "Igen", "Nem");
		if (delete)
		{
			await Navigation.PopToRootAsync();
		}
    }

	private async void saveNote_Clicked(object sender, EventArgs e)
	{
        bool save = await DisplayAlert("Ment�s", "Biztosan menteni akarod a jegyzetet?", "Igen", "Nem");
        if (save)
        {
            if (!string.IsNullOrWhiteSpace(noteTitle.Text) || !string.IsNullOrWhiteSpace(noteEditor.Text))
            {
                App.Database.SaveNote(new Note
                {
                    Title = noteTitle.Text,
                    Text = noteEditor.Text,
                    IsCoded = false, //alapb�l legyen false
                });

                await Navigation.PopToRootAsync();
            }
            else
            {
                await DisplayAlert("Ment�s", "A jegyzet ment�s�hez adj meg c�met bagy sz�veget!", "OK");
            }
        }
    }
}
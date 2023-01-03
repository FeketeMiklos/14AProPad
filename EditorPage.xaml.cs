namespace ProPad;

public partial class EditorPage : ContentPage
{
	public EditorPage()
	{
		InitializeComponent();
	}

	private async void deleteNote_Clicked(object sender, EventArgs e)
	{
        bool delete = await DisplayAlert("Törlés", "Biztosan törölni akarod a jegyzetet?", "Igen", "Nem");
		if (delete)
		{
			//Ha létezik törölni
			await Navigation.PopToRootAsync();
			
		}
    }

	private void saveNote_Clicked(object sender, EventArgs e)
	{
		//menteni adatb be
	}
}
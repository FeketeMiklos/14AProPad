namespace ProPad;

public partial class EditorPage : ContentPage
{
	public EditorPage()
	{
		InitializeComponent();
	}

	private async void deleteNote_Clicked(object sender, EventArgs e)
	{
        bool delete = await DisplayAlert("T�rl�s", "Biztosan t�r�lni akarod a jegyzetet?", "Igen", "Nem");
		if (delete)
		{
			//Ha l�tezik t�r�lni
			await Navigation.PopToRootAsync();
			
		}
    }

	private void saveNote_Clicked(object sender, EventArgs e)
	{
		//menteni adatb be
	}
}
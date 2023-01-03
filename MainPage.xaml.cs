namespace ProPad;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private async void newEditor_Clicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new EditorPage());
    }
}


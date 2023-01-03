using Microsoft.Maui.Animations;

namespace ProPad;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        var notesList = new List<string>();

        notesList.Add("Első bejegyzés");
        notesList.Add("Második bejegyzés");
        notesList.Add("Harmadik bejegyzés");
        notesList.Add("Harmadik bejegyzés");
        notesList.Add("Harmadik bejegyzés");
        notesList.Add("Harmadik bejegyzés");
        notesList.Add("Harmadik bejegyzés");
        notesList.Add("Harmadik bejegyzés");

        int col = 0;
        int row = 0;

        foreach (var i in notesList)
        {
            Frame fr = new Frame()
            {
                WidthRequest = 160,
                HeightRequest = 160,
                Margin = 40,
            };

            Label lb = new Label() { Text = i };

            fr.Content = lb;

            grid.Children.Add(fr);

            grid.SetRow(fr, row);
            grid.SetColumn(fr, col);

            if (col == 0)
            {
                col = 1;
            }
            else
            {
                col = 0;
                RowDefinition rd = new RowDefinition() { Height = 200 };
                grid.RowDefinitions.Add(rd);
                row++;
            }
        }


    }

    private async void newEditor_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EditorPage());
    }

}

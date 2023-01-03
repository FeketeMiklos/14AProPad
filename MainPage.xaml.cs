using Microsoft.Maui.Animations;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System.Drawing;

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
        notesList.Add("Második");
        notesList.Add("Harmadik");
        notesList.Add("Negyedik");
        notesList.Add("Ötödik");
        notesList.Add("Hatodik");
        notesList.Add("Hatodik");
        notesList.Add("Hatodik");



        int col = 0;
        int row = 0;

        foreach (var i in notesList)
        {
            Frame fr = new Frame()
            {
                Margin = 30,
                Padding= 15,
                BackgroundColor = Microsoft.Maui.Graphics.Color.FromArgb("111111"),
            };

            Label lb = new Label() { Text = i, TextColor = Colors.White, FontSize = 16 };

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

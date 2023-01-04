using Microsoft.Maui.Animations;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using ProPad.ViewModels;
using System.Collections.ObjectModel;
using System.Drawing;

namespace ProPad;

public partial class MainPage : ContentPage
{
    NoteListModelView model;

    public MainPage()
    {
        InitializeComponent();
        model= new NoteListModelView();
    }

    protected override void OnAppearing()
    {
       base.OnAppearing();
       this.BindingContext = model;
       model.caps = new ObservableCollection<adatok>() {new adatok("Rakéta kód", "Első mondat", true)};
       clview.ItemsSource = model.caps;
    }

    private async void newEditor_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EditorPage());
    }

}

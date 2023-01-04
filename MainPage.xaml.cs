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
        model = new NoteListModelView();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        this.BindingContext = model;
        model.notes = new ObservableCollection<Note>(App.Database.GetAllNotes());
        clview.ItemsSource = model.notes;

        //var notesList = new List<string>();

        //foreach (var item in App.Database.GetAllNotes())
        //{
        //    notesList.Add($"{item.Title + "\n" + item.Text}");
        //}
    }

    private async void newEditor_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EditorPage(new Note()));
    }

}

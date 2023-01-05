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
        //model.notes = new ObservableCollection<Note> { new Note("Cím","Szöveg",false), new Note("Cím2","Szöveg2",true) };
        clview.ItemsSource = model.notes;
    }

    private async void newEditor_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EditorPage(new Note()));
    }

    private void OpenNote(object sender, EventArgs e)
    {
        
    }
}

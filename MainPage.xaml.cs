using Microsoft.Maui.Animations;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using ProPad.ViewModels;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Diagnostics;
using System.Net.WebSockets;

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
        clView.ItemsSource = model.notes;
    }

    private async void newEditor_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EditorPage());
    }

    private async void openNote(object sender, EventArgs e)
    {

        if (clView.SelectedItem != null)
        {
            Note selected = (Note)clView.SelectedItem;
            await Navigation.PushAsync(new EditorPage(selected));
        }
    }

    private async void SettingsToolbarItem_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SettingsPage());
    }
}

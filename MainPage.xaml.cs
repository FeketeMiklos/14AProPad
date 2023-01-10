using Microsoft.Maui.Animations;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using ProPad.ViewModels;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Diagnostics;
using System.Net.WebSockets;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;

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

    private async void SettingsToolbarItem_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SettingsPage());
    }

    private async  void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        var frame = sender as Frame;
        var ctx = frame.BindingContext;
        if (ctx is not Note  selected)
        {
            return;
        }

        if (selected.IsCoded && !await UnlockNote(selected))
        {
            return;
        }
        await Navigation.PushAsync(new EditorPage(selected));

    }

    private async Task<bool> UnlockNote(Note note)
    {

        var isFingerprintAvailable = await CrossFingerprint.Current.IsAvailableAsync();
        if (isFingerprintAvailable)
        {
            var config = new AuthenticationRequestConfiguration("Azonosítás", "Azonosítsd magad ujjlenyomat-olvasóval/arcfelismeréssel!") { };
            CrossFingerprint.Current.AuthenticateAsync(config);
        }

        return true;
    }
}

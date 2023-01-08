using Microsoft.Maui.Platform;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using ProPad.Data;
using System.Drawing.Text;

namespace ProPad;


public partial class SettingsPage : ContentPage
{
    private Settings settings;

    public SettingsPage()
    {
        InitializeComponent();
    }

    protected  async override void OnAppearing()
    {
        base.OnAppearing();
        settings = App.Database.GetSettings();
        FontSizeInput.Text = settings.FontSize.ToString();
        FontFamilyInput.SelectedIndex = FontFamilyInput
                                                       .Items
                                                       .ToList()
                                                       .FindIndex(font => font == settings.FontFamily);
        UpdateColorPicker();

    }
    
    private void FontSizeInput_TextChanged(object sender, TextChangedEventArgs e)
    {
        var entry = sender as Entry;
        if(!int.TryParse(entry.Text, out int fontSize))
        {
            return;
        }

        settings.FontSize = fontSize;
        App.Database.SetSettings(settings);
    }

    private void FontFamilyInput_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = sender as Picker;
        settings.FontFamily = picker.SelectedItem as string;
        App.Database.SetSettings(settings);
    }

    private void ColorOrb_Tapped(object sender, EventArgs e)
    {
        if (settings == null || sender is not Border border)
        {
            return;
        }

        settings.TextColor = border.BackgroundColor.ToHex();
        App.Database.SetSettings(settings);
        UpdateColorPicker();
    }

    private void UpdateColorPicker()
    {
        foreach (var item in ColorPickerContainer)
        {
            if (item is Border border
)
            {
                if (border.BackgroundColor.ToHex() == settings.TextColor)
                {
                    border.StrokeThickness = 3;
                }
                else
                {
                    border.StrokeThickness = 0;
                }
            }
        }
    }

}
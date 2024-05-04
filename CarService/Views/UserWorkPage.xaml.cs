using Android.AdServices.Common;
using CarService.Requests;
using CarService.ViewModels;
using CommunityToolkit.Mvvm.Messaging;
using Request_API;

namespace CarService.Views;

public partial class UserWorkPage : ContentPage
{
    public static string _title = "";
    public static Users? _model;
    public UserWorkPage()
    {
        InitializeComponent();
        pTip.ItemsSource = MauiProgram._UserTypes.Select(x => Parameters.ActiveUser?.UserType != "C" ? x.Code != "C" : x.Code == "C").ToList();
        pTip.ItemDisplayBinding = new Binding("Description");
    }

    private void chkSifre_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        txtSifre.IsEnabled = chkSifre.IsChecked;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        this.Title = _title;
        txtAd.Text = _model?.Ad ?? "";
        txtSoyad.Text = _model?.Soyad ?? "";
        txtMail.Text = _model?.Mail ?? "";
        pTip.SelectedItem = MauiProgram._UserTypes.FirstOrDefault(x => x.Code == _model?.Tip);
        txtSifre.IsEnabled = false;
        chkSifre.IsEnabled = _model?.Idno > 0m && Parameters.ActiveUser?.UserId == _model.Idno;
        chkSifre.IsChecked = false;
    }

    protected override void OnDisappearing()
    {
        _title = "";
        _model = null;
        base.OnDisappearing();
    }

    private async void Save_Clicked(object sender, EventArgs e)
    {
        if (LoadingIndicator.IsVisible)
        {
            return;
        }
        if (string.IsNullOrEmpty(txtAd.Text))
        {
            await DisplayAlert("Uyarý", "Ad girmelisiniz", "OK");
            return;
        }
        if (string.IsNullOrEmpty(txtSoyad.Text))
        {
            await DisplayAlert("Uyarý", "Soyad girmelisiniz", "OK");
            return;
        }
        if (pTip.SelectedIndex < 0)
        {
            await DisplayAlert("Uyarý", "Personel türünü seçmelisiniz", "OK");
            return;
        }
        var t = await UsersRequest.WorkUser(txtAd.Text, txtSoyad.Text, (_model?.Idno ?? 0m), (pTip.SelectedIndex == 0 ? "A" : (pTip.SelectedIndex == 1 ? "R" : "")));
        if (t.Status)
        {
            await DisplayAlert("Bilgi", "Ýþlem baþarýlý", "OK");
            await Shell.Current.GoToAsync("//AboutPage");
        }
    }
}
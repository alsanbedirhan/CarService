using Android.AdServices.Common;
using CarService.Requests;
using CarService.ViewModels;

namespace CarService.Views;

public partial class UserWorkPage : ContentPage
{
    public static string _ad = "", _soyad = "", _title = "";
    public static decimal _id = 0m;
    public UserWorkPage()
    {
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        this.Title = _title;
        txtAd.Text = _ad;
        txtSoyad.Text = _soyad;
    }
    protected override void OnDisappearing()
    {
        _title = "";
        _ad = "";
        _soyad = "";
        _id = 0m;
        this.Title = "";
        txtAd.Text = "";
        txtSoyad.Text = "";
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
        var t = await UsersRequest.WorkUser(txtAd.Text, txtSoyad.Text, _id);
        if (t.Status)
        {
            await DisplayAlert("Bilgi", "Ýþlem baþarýlý", "OK");
            await Shell.Current.GoToAsync("//AboutPage");
        }
    }
}
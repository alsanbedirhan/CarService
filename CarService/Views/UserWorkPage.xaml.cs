using Android.AdServices.Common;
using CarService.Requests;
using CarService.ViewModels;
using CommunityToolkit.Mvvm.Messaging;
using Request_API;

namespace CarService.Views;

public partial class UserWorkPage : ContentPage
{
    public static Users? _model;
    ToolbarItem? _toolbar;
    public UserWorkPage()
    {
        InitializeComponent();
        pTip.ItemsSource = MauiProgram._UserTypes.Where(x => Parameters.ActiveUser?.UserType != "C" ? x.Code != "C" : x.Code == "C").ToList();
        pTip.ItemDisplayBinding = new Binding("Description");
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (_model != null)
        {
            if (_model.Idno > 0)
            {
                this.Title = _model.Idno.ToString();
                txtAd.Text = _model.Ad;
                txtSoyad.Text = _model.Soyad;
                txtMail.Text = _model.Mail;
                pTip.SelectedItem = MauiProgram._UserTypes.FirstOrDefault(x => x.Code == _model.Tip);
            }
            else
            {
                this.Title = "Yeni Kayýt";
                txtAd.Text = "";
                txtSoyad.Text = "";
                txtMail.Text = "";
            }
            _toolbar = new ToolbarItem { Text = "GERÝ" };
            _toolbar.Clicked += Back_Clicked;
            this.ToolbarItems.Add(_toolbar);
        }
        else if (Parameters.ActiveUser != null)
        {
            this.Title = "Profilim";
            txtAd.Text = Parameters.ActiveUser.Ad;
            txtSoyad.Text = Parameters.ActiveUser.Soyad;
            txtMail.Text = Parameters.ActiveUser.Mail;
            pTip.SelectedItem = MauiProgram._UserTypes.FirstOrDefault(x => x.Code == Parameters.ActiveUser.UserType);
        }
    }

    private async void Back_Clicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//UsersPage");
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        if (_toolbar != null)
        {
            this.ToolbarItems.Remove(_toolbar);
            _toolbar.Clicked -= Back_Clicked;
        }
        _toolbar = null;
        txtAd.Text = "";
        txtSoyad.Text = "";
        txtMail.Text = "";
        _model = null;
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
        if (string.IsNullOrEmpty(txtMail.Text))
        {
            await DisplayAlert("Uyarý", "Mail girmelisiniz", "OK");
            return;
        }
        if (pTip.SelectedItem is not UserTypes types || types == null)
        {
            await DisplayAlert("Uyarý", "Kullanýcý türünü seçmelisiniz", "OK");
            return;
        }
        var t = await UsersRequest.WorkUser(txtAd.Text, txtSoyad.Text, (_model != null ? _model.Idno : (Parameters.ActiveUser != null ? Parameters.ActiveUser.UserId : 0m)),
            types.Code, txtMail.Text);
        if (t.Status)
        {
            await DisplayAlert("Bilgi", "Ýþlem baþarýlý", "OK");
            await Shell.Current.GoToAsync("//AboutPage");
        }
    }
}
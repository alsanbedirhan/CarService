using Android.AdServices.Common;
using CarService.Requests;
using CarService.ViewModels;
using CommunityToolkit.Mvvm.Messaging;
using Request_API;

namespace CarService.Views;

public partial class UserWorkPage : ContentPage
{
    public static UserInfo? _model;
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
            if (_model.UserId > 0)
            {
                this.Title = _model.UserId.ToString();
                txtAd.Text = _model.Ad;
                txtSoyad.Text = _model.Soyad;
                txtMail.Text = _model.Mail;
                txtMail.IsEnabled = false;
                pTip.SelectedItem = MauiProgram._UserTypes.FirstOrDefault(x => x.Code == _model.UserType);
            }
            else
            {
                this.Title = "Yeni Kay�t";
                txtAd.Text = "";
                txtSoyad.Text = "";
                txtMail.Text = "";
                txtMail.IsEnabled = true;
            }
            _toolbar = new ToolbarItem { Text = "GER�" };
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
            txtMail.IsEnabled = false;
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
        this.Title = "";
        txtAd.Text = "";
        txtSoyad.Text = "";
        txtMail.Text = "";
        txtMail.IsEnabled = false;
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
            await DisplayAlert("Uyar�", "Ad girmelisiniz", "OK");
            return;
        }
        if (string.IsNullOrEmpty(txtSoyad.Text))
        {
            await DisplayAlert("Uyar�", "Soyad girmelisiniz", "OK");
            return;
        }
        if (string.IsNullOrEmpty(txtMail.Text))
        {
            await DisplayAlert("Uyar�", "Mail girmelisiniz", "OK");
            return;
        }
        if (pTip.SelectedItem is not clsCombo types || types == null)
        {
            await DisplayAlert("Uyar�", "Kullan�c� t�r�n� se�melisiniz", "OK");
            return;
        }
        if (!await DisplayAlert("Soru", "Kay�t i�lemi ger�ekle�tirilecektir, devam etmek istiyor musunuz?", "YES", "NO"))
        {
            return;
        }
        var t = await UsersRequest.WorkUser(txtAd.Text, txtSoyad.Text, (_model != null ? _model.UserId :
            (Parameters.ActiveUser?.UserId ?? 0m)), types.Code, txtMail.Text);
        if (t.Status)
        {
            await DisplayAlert("Bilgi", "��lem ba�ar�l�", "OK");
            if (Parameters.ActiveUser?.UserType == "C")
            {
                await Shell.Current.GoToAsync("//AboutPage");
            }
            else
            {
                await Shell.Current.GoToAsync("//UsersPage");
            }
        }
    }
}
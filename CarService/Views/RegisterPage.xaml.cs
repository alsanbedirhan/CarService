using CommunityToolkit.Mvvm.Messaging;
using CarService.Requests;

namespace CarService.Views;

public partial class RegisterPage : ContentPage
{
    public RegisterPage()
    {
        InitializeComponent();
    }
    private async void Register_Clicked(object sender, EventArgs e)
    {
        if (LoadingIndicator.IsVisible)
        {
            return;
        }
        if (string.IsNullOrEmpty(txtName.Text))
        {
            await DisplayAlert("Uyarý", "Adýnýzý girmelisiniz", "OK");
            return;
        }
        if (string.IsNullOrEmpty(txtSurname.Text))
        {
            await DisplayAlert("Uyarý", "Soyadýnýzý girmelisiniz", "OK");
            return;
        }
        if (string.IsNullOrEmpty(txtMail.Text))
        {
            await DisplayAlert("Uyarý", "Mail adresinizi girmelisiniz", "OK");
            return;
        }
        if (string.IsNullOrEmpty(txtPsw.Text))
        {
            await DisplayAlert("Uyarý", "Þifre girmelisiniz", "OK");
            return;
        }
        if (!await DisplayAlert("Soru", "Kayýt olunacaktýr, devam etmek istiyor musunuz?", "EVET", "HAYIR"))
        {
            return;
        }
        LoadingIndicator.IsVisible = true;
        var r = new Request();
        var t = await r.Register(txtName.Text, txtSurname.Text, txtMail.Text, txtPsw.Text);
        LoadingIndicator.IsVisible = false;
        if (t.Status)
        {
            txtName.Text = "";
            txtSurname.Text = "";
            txtMail.Text = "";
            txtPsw.Text = "";
            chkPsw.IsChecked = false;
            WeakReferenceMessenger.Default.Send("LOGINOK");
        }
    }

    private void chkPsw_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        txtPsw.IsPassword = !chkPsw.IsChecked;
    }
}
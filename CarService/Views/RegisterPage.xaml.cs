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
            await DisplayAlert("Uyar�", "Ad�n�z� girmelisiniz", "OK");
            return;
        }
        if (string.IsNullOrEmpty(txtSurname.Text))
        {
            await DisplayAlert("Uyar�", "Soyad�n�z� girmelisiniz", "OK");
            return;
        }
        if (string.IsNullOrEmpty(txtMail.Text))
        {
            await DisplayAlert("Uyar�", "Mail adresinizi girmelisiniz", "OK");
            return;
        }
        if (string.IsNullOrEmpty(txtPsw.Text))
        {
            await DisplayAlert("Uyar�", "�ifre girmelisiniz", "OK");
            return;
        }
        if (!await DisplayAlert("Soru", "Kay�t olunacakt�r, devam etmek istiyor musunuz?", "EVET", "HAYIR"))
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
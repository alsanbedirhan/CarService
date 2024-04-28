using CommunityToolkit.Mvvm.Messaging;
using CarService.Requests;

namespace CarService.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }
    private async void Login_Clicked(object sender, EventArgs e)
    {
        if (LoadingIndicator.IsVisible)
        {
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
        LoadingIndicator.IsVisible = true;
        var r = new Request();
        var t = await r.Login(txtMail.Text, txtPsw.Text);
        LoadingIndicator.IsVisible = false;
        if (t.Status)
        {
            txtMail.Text = "";
            txtPsw.Text = "";
            chkPsw.IsChecked = false;
            WeakReferenceMessenger.Default.Send("LOGINOK");
        }
        //else if (t.StringValue1.StartsWith("VERSION:") && await DisplayAlert("Soru", "Uygulaman�nz g�ncel de�ildir, g�ncellemek istiyor musunuz?", "Evet", "Hay�r"))
        //{
        //    await Launcher.OpenAsync(GlobalValues.BaseURL + "update/file?token=" + t.StringValue1.Remove(0, 8));
        //}
    }

    private void Register_Clicked(object sender, EventArgs e)
    {
        if (LoadingIndicator.IsVisible)
        {
            return;
        }
        WeakReferenceMessenger.Default.Send("NEEDREGISTER");
    }

    private void chkPsw_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        txtPsw.IsPassword = !chkPsw.IsChecked;
    }
}
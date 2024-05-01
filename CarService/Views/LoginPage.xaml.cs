using CommunityToolkit.Mvvm.Messaging;
using CarService.Requests;
using Request_API;

namespace CarService.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }
    private void Login_Clicked(object sender, EventArgs e)
    {
        Login();
    }

    async void Login()
    {
        if (LoadingIndicator.IsVisible)
        {
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
        LoadingIndicator.IsVisible = true;
        var t = await LoginRequest.Login(txtMail.Text, txtPsw.Text);
        LoadingIndicator.IsVisible = false;
        if (t.Status)
        {
            txtMail.Text = "";
            txtPsw.Text = "";
            chkPsw.IsChecked = false;
            Parameters.ActiveUser = t.Data;
            WeakReferenceMessenger.Default.Send("LOGINOK");
        }
        //else if (t.StringValue1.StartsWith("VERSION:") && await DisplayAlert("Soru", "Uygulamanýnz güncel deðildir, güncellemek istiyor musunuz?", "Evet", "Hayýr"))
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
        txtMail.Text = "";
        txtPsw.Text = "";
        chkPsw.IsChecked = false;
        WeakReferenceMessenger.Default.Send("NEEDREGISTER");
    }

    private void chkPsw_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        txtPsw.IsPassword = !chkPsw.IsChecked;
    }

    private void txtPsw_Completed(object sender, EventArgs e)
    {
        Login();
    }
}
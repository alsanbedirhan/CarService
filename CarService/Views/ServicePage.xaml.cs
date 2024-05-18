using CarService.Requests;
using CarService.ViewModels;
using CommunityToolkit.Maui.Views;
using Request_API;

namespace CarService.Views;

public partial class ServicePage : ContentPage
{
    ServiceViewModel viewModel;
    public ServicePage()
    {
        InitializeComponent();
        viewModel = new ServiceViewModel();
        BindingContext = viewModel;
    }
    protected override void OnDisappearing()
    {
        viewModel = new ServiceViewModel();
        BindingContext = viewModel;
        base.OnDisappearing();
    }
    private async void btnUserCar_Clicked(object sender, EventArgs e)
    {
        if (viewModel.UserId <= 0)
        {
            return;
        }
        SearchUserCar.UserId = viewModel.UserId;
        var popup = new SearchUserCar();
        var result = await this.ShowPopupAsync(popup);
        if (result != null && result is SearchUserCarList search && search != null)
        {
            viewModel.MarkaModelPlaka = search.MarkaModel + " - " + search.Plaka;
            viewModel.UserCarId = search.Idno;
        }
    }

    private async void btnUser_Clicked(object sender, EventArgs e)
    {
        var popup = new SearchUser();
        var result = await this.ShowPopupAsync(popup);
        if (result != null && result is SearchUserList search && search != null)
        {
            viewModel.UserId = search.Idno;
            viewModel.AdSoyad = search.AdSoyad;
        }
    }

    private async void Save_Clicked(object sender, EventArgs e)
    {
        if (LoadingIndicator.IsVisible)
        {
            return;
        }
        if (viewModel.UserCarId <= 0)
        {
            await DisplayAlert("Uyarý", "Araç seçmelisiniz", "OK");
            return;
        }
        if (string.IsNullOrEmpty(viewModel.Aciklama))
        {
            await DisplayAlert("Uyarý", "Açýklama girmelisiniz", "OK");
            return;
        }
        LoadingIndicator.IsVisible = true;
        var s = await ServiceRequests.Save(viewModel.UserCarId, viewModel.Aciklama);
        LoadingIndicator.IsVisible = false;
        if (s.Status)
        {
            await DisplayAlert("Bilgi", "Ýþlem baþarýlý", "OK");
            await Shell.Current.GoToAsync("//AboutPage");
        }
    }
}
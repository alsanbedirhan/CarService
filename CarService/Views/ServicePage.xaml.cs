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
    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        if (width > height)
        {
            //view.ItemsLayout = new GridItemsLayout(2, ItemsLayoutOrientation.Vertical);
            sfilter.Orientation = StackOrientation.Horizontal;
        }
        else
        {
            //view.ItemsLayout = new GridItemsLayout(1, ItemsLayoutOrientation.Vertical);
            sfilter.Orientation = StackOrientation.Vertical;
        }
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        //MainThread.BeginInvokeOnMainThread(async () =>
        //{
        //    await SendRequest();
        //});
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
        var popup = new SearchUserCar();
        popup.UserId = viewModel.UserId;
        var result = await this.ShowPopupAsync(popup);
        if (result != null)
        {

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
}
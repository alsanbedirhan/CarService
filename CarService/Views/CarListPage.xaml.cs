using CarService.Requests;
using CarService.ViewModels;

namespace CarService.Views;

public partial class CarListPage : ContentPage
{
    CarListViewModel viewModel;
    public CarListPage()
    {
		InitializeComponent();
        viewModel = new CarListViewModel();
        BindingContext = viewModel;
    }
    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        if (width > height)
        {
            view.ItemsLayout = new GridItemsLayout(2, ItemsLayoutOrientation.Vertical);
            sfilter.Orientation = StackOrientation.Horizontal;
        }
        else
        {
            view.ItemsLayout = new GridItemsLayout(1, ItemsLayoutOrientation.Vertical);
            sfilter.Orientation = StackOrientation.Vertical;
        }
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await SendRequest();
        });
    }

    async Task SendRequest()
    {
        if (LoadingIndicator.IsVisible)
        {
            return;
        }
        viewModel.ListSource.Clear();
        LoadingIndicator.IsVisible = true;
        var t = await CarListRequests.AllUser(txtMarka?.Text?.Trim() ?? "", txtModel?.Text?.Trim() ?? "");
        LoadingIndicator.IsVisible = false;
        if (t.Status)
        {
            t.Data.ForEach(viewModel.ListSource.Add);
        }
    }

    protected override void OnDisappearing()
    {
        viewModel = new CarListViewModel();
        BindingContext = viewModel;
        base.OnDisappearing();
    }
    private void Search_Clicked(object sender, EventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await SendRequest();
        });
    }

    private async void Add_Clicked(object sender, EventArgs e)
    {
        UserWorkPage._model = new Users();
        await Shell.Current.GoToAsync("//UserWorkPage");
    }

    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection == null || !e.CurrentSelection.Any() || e.CurrentSelection[0] is not Users s)
        {
            return;
        }
        UserWorkPage._model = s;
        await Shell.Current.GoToAsync("//UserWorkPage");
    }
}
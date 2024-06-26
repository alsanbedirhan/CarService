using CarService.Requests;
using CarService.ViewModels;
using CommunityToolkit.Maui.Views;
using Request_API;

namespace CarService.Views;

public partial class CarListPage : ContentPage
{
    public static decimal UserId = 0;
    CarListViewModel viewModel;
    public CarListPage()
    {
        InitializeComponent();
        viewModel = new CarListViewModel();
        BindingContext = viewModel;
        pkModel.ItemDisplayBinding = new Binding("DisplayValue");
        pkMarka.ItemDisplayBinding = new Binding("DisplayValue");
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
        if (Parameters.ActiveUser?.UserType == "C")
        {
            UserId = Parameters.ActiveUser.UserId;
        }
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await SendRequest();
            pkMarka.ItemsSource = await CarListRequests.GetMakes();
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
        var t = await CarListRequests.Cars(UserId, (pkMarka.SelectedItem is clsSearch marka && marka != null ? marka.Key : 0m),
            (pkModel.SelectedItem is clsSearch model && model != null ? model.Key : 0m));
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
        UserId = 0;
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
        await Shell.Current.GoToAsync("//CarWorkPage");
    }

    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection == null || !e.CurrentSelection.Any() || e.CurrentSelection[0] is not Cars s)
        {
            return;
        }
        CarWorkPage._model = s;
        await Shell.Current.GoToAsync("//CarWorkPage");
    }

    //private async void btnModel_Clicked(object sender, EventArgs e)
    //{
    //    if (!MakeIds.Any())
    //    {
    //        return;
    //    }
    //    LoadingIndicator.IsVisible = true;
    //    var data = await CarListRequests.GetMakeModels(MakeIds);
    //    LoadingIndicator.IsVisible = false;
    //    var popup = new SearchPopUp(data, SelectionMode.Multiple, MakeModelIds);
    //    var result = await this.ShowPopupAsync(popup);
    //    if (result != null && result is List<clsSearch> search)
    //    {
    //        MakeModelIds = search.Select(x => x.Key).ToList();
    //        //txtModels.Text = string.Join(",", search.Select(x => x.DisplayValue));
    //    }
    //}

    //private async void btnMarka_Clicked(object sender, EventArgs e)
    //{
    //    LoadingIndicator.IsVisible = true;
    //    var data = await CarListRequests.GetMakes();
    //    LoadingIndicator.IsVisible = false;
    //    var popup = new SearchPopUp(data, SelectionMode.Multiple, MakeIds);
    //    var result = await this.ShowPopupAsync(popup);
    //    if (result != null && result is List<clsSearch> search)
    //    {
    //        MakeIds = search.Select(x => x.Key).ToList();
    //        //txtMarkas.Text = string.Join(",", search.Select(x => x.DisplayValue));
    //        MakeModelIds = new List<decimal>();
    //        //txtModels.Text = "";
    //    }
    //}

    private async void pkMarka_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (pkMarka.SelectedItem is not clsSearch search || search == null)
        {
            return;
        }
        pkModel.ItemsSource = await CarListRequests.GetMakeModels(new List<decimal> { search.Key });
    }

    private void Clear_Clicked(object sender, EventArgs e)
    {
        pkModel.SelectedIndex = -1;
        pkMarka.SelectedIndex = -1;
        pkModel.ItemsSource = new List<clsSearch>();
    }
}
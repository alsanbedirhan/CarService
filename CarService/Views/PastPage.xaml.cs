using CarService.Requests;
using CarService.ViewModels;
using Request_API;

namespace CarService.Views;

public partial class PastPage : ContentPage
{
    CompanyWorkListViewModel viewModel;
    public PastPage()
    {
        InitializeComponent();
        viewModel = new CompanyWorkListViewModel();
        BindingContext = viewModel;
        pkModel.ItemDisplayBinding = new Binding("DisplayValue");
        pkMarka.ItemDisplayBinding = new Binding("DisplayValue");
        dtStart.MaximumDate = DateTime.Today;
        dtEnd.MaximumDate = DateTime.Today;
    }
    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        if (width > height)
        {
            view.ItemsLayout = new GridItemsLayout(2, ItemsLayoutOrientation.Vertical);
            sfilterC.Orientation = StackOrientation.Horizontal;
            sfilterU.Orientation = StackOrientation.Horizontal;
        }
        else
        {
            view.ItemsLayout = new GridItemsLayout(1, ItemsLayoutOrientation.Vertical);
            sfilterC.Orientation = StackOrientation.Vertical;
            sfilterU.Orientation = StackOrientation.Vertical;
        }
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        dtStart.Date = DateTime.Today.AddDays(-7);
        dtEnd.Date = DateTime.Today;
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
        var t = await ServiceRequests.SearchCompanyWork((pkMarka.SelectedItem is clsSearch marka && marka != null ? marka.Key : 0m),
            (pkModel.SelectedItem is clsSearch model && model != null ? model.Key : 0m), "", "", "", "", "", dtStart.Date, dtEnd.Date);
        LoadingIndicator.IsVisible = false;
        if (t.Status)
        {
            t.Data.ForEach(viewModel.ListSource.Add);
        }
    }
    private void Search_Clicked(object sender, EventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await SendRequest();
        });
    }
    private void Clear_Clicked(object sender, EventArgs e)
    {
        pkModel.SelectedIndex = -1;
        pkMarka.SelectedIndex = -1;
        pkModel.ItemsSource = new List<clsSearch>();
        dtStart.Date = DateTime.Today.AddDays(-7);
        dtEnd.Date = DateTime.Today;
    }
    protected override void OnDisappearing()
    {
        viewModel = new CompanyWorkListViewModel();
        BindingContext = viewModel;
        pkModel.SelectedIndex = -1;
        pkMarka.SelectedIndex = -1;
        pkModel.ItemsSource = new List<clsSearch>();
        base.OnDisappearing();
    }
    private async void pkMarka_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (pkMarka.SelectedItem is not clsSearch search || search == null)
        {
            return;
        }
        pkModel.ItemsSource = await CarListRequests.GetMakeModels(new List<decimal> { search.Key });
    }
    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        viewModel.ListSource.ToList().ForEach(x => x.BColor = Color.FromArgb("#F0F8FF"));
        if (view.SelectedItem is SearchCompanyWorkList detail && detail != null)
        {
            detail.BColor = Color.FromArgb("#FF9400");
        }
    }
}
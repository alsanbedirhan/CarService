using CarService.Requests;
using CommunityToolkit.Maui.Views;
using Request_API;

namespace CarService.Views;

public partial class SearchCompanyWork : Popup
{
    public SearchCompanyWork()
    {
        InitializeComponent();
        pkModel.ItemDisplayBinding = new Binding("DisplayValue");
        pkMarka.ItemDisplayBinding = new Binding("DisplayValue");
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await SendRequest();
            pkMarka.ItemsSource = await CarListRequests.GetMakes();
        });
    }
    async Task SendRequest()
    {
        var list = new List<SearchCompanyWorkList>();
        LoadingIndicator.IsVisible = true;
        var t = await ServiceRequests.SearchCompanyWork(pkMarka.SelectedItem is clsSearch marka && marka != null ? marka.Key : 0m,
            pkModel.SelectedItem is clsSearch model && model != null ? model.Key : 0, txtPlaka.Text, txtAd.Text, txtSoyad.Text);
        LoadingIndicator.IsVisible = false;
        if (t.Status)
        {
            list.AddRange(t.Data);
        }
        ItemsCollectionView.ItemsSource = list;
    }
    private void Ara_Clicked(object sender, EventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(async () => await SendRequest());
    }
    private void ItemsCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Close(e.CurrentSelection.FirstOrDefault());
    }

    private async void pkMarka_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (pkMarka.SelectedItem is not clsSearch search || search == null)
        {
            return;
        }
        pkModel.ItemsSource = await CarListRequests.GetMakeModels(new List<decimal> { search.Key });
    }
}
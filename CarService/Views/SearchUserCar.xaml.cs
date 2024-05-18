using CarService.Requests;
using CommunityToolkit.Maui.Views;

namespace CarService.Views;

public partial class SearchUserCar : Popup
{
    public decimal UserId = 0;
    public SearchUserCar()
    {
        InitializeComponent();
    }
    async Task SendRequest()
    {
        var list = new List<SearchUserCarList>();
        LoadingIndicator.IsVisible = true;
        var t = await ServiceRequests.SearchUserCar(UserId, pkMarka.SelectedItem is clsSearch marka && marka != null ? marka.Key : 0m,
            pkModel.SelectedItem is clsSearch model && model != null ? model.Key : 0, txtPlaka.Text,
            short.TryParse(txtYil.Text, out short yil) && yil > 1900 && yil < 3000 ? yil : Convert.ToInt16(0));
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

    private async void btnEkle_Clicked(object sender, EventArgs e)
    {
        if (LoadingIndicator.IsVisible || string.IsNullOrEmpty(txtPlaka.Text?.Trim()) || string.IsNullOrEmpty(txtYil.Text?.Trim())
            || !short.TryParse(txtYil.Text, out short yil) || yil <= 1900 || yil > 3000 || pkModel.SelectedItem is not clsSearch search || search == null)
        {
            return;
        }
        LoadingIndicator.IsVisible = true;
        var r = await CarRequests.WorkCar(0, UserId, search.Key, txtPlaka.Text, yil);
        LoadingIndicator.IsVisible = false;
        if (r.Status)
        {
            MainThread.BeginInvokeOnMainThread(async () => await SendRequest());
        }
    }

    private void ItemsCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Close(e.CurrentSelection.FirstOrDefault());
    }
}
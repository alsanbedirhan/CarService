using CarService.Requests;
using CommunityToolkit.Maui.Views;

namespace CarService.Views;

public partial class SearchUser : Popup
{
    public SearchUser()
    {
        InitializeComponent();
    }
    async Task SendRequest()
    {
        var list = new List<SearchUserList>();
        LoadingIndicator.IsVisible = true;
        var t = await ServiceRequests.SearchUser(txtAd?.Text?.Trim() ?? "", txtSoyad?.Text?.Trim() ?? "", txtMail.Text?.Trim() ?? "");
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
        if (LoadingIndicator.IsVisible || string.IsNullOrEmpty(txtAd.Text?.Trim()) || string.IsNullOrEmpty(txtSoyad.Text?.Trim()) || string.IsNullOrEmpty(txtMail.Text?.Trim()))
        {
            return;
        }
        LoadingIndicator.IsVisible = true;
        var r = await UsersRequest.WorkUser(txtAd.Text, txtSoyad.Text, 0, "C", txtMail.Text);
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

    private void text_Completed(object sender, EventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(async () => await SendRequest());
    }
}
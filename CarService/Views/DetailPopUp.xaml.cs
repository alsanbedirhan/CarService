using CarService.Requests;
using CarService.ViewModels;
using CommunityToolkit.Maui.Views;

namespace CarService.Views;

public partial class DetailPopUp : Popup
{
    decimal _HeaderId = 0;
    public DetailPopUp(decimal HeaderId)
    {
        InitializeComponent();
        _HeaderId = HeaderId;
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await SendRequest();
        });
    }
    async Task SendRequest()
    {
        if (_HeaderId <= 0)
        {
            Close();
            return;
        }
        List<CompanyWorkDetail> list = new List<CompanyWorkDetail>();
        LoadingIndicator.IsVisible = true;
        var r = await CompanyWorkRequests.Detail(_HeaderId);
        LoadingIndicator.IsVisible = false;
        if (r.Status)
        {
            list.AddRange(r.Data);
        }

        ItemsCollectionView.ItemsSource = list;
    }
}
using AndroidX.Lifecycle;
using CarService.Requests;
using CarService.ViewModels;

namespace CarService.Views;

public partial class UsersPage : ContentPage
{
    UsersViewModel viewModel;
    List<Users> alldata = new List<Users>();
    public UsersPage()
    {
        InitializeComponent();
        viewModel = new UsersViewModel();
        BindingContext = viewModel;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        alldata = new List<Users>();
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
        var t = await UsersRequest.AllUser(sbAd?.Text?.Trim() ?? "", sbSoyad?.Text?.Trim() ?? "");
        LoadingIndicator.IsVisible = false;
        if (t.Status)
        {
            t.Data.ForEach(viewModel.ListSource.Add);
            alldata = viewModel.ListSource.ToList();
        }
    }

    protected override void OnDisappearing()
    {
        viewModel = new UsersViewModel();
        BindingContext = viewModel;
        alldata = new List<Users>();
        base.OnDisappearing();
    }

    private void Yenile_Clicked(object sender, EventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await SendRequest();
        });
    }

    private async void Add_Clicked(object sender, EventArgs e)
    {
        UserWorkPage._title = "Yeni Personel";
        await Shell.Current.GoToAsync("//UserWorkPage");
    }

    private void Ad_SearchButtonPressed(object sender, EventArgs e)
    {
        viewModel.ListSource.Clear();
        alldata.Where(x => x.Ad.ToLower().Contains(sbAd?.Text?.ToLower().Trim() ?? "")).ToList().ForEach(viewModel.ListSource.Add);
    }

    private void Soyad_SearchButtonPressed(object sender, EventArgs e)
    {
        viewModel.ListSource.Clear();
        alldata.Where(x => x.Soyad.ToLower().Contains(sbSoyad?.Text?.ToLower().Trim() ?? "", StringComparison.OrdinalIgnoreCase)).ToList().ForEach(viewModel.ListSource.Add);
    }

    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection == null || !e.CurrentSelection.Any() || e.CurrentSelection[0] is not Users s)
        {
            return;
        }
        UserWorkPage._ad = s.Ad;
        UserWorkPage._soyad = s.Soyad;
        UserWorkPage._id = s.Idno;
        UserWorkPage._title = s.Idno.ToString();
        await Shell.Current.GoToAsync("//UserWorkPage");
    }
}
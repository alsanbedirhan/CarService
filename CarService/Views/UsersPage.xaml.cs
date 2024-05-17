using AndroidX.Lifecycle;
using CarService.Requests;
using CarService.ViewModels;
using Microsoft.Maui.Controls;
using Request_API;

namespace CarService.Views;

public partial class UsersPage : ContentPage
{
    UsersViewModel viewModel;
    public UsersPage()
    {
        InitializeComponent();
        viewModel = new UsersViewModel();
        BindingContext = viewModel;
        pTip.ItemsSource = new List<clsCombo>
        {
            new clsCombo { Code = "", Description = "Hepsi" },
            new clsCombo { Code = "A", Description = "Yetkili" },
            new clsCombo { Code = "R", Description = "Normal" }
        };
        pTip.ItemDisplayBinding = new Binding("Description");
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
        pTip.SelectedIndex = 0;
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await SendRequest();
        });
    }

    async Task SendRequest()
    {
        if (LoadingIndicator.IsVisible || pTip.SelectedItem is not clsCombo combo || combo == null)
        {
            return;
        }
        viewModel.ListSource.Clear();
        LoadingIndicator.IsVisible = true;
        var t = await UsersRequest.AllUser(txtAd?.Text?.Trim() ?? "", txtSoyad?.Text?.Trim() ?? "", combo.Code);
        LoadingIndicator.IsVisible = false;
        if (t.Status)
        {
            t.Data.ForEach(viewModel.ListSource.Add);
        }
    }

    protected override void OnDisappearing()
    {
        viewModel = new UsersViewModel();
        BindingContext = viewModel;
        txtAd.Text = "";
        txtSoyad.Text = "";
        pTip.SelectedIndex = 0;
        base.OnDisappearing();
    }

    //private void Yenile_Clicked(object sender, EventArgs e)
    //{

    //}

    private async void Add_Clicked(object sender, EventArgs e)
    {
        UserWorkPage._model = new Users();
        await Shell.Current.GoToAsync("//UserWorkPage");
    }

    //private void Ad_SearchButtonPressed(object sender, EventArgs e)
    //{
    //    viewModel.ListSource.Clear();
    //    alldata.Where(x => x.Ad.ToLower().Contains(sbAd?.Text?.ToLower().Trim() ?? "")).ToList().ForEach(viewModel.ListSource.Add);
    //}

    //private void Soyad_SearchButtonPressed(object sender, EventArgs e)
    //{
    //    viewModel.ListSource.Clear();
    //    alldata.Where(x => x.Soyad.ToLower().Contains(sbSoyad?.Text?.ToLower().Trim() ?? "", StringComparison.OrdinalIgnoreCase)).ToList().ForEach(viewModel.ListSource.Add);
    //}

    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection == null || !e.CurrentSelection.Any() || e.CurrentSelection[0] is not Users s)
        {
            return;
        }
        UserWorkPage._model = s;
        await Shell.Current.GoToAsync("//UserWorkPage");
    }

    //private void pTip_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (pTip.SelectedIndex < 0)
    //    {
    //        return;
    //    }
    //    viewModel.ListSource.Clear();
    //    alldata.Where(x => (pTip.SelectedIndex == 1 ? x.Tip == "A" : (pTip.SelectedIndex == 2 ? x.Tip == "R" : true))).ToList().ForEach(viewModel.ListSource.Add);
    //}

    private void Search_Clicked(object sender, EventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await SendRequest();
        });
    }
}
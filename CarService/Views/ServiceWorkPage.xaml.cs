using AndroidX.Lifecycle;
using CarService.Requests;
using CarService.ViewModels;
using CommunityToolkit.Maui.Views;
using Request_API;

namespace CarService.Views;

public partial class ServiceWorkPage : ContentPage
{
    CompanyWorkViewModel viewModel;
    public ServiceWorkPage()
    {
        InitializeComponent();
        viewModel = new CompanyWorkViewModel();
        BindingContext = viewModel;
    }
    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        if (width > height)
        {
            view.ItemsLayout = new GridItemsLayout(2, ItemsLayoutOrientation.Vertical);
        }
        else
        {
            view.ItemsLayout = new GridItemsLayout(1, ItemsLayoutOrientation.Vertical);
        }
    }
    protected override void OnDisappearing()
    {
        viewModel = new CompanyWorkViewModel();
        BindingContext = viewModel;
        base.OnDisappearing();
    }
    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        viewModel.ListSource.ToList().ForEach(x => x.BColor = Color.FromArgb("#F0F8FF"));
        if (view.SelectedItem is CompanyWorkDetail detail && detail != null)
        {
            detail.BColor = Color.FromArgb("#FF9400");
        }
    }
    async Task SendRequest()
    {
        viewModel.ClearRows();
        if (viewModel.CompanyWorkId <= 0)
        {
            return;
        }
        LoadingIndicator.IsVisible = true;
        var r = await CompanyWorkRequests.Detail(viewModel.CompanyWorkId);
        LoadingIndicator.IsVisible = false;
        if (r.Status)
        {
            r.Data.ForEach(viewModel.AddRow);
        }
    }
    private async void Sec_Clicked(object sender, EventArgs e)
    {
        viewModel.ClearRows();
        var popup = new SearchCompanyWork();
        var result = await this.ShowPopupAsync(popup);
        if (result != null && result is SearchCompanyWorkList search && search != null)
        {
            viewModel.AdSoyad = search.AdSoyad;
            viewModel.AracBilgi = search.MarkaModel + " - " + search.Plaka;
            viewModel.CompanyWorkId = search.Idno;
            await SendRequest();
        }
    }

    private async void Ekle_Clicked(object sender, EventArgs e)
    {
        if (viewModel.CompanyWorkId <= 0 || LoadingIndicator.IsVisible || string.IsNullOrEmpty(txtAciklama.Text?.Trim()) || string.IsNullOrEmpty(txtPrice.Text?.Trim()) ||
            !decimal.TryParse(txtPrice.Text, out decimal price) || price < 0 ||
            !(await DisplayAlert("Soru", "Kayýt iþlemi gerçekleþtirilecektir, devam etmek istiyor musunuz?", "YES", "NO")))
        {
            return;
        }
        LoadingIndicator.IsVisible = true;
        var r = await CompanyWorkRequests.AddDetail(viewModel.CompanyWorkId, price, txtAciklama.Text);
        LoadingIndicator.IsVisible = false;
        if (r.Status)
        {
            txtAciklama.Text = "";
            txtPrice.Text = "";
            await DisplayAlert("Bilgi", "Ýþlem baþarýlý", "OK");
            await SendRequest();
        }
    }

    private async void Cikar_Clicked(object sender, EventArgs e)
    {
        if (LoadingIndicator.IsVisible || view.SelectedItem is not CompanyWorkDetail detail || detail == null ||
            !(await DisplayAlert("Soru", "Seçil kayýt silinecektir, devam etmek istiyor musunuz?", "YES", "NO")))
        {
            return;
        }
        if (detail.Cuser != Parameters.ActiveUser?.UserId && Parameters.ActiveUser?.UserType != "A")
        {
            await DisplayAlert("Uyarý", "Seçili kayýt sadece kayýt eden kullanýcý ya da yetkili kullanýcý tarafýndan silinebilir.", "OK");
            return;
        }
        LoadingIndicator.IsVisible = true;
        var r = await CompanyWorkRequests.DeleteDetail(detail.Idno);
        LoadingIndicator.IsVisible = false;
        if (r.Status)
        {
            txtAciklama.Text = "";
            txtPrice.Text = "";
            await DisplayAlert("Bilgi", "Ýþlem baþarýlý", "OK");
            await SendRequest();
        }
    }

    private async void OK_Clicked(object sender, EventArgs e)
    {
        if (LoadingIndicator.IsVisible || !(await DisplayAlert("Soru", "Seçil aracýn iþlemi sonlandýrýlacaktýr, devam etmek istiyor musunuz?", "YES", "NO")))
        {
            return;
        }
    }
}
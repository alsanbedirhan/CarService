using CarService.Requests;
using CarService.ViewModels;
using CommunityToolkit.Maui.Views;
using Request_API;

namespace CarService.Views;

public partial class CarWorkPage : ContentPage
{
    public static Cars _model = new Cars();
    public CarWorkPage()
    {
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (_model.Idno > 0)
        {
            this.Title = _model.Idno.ToString();
        }
        else
        {
            this.Title = "Yeni Kayýt";
        }
        if (Parameters.ActiveUser?.UserType == "C")
        {
            _model.UserId = Parameters.ActiveUser.UserId;
        }
        this.Title = _model.Idno > 0 ? _model.Idno.ToString() : "Yeni Kayýt";
        txtMarka.Text = _model.Marka;
        txtModel.Text = _model.Model;
        txtPlaka.Text = _model.Plaka;
        txtYil.Text = _model.Yil.ToString();
        //_toolbar = new ToolbarItem { Text = "GERÝ" };
        //_toolbar.Clicked += Back_Clicked;
        //this.ToolbarItems.Add(_toolbar);
    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        //if (_toolbar != null)
        //{
        //    this.ToolbarItems.Remove(_toolbar);
        //    _toolbar.Clicked -= Back_Clicked;
        //}
        //_toolbar = null;
        txtMarka.Text = "";
        txtModel.Text = "";
        txtPlaka.Text = "";
        txtYil.Text = "";
        _model = new Cars();
    }

    private async void btnModel_Clicked(object sender, EventArgs e)
    {
        if (_model.MarkaId > 0)
        {
            var data = await CarListRequests.GetMakeModels(new List<decimal> { _model.MarkaId });
            var popup = new SearchPopUp(data, SelectionMode.Single);
            var result = await this.ShowPopupAsync(popup);
            if (result != null && result is List<clsSearch> search && search.Any())
            {
                _model.ModelId = search[0].Key;
                txtModel.Text = search[0].DisplayValue;
                _model.Model = search[0].DisplayValue;
            }
        }
    }

    private async void btnMarka_Clicked(object sender, EventArgs e)
    {
        LoadingIndicator.IsVisible = true;
        var data = await CarListRequests.GetMakes();
        LoadingIndicator.IsVisible = false;
        var popup = new SearchPopUp(data, SelectionMode.Single);
        var result = await this.ShowPopupAsync(popup);
        if (result != null && result is List<clsSearch> search && search.Any())
        {
            _model.MarkaId = search[0].Key;
            txtMarka.Text = search[0].DisplayValue;
            _model.Marka = search[0].DisplayValue;
            txtModel.Text = "";
            _model.ModelId = 0;
            _model.Model = "";
            //txtModels.Text = string.Join(",", search.Select(x => x.DisplayValue));
        }
    }
    private async void Save_Clicked(object sender, EventArgs e)
    {
        if (LoadingIndicator.IsVisible)
        {
            return;
        }
        if (string.IsNullOrEmpty(txtMarka.Text))
        {
            await DisplayAlert("Uyarý", "Marka seçmelisiniz", "OK");
            return;
        }
        if (string.IsNullOrEmpty(txtModel.Text))
        {
            await DisplayAlert("Uyarý", "Model seçmelisiniz", "OK");
            return;
        }
        if (Parameters.ActiveUser?.UserType != "C")
        {
            if (string.IsNullOrEmpty(txtPlaka.Text))
            {
                await DisplayAlert("Uyarý", "Plaka girmelisiniz", "OK");
                return;
            }
            if (!int.TryParse(txtYil.Text, out int yil) || yil <= 1900)
            {
                await DisplayAlert("Uyarý", "Yýl girmelisiniz", "OK");
                return;
            }
        }
        if (!await DisplayAlert("Soru", "Kayýt iþlemi gerçekleþtirilecektir, devam etmek istiyor musunuz?", "YES", "NO"))
        {
            return;
        }
        var t = await CarRequests.WorkCar(_model.Idno, _model.UserId, _model.ModelId, txtPlaka.Text, txtYil.Text);
        if (t.Status)
        {
            await DisplayAlert("Bilgi", "Ýþlem baþarýlý", "OK");
            await Shell.Current.GoToAsync("//AboutPage");
        }
    }
}
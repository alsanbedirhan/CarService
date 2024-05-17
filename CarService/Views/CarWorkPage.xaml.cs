using CarService.Requests;
using CarService.ViewModels;
using Java.Lang.Reflect;
using Request_API;

namespace CarService.Views;

public partial class CarWorkPage : ContentPage
{
    public static Cars? _model;
    public CarWorkPage()
    {
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (_model != null)
        {
            this.Title = _model.Idno.ToString();
            txtMarka.Text = _model.Marka;
            txtModel.Text = _model.Model;
            txtPlaka.Text = _model.Plaka;
            txtYil.Text = _model.Yil.ToString();
            //_toolbar = new ToolbarItem { Text = "GER�" };
            //_toolbar.Clicked += Back_Clicked;
            //this.ToolbarItems.Add(_toolbar);
        }
        else
        {
            this.Title = "Yeni Kay�t";
            txtMarka.Text = "";
            txtModel.Text = "";
            txtPlaka.Text = "";
            txtYil.Text = "";
            _model = new Cars();
        }
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
        _model = null;
    }

    private void btnModel_Clicked(object sender, EventArgs e)
    {
        _model.ModelId = 0m;
    }

    private void btnMarka_Clicked(object sender, EventArgs e)
    {

    }
    private async void Save_Clicked(object sender, EventArgs e)
    {
        if (LoadingIndicator.IsVisible)
        {
            return;
        }
        if (string.IsNullOrEmpty(txtMarka.Text))
        {
            await DisplayAlert("Uyar�", "Marka se�melisiniz", "OK");
            return;
        }
        if (string.IsNullOrEmpty(txtModel.Text))
        {
            await DisplayAlert("Uyar�", "Model se�melisiniz", "OK");
            return;
        }
        if (Parameters.ActiveUser?.UserType != "C")
        {
            if (string.IsNullOrEmpty(txtPlaka.Text))
            {
                await DisplayAlert("Uyar�", "Plaka girmelisiniz", "OK");
                return;
            }
            if (!int.TryParse(txtYil.Text, out int yil) || yil <= 1900)
            {
                await DisplayAlert("Uyar�", "Y�l girmelisiniz", "OK");
                return;
            }
        }
        if (!await DisplayAlert("Soru", "Kay�t i�lemi ger�ekle�tirilecektir, devam etmek istiyor musunuz?", "YES", "NO"))
        {
            return;
        }
        var t = await CarRequests.WorkCar(_model.Idno, _model.ModelId, txtPlaka.Text, txtYil.Text);
        if (t.Status)
        {
            await DisplayAlert("Bilgi", "��lem ba�ar�l�", "OK");
            await Shell.Current.GoToAsync("//AboutPage");
        }
    }
}
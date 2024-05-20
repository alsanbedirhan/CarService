using CarService.Requests;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.ViewModels
{
    public class CompanyWorkListViewModel : BaseViewModel
    {
        private ObservableCollection<SearchCompanyWorkList> _ListSource = new ObservableCollection<SearchCompanyWorkList>();
        public ObservableCollection<SearchCompanyWorkList> ListSource { get => _ListSource; set => SetProperty(ref _ListSource, value); }
    }

    public class SearchCompanyWorkList : BaseViewModel
    {
        public decimal Idno { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string AdSoyad => Ad + " " + Soyad;
        public string Marka { get; set; }
        public string Model { get; set; }
        public string MarkaModel => Marka + " " + Model;
        public string Plaka { get; set; }
        public decimal TotalPrice { get; set; }
        private Color _BColor = Color.FromArgb("#F0F8FF");
        public Color BColor { get => _BColor; set => SetProperty(ref _BColor, value); }
    }
}

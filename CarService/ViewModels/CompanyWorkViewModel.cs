using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.ViewModels
{
    public class CompanyWorkViewModel : BaseViewModel
    {
        private string _AdSoyad, _AracBilgi;
        public decimal CompanyWorkId { get; set; }
        public string AdSoyad { get => _AdSoyad; set => SetProperty(ref _AdSoyad, value); }
        public string AracBilgi { get => _AracBilgi; set => SetProperty(ref _AracBilgi, value); }
        private ObservableCollection<CompanyWorkDetail> _ListSource = new ObservableCollection<CompanyWorkDetail>();
        public ObservableCollection<CompanyWorkDetail> ListSource { get => _ListSource; set => SetProperty(ref _ListSource, value); }
    }
    public class CompanyWorkDetail
    {
        public decimal Idno { get; set; }
        public decimal Price { get; set; }
        public string Aciklama { get; set; }
        public DateTime Cdate { get; set; }
        public decimal Cuser { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Cinfo => Ad + " " + Soyad + " - " + Cinfo;
    }
}

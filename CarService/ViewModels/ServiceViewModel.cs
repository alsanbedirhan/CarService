using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.ViewModels
{
    public class ServiceViewModel : BaseViewModel
    {
        private string _AdSoyad, _MarkaModelPlaka, _Aciklama;
        public decimal UserCarId { get; set; }
        public decimal UserId { get; set; }
        public string AdSoyad { get => _AdSoyad; set => SetProperty(ref _AdSoyad, value); }
        public string MarkaModelPlaka { get => _MarkaModelPlaka; set => SetProperty(ref _MarkaModelPlaka, value); }
        public string Aciklama { get => _Aciklama; set => SetProperty(ref _Aciklama, value); }
    }
}

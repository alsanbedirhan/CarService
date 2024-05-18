using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.ViewModels
{
    public class ServiceViewModel : BaseViewModel
    {
        private decimal _Idno, _UserCardId, _UserId, _MakeModelId;
        private string _AdSoyad, _MarkaModel;
        public decimal Idno { get => _Idno; set => SetProperty(ref _Idno, value); }
        public decimal UserCarId { get => _UserCardId; set => SetProperty(ref _UserCardId, value); }
        public decimal UserId { get => _UserId; set => SetProperty(ref _UserId, value); }
        public decimal MakeModelId { get => _MakeModelId; set => SetProperty(ref _MakeModelId, value); }
        public string AdSoyad { get => _AdSoyad; set => SetProperty(ref _AdSoyad, value); }
        public string MarkaModel { get => _MarkaModel; set => SetProperty(ref _MarkaModel, value); }
    }
}

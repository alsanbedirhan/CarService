using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.ViewModels
{
    public class ServiceViewModel : BaseViewModel
    {
        private Users _Musteri;
        public Users Musteri { get => _Musteri; set => SetProperty(ref _Musteri, value); }
    }
}

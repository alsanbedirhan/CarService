using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.ViewModels
{
    public class UsersViewModel : BaseViewModel
    {
        private ObservableCollection<Users> _ListSource = new ObservableCollection<Users>();
        public ObservableCollection<Users> ListSource { get => _ListSource; set => SetProperty(ref _ListSource, value); }
    }
    public class Users
    {
        public decimal Idno { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string AdSoyad => string.Concat(Ad, " ", Soyad);
        public DateTime Cdate { get; set; }
    }
}

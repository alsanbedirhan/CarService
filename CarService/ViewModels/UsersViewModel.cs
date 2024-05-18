using Request_API;
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
        private ObservableCollection<UserInfo> _ListSource = new ObservableCollection<UserInfo>();
        public ObservableCollection<UserInfo> ListSource { get => _ListSource; set => SetProperty(ref _ListSource, value); }
    }
    //public class Users
    //{
    //    public decimal Idno { get; set; }
    //    public string Ad { get; set; }
    //    public string Soyad { get; set; }
    //    public string Mail { get; set; }
    //    public string Tip { get; set; }
    //    public string AdSoyad => string.Concat(Ad, " ", Soyad);
    //    public DateTime Cdate { get; set; }
    //}
}

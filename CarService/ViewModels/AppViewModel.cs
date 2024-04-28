using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.ViewModels
{
    public class AppViewModel : BaseViewModel
    {
        private bool _IsLogin = false;
        public bool IsLogin { get => _IsLogin; set => SetProperty(ref _IsLogin, value); }
        public AppViewModel()
        {
            WeakReferenceMessenger.Default.Register<string>(this, (x, y) =>
            {
                switch (y)
                {
                    case "LOGINOK":
                        IsLogin = true;
                        Shell.Current.GoToAsync("//AboutPage");
                        break;
                    case "NEEDLOGIN":
                        IsLogin = false;
                        Parameters.ActiveUser = null;
                        Shell.Current.GoToAsync("//LoginPage");
                        break;
                    case "NEEDREGISTER":
                        IsLogin = false;
                        Parameters.ActiveUser = null;
                        Shell.Current.GoToAsync("//RegisterPage");
                        break;
                }
            });
        }
    }
}

using CommunityToolkit.Mvvm.Messaging;
using Request_API;
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
        private bool _IsCustomer = false;
        private bool _IsAuthorized = false;
        public bool IsLogin { get => _IsLogin; set => SetProperty(ref _IsLogin, value); }
        public bool IsCustomer { get => _IsCustomer; set { SetProperty(ref _IsCustomer, value); OnPropertyChanged(nameof(IsNotCustomer)); } }
        public bool IsNotCustomer => IsLogin && !IsCustomer;
        public bool IsAuthorized { get => _IsAuthorized; set => SetProperty(ref _IsAuthorized, value); }
        public AppViewModel()
        {
            WeakReferenceMessenger.Default.Register<string>(this, async (x, y) =>
            {
                switch (y)
                {
                    case "LOGINOK":
                        if (Parameters.ActiveUser != null)
                        {
                            IsLogin = true;
                            IsCustomer = Parameters.ActiveUser.UserType == "C";
                            IsAuthorized = Parameters.ActiveUser.UserType == "A";
                            await Shell.Current.GoToAsync("//AboutPage");
                        }
                        else
                        {
                            IsLogin = false;
                            IsCustomer = false;
                            IsAuthorized = false;
                        }
                        break;
                    case "NEEDLOGIN":
                        IsLogin = false;
                        IsCustomer = false;
                        IsAuthorized = false;
                        Parameters.ActiveUser = null;
                        await Shell.Current.GoToAsync("//LoginPage");
                        break;
                    case "NEEDREGISTER":
                        IsLogin = false;
                        IsCustomer = false;
                        IsAuthorized = false;
                        Parameters.ActiveUser = null;
                        await Shell.Current.GoToAsync("//RegisterPage");
                        break;
                }
            });
        }
    }
}

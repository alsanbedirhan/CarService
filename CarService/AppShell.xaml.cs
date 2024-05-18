using CommunityToolkit.Mvvm.Messaging;
using Request_API;

namespace CarService
{
    public partial class AppShell : Shell
    {
        bool isStart = true;
        public AppShell()
        {
            InitializeComponent();
            WeakReferenceMessenger.Default.Register<ErrorProgess>(this, async (x, sender) =>
                await DisplayAlert("Hata", string.IsNullOrEmpty(sender.Message) ? "Hata oluştu" : sender.Message, "OK"));
            //Parameters.AppVersion = VersionTracking.CurrentVersion;
        }

        private void Shell_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentItem")
            {
                if (isStart)
                {
                    isStart = false;
                    return;
                }

                if (!isStart)
                {
                    if (this.Items.IndexOf(this.CurrentItem) == 0 && Parameters.ActiveUser != null)
                    {
                        WeakReferenceMessenger.Default.Send("NEEDLOGIN");
                    }

                    if (Parameters.ActiveUser != null)
                    {
                        LoginItem.Title = "Çıkış Yap";
                        txtUserName.Text = string.Concat(Parameters.ActiveUser.UserId + " - ", Parameters.ActiveUser.AdSoyad);
                    }
                    else
                    {
                        LoginItem.Title = "Giriş Yap";
                        txtUserName.Text = "Giriş Yapmalısınız";
                    }
                }
            }
        }
    }
}

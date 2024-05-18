using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.ViewModels
{
    public class CarListViewModel : BaseViewModel
    {
        private ObservableCollection<Cars> _ListSource = new ObservableCollection<Cars>();
        public ObservableCollection<Cars> ListSource { get => _ListSource; set => SetProperty(ref _ListSource, value); }
    }
    public class Cars
    {
        public decimal Idno { get; set; }
        public decimal ModelId { get; set; }
        public decimal MarkaId { get; set; }
        public decimal UserId { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public short Yil { get; set; }
        public string Plaka { get; set; }
    }
}
using CarService.ViewModels;
using Newtonsoft.Json;
using Request_API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Requests
{
    public class ServiceRequests
    {
        public static async Task<RequestModel<List<SearchUserList>>> SearchUser(string ad, string soyad, string mail)
        {
            return await new Request().Post<List<SearchUserList>>("users/searchcustomer", JsonConvert.SerializeObject(new { ad, soyad, mail }));
        }
        public static async Task<RequestModel<List<SearchUserCarList>>> SearchUserCar(decimal UserId, decimal MakeId, decimal MakeModelId, string Plaka, short Yil)
        {
            return await new Request().Post<List<SearchUserCarList>>("users/searchcustomercars", JsonConvert.SerializeObject(new { UserId, MakeId, MakeModelId, Plaka, Yil }));
        }
        public static async Task<RequestModel<List<SearchCompanyWorkList>>> SearchCompanyWork(decimal MakeId, decimal MakeModelId, string Plaka, string Ad, string Soyad)
        {
            return await new Request().Post<List<SearchCompanyWorkList>>("companies/search", JsonConvert.SerializeObject(new { MakeId, MakeModelId, Plaka, Ad, Soyad, Isdone = "N" }));
        }
        public static async Task<RequestModel> Save(decimal UserCarId, string Aciklama)
        {
            return await new Request().Post("companies/serviceentry", JsonConvert.SerializeObject(new { UserCarId, Aciklama }));
        }
    }
    public class SearchUserList
    {
        public decimal Idno { get; set; }
        public string AdSoyad { get; set; }
        public string Mail { get; set; }
    }
    public class SearchCompanyWorkList
    {
        public decimal Idno { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string AdSoyad => Ad + " " + Soyad;
        public string Marka { get; set; }
        public string Model { get; set; }
        public string MarkaModel => Marka + " " + Model;
        public string Plaka { get; set; }
    }
    public class SearchUserCarList
    {
        public decimal Idno { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public decimal MarkaModelId { get; set; }
        public string Plaka { get; set; }
        public string MarkaModel => Marka + " " + Model;
    }
}
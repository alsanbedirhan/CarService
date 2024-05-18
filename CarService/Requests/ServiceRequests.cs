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
        public static async Task<RequestModel<List<SearchUserCarList>>> SearchUserCar(decimal UserId, decimal MakeId, decimal MakeModelId, string plaka, short yil)
        {
            return await new Request().Post<List<SearchUserCarList>>("users/searchcustomercars", JsonConvert.SerializeObject(new { UserId, MakeId, MakeModelId, plaka, yil }));
        }
    }
    public class SearchUserList
    {
        public decimal Idno { get; set; }
        public string AdSoyad { get; set; }
        public string Mail { get; set; }
    }
    public class SearchUserCarList
    {
        public decimal Idno { get; set; }
        public string MarkaModel { get; set; }
        public string Plaka { get; set; }
    }
}
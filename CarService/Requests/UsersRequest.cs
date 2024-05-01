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
    public class UsersRequest
    {
        public static async Task<RequestModel<List<Users>>> AllUser(string ad, string soyad)
        {
            return await new Request().Post<List<Users>>("users/allusers", JsonConvert.SerializeObject(new { ad, soyad }));
        }
        public static async Task<RequestModel> WorkUser(string ad, string soyad, decimal id)
        {
            return await new Request().Post("users/workuser", JsonConvert.SerializeObject(new { ad, soyad, id }));
        }
    }
}

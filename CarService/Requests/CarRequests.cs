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
    public class CarRequests
    {
        public static async Task<RequestModel<List<Cars>>> WorkCar(decimal Idno, decimal UserId, decimal ModelId, string Plaka, short Yil)
        {
            return await new Request().Post<List<Cars>>("cars/workcar", JsonConvert.SerializeObject(new { Idno, UserId, ModelId, Plaka, Yil }));
        }
    }
}

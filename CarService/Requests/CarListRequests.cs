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
    public class CarListRequests
    {
        public static async Task<RequestModel<List<Cars>>> AllCars(string marka, string model)
        {
            return await new Request().Post<List<Cars>>("cars/allcars", JsonConvert.SerializeObject(new { marka, model }));
        }
    }
}

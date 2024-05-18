using CarService.ViewModels;
using CarService.Views;
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
        private static List<clsSearchDetail>? AllMakeModels;
        public static async Task<RequestModel<List<Cars>>> Cars(decimal UserId, List<decimal> MakeIds, List<decimal> MakeModelIds)
        {
            return await new Request().Post<List<Cars>>("cars/allcars", JsonConvert.SerializeObject(new { UserId, MakeIds, MakeModelIds }));
        }
        public static async Task<List<clsSearch>> GetMakes()
        {
            if (AllMakeModels == null)
            {
                AllMakeModels = await GetAllMakeModels();
            }
            return AllMakeModels?.Select(x => new clsSearch { Key = x.Key, DisplayValue = x.DisplayValue }).ToList() ?? new List<clsSearch>();
        }
        private static async Task<List<clsSearchDetail>?> GetAllMakeModels()
        {
            var t = await new Request().Get<List<clsSearchDetail>>("cars/makemodels");
            return t.Status ? t.Data : null;
            //if (t.Status)
            //{
            //    return t.Data.Select(x => new clsSearchDetail
            //    {
            //        Key = x.Key,
            //        DisplayValue = x.DisplayValue,
            //        Details = x.Details.Select(y => new clsSearch { Key = y.Key, DisplayValue = x.DisplayValue + " - " + y.DisplayValue }).ToList()
            //    }).ToList();
            //}
            //return null;
        }
        public static async Task<List<clsSearch>> GetMakeModels(List<decimal> MakeIds)
        {
            if (AllMakeModels == null)
            {
                AllMakeModels = await GetAllMakeModels();
            }
            return AllMakeModels?.Where(x => MakeIds.Contains(x.Key)).SelectMany(x => x.Details).ToList() ?? new List<clsSearch>();
        }
        public static async Task<List<clsSearchDetail>?> SetMakeModels()
        {
            var m = await SecureStorage.GetAsync("makemodels");
            if (m != null)
            {
                return JsonConvert.DeserializeObject<List<clsSearchDetail>>(m);
            }
            var t = await new Request().Get<List<clsSearchDetail>>("cars/makemodels");
            if (t.Status)
            {
                await SecureStorage.SetAsync("makemodels", JsonConvert.SerializeObject(t.Data));
                return t.Data;
            }
            return null;
        }
    }
}

public class clsSearchDetail : clsSearch
{
    public List<clsSearch> Details { get; set; }
}
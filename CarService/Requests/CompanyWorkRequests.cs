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
    public class CompanyWorkRequests
    {
        public static async Task<RequestModel<List<CompanyWorkDetail>>> Detail(decimal idno)
        {
            return await new Request().Get<List<CompanyWorkDetail>>("companies/detail?headerid=" + idno);
        }
        public static async Task<RequestModel> AddDetail(decimal CompanyWorkId, decimal Price, string Aciklama)
        {
            return await new Request().Post("companies/adddetail", JsonConvert.SerializeObject(new { CompanyWorkId, Price, Aciklama }));
        }
        public static async Task<RequestModel> DeleteDetail(decimal Idno)
        {
            return await new Request().Post("companies/deletedetail", JsonConvert.SerializeObject(new { Idno }));
        }
    }
}

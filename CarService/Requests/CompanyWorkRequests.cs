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
        public static async Task<RequestModel> WorkDetail(decimal Idno, decimal CompanyWorkId, decimal Price, string Aciklama)
        {
            return await new Request().Post("companies/workdetail", JsonConvert.SerializeObject(new { Idno, CompanyWorkId, Price, Aciklama }));
        }
        public static async Task<RequestModel> DeleteDetail(decimal Idno)
        {
            return await new Request().Post("companies/deletedetail", JsonConvert.SerializeObject(new { Idno }));
        }
        public static async Task<RequestModel> Status(decimal Idno, string Isdone, string Isout)
        {
            return await new Request().Post("companies/status", JsonConvert.SerializeObject(new { Idno, Isdone, Isout }));
        }
    }
}

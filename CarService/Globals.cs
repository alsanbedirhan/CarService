using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService
{
    public class ErrorProgess
    {
        public string Message { get; set; } = "";
    }
    public static class GlobalValues
    {
        public static string ServerIP = "http://172.20.10.2:5209/";
        public static string BaseURL = ServerIP + "api/";
    }
    public class ResultModel<T> : ResultModel
    {
        public T? Data { get; set; }
    }
    public class RequestModel : ResultModel
    {
        public string Token { get; set; } = "";
    }
    public class RequestModel<T> : RequestModel
    {
        public T? Data { get; set; }
    }
    public class ResultModel
    {
        public string Message { get; set; } = "";
        public string StringValue1 { get; set; } = "";
        public long LongValue1 { get; set; } = 0;
        public bool Status { get; set; } = false;
    }
    public class User
    {
        public int UserId { get; set; }
        public string Ad { get; set; } = "";
        public string Soyad { get; set; } = "";
        public string AdSoyad { get { return string.Concat(Ad, " ", Soyad); } }
        public string Token { get; set; } = "";
        public string UserType { get; set; }
        public string CompanyName { get; set; }
        public decimal CompanyId { get; set; }
    }
    public static class Parameters
    {
        public static User? ActiveUser { get; set; } = null;
        public static string DeviceId { get; set; } = "";
        public static string AppVersion { get; set; } = "";
    }
}

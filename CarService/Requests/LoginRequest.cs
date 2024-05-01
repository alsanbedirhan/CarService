using Request_API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Requests
{
    public class LoginRequest
    {
        public static async Task<RequestModel<User>> Login(string mail, string psw)
        {
            return await (new Request()).Post<User>("noauth/login", Newtonsoft.Json.JsonConvert.SerializeObject(new { mail, psw }));
        }
    }
}

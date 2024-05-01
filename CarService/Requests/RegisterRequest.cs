using Request_API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Requests
{
    public class RegisterRequest
    {
        public static async Task<RequestModel<User>> Register(string name, string surname, string mail, string psw)
        {
            return await (new Request()).Post<User>("noauth/register", Newtonsoft.Json.JsonConvert.SerializeObject(new { name, surname, mail, psw }));
        }
    }
}

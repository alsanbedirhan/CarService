using CommunityToolkit.Mvvm.Messaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Requests
{
    public class Request
    {
        public Request()
        {

        }
        public async Task<ResultModel<T>> Get<T>(string prm)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(25);
                    string url = GlobalValues.BaseURL + prm;
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Parameters.ActiveUser.Token);
                    client.DefaultRequestHeaders.Add("Device", Parameters.DeviceId);
                    client.DefaultRequestHeaders.Add("App-Version", Parameters.AppVersion);

                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        var jsonvalue = JsonConvert.DeserializeObject<RequestModel<T>>(responseData);
                        Parameters.ActiveUser.Token = jsonvalue.Token;
                        if (jsonvalue.Status)
                        {
                            return new ResultModel<T> { Status = true, Data = jsonvalue.Data, StringValue1 = jsonvalue.StringValue1, LongValue1 = jsonvalue.LongValue1 };
                        }
                        else
                        {
                            WeakReferenceMessenger.Default.Send(new ErrorProgess { Message = jsonvalue.Message });
                            //MessagingCenter.Send(new Global.ErrorProgess { Message = jsonvalue.Message }, "APIERROR");
                            return new ResultModel<T> { Status = false };
                        }
                    }
                    else
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized || response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                        {
                            string error = (response.StatusCode == System.Net.HttpStatusCode.Unauthorized ? "Oturumunuz zaman aşımına uğramış, lütfen tekrar giriş yapınız" :
                                "Uygulamanız en güncel sürüme sahip değil, uygulamayı kullanmak için güncelleme yapmanız gerekiyor");
                            WeakReferenceMessenger.Default.Send(new ErrorProgess { Message = error });
                            //MessagingCenter.Send(new Global.ErrorProgess { Message = error }, "APIERROR");
                            WeakReferenceMessenger.Default.Send("NEEDLOGIN");
                            //MessagingCenter.Send(new Global.LoginProgess(), "NO");
                            return new ResultModel<T> { Status = false };
                        }
                        string message = "";
                        try
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var jsonvalue = JsonConvert.DeserializeObject<RequestModel>(content);
                            message = jsonvalue.Message;
                            Parameters.ActiveUser.Token = jsonvalue.Token;
                        }
                        catch (Exception)
                        {
                            try
                            {
                                message = await response.Content.ReadAsStringAsync();
                            }
                            catch (Exception)
                            {

                            }
                        }
                        WeakReferenceMessenger.Default.Send(new ErrorProgess { Message = message });
                        //MessagingCenter.Send(new Global.ErrorProgess { Message = message }, "APIERROR");
                        return new ResultModel<T> { Status = false };
                    }
                }
            }
            catch (Exception ex)
            {
                WeakReferenceMessenger.Default.Send(new ErrorProgess { Message = "Bağlantı hatası, lütfen sonra tekrar deneyiniz" });
                //MessagingCenter.Send(new Global.ErrorProgess { Message = "Bağlantı hatası, lütfen sonra tekrar deneyiniz" }, "APIERROR");
                return new ResultModel<T> { Status = false };
            }
        }
        public async Task<ResultModel<T>> Post<T>(string prm, string json)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(25);

                    string url = GlobalValues.BaseURL + prm;

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Parameters.ActiveUser.Token);
                    client.DefaultRequestHeaders.Add("Device", Parameters.DeviceId);
                    client.DefaultRequestHeaders.Add("App-Version", Parameters.AppVersion);

                    var requestContent = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(url, requestContent);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        var jsonvalue = JsonConvert.DeserializeObject<RequestModel<T>>(responseData);
                        Parameters.ActiveUser.Token = jsonvalue.Token;
                        if (jsonvalue.Status)
                        {
                            return new ResultModel<T> { Status = true, Message = jsonvalue.Message, Data = jsonvalue.Data, StringValue1 = jsonvalue.StringValue1, LongValue1 = jsonvalue.LongValue1 };
                        }
                        else
                        {
                            WeakReferenceMessenger.Default.Send(new ErrorProgess { Message = jsonvalue.Message });
                            //MessagingCenter.Send(new Global.ErrorProgess { Message = jsonvalue.Message }, "APIERROR");
                            return new ResultModel<T> { Status = false };
                        }
                    }
                    else
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized || response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                        {
                            string error = (response.StatusCode == System.Net.HttpStatusCode.Unauthorized ? "Oturumunuz zaman aşımına uğramış, lütfen tekrar giriş yapınız" :
                                "Uygulamanız en güncel sürüme sahip değil, uygulamayı kullanmak için güncelleme yapmanız gerekiyor");
                            WeakReferenceMessenger.Default.Send(new ErrorProgess { Message = error });
                            //MessagingCenter.Send(new Global.ErrorProgess { Message = error }, "APIERROR");
                            WeakReferenceMessenger.Default.Send("NEEDLOGIN");
                            //MessagingCenter.Send(new Global.LoginProgess(), "NO");
                            return new ResultModel<T> { Status = false };
                        }
                        string message = "";
                        try
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var jsonvalue = JsonConvert.DeserializeObject<RequestModel>(content);
                            message = jsonvalue.Message;
                            Parameters.ActiveUser.Token = jsonvalue.Token;
                        }
                        catch (Exception)
                        {
                            try
                            {
                                message = await response.Content.ReadAsStringAsync();
                            }
                            catch (Exception)
                            {

                            }
                        }

                        WeakReferenceMessenger.Default.Send(new ErrorProgess { Message = message });
                        //MessagingCenter.Send(new Global.ErrorProgess { Message = message }, "APIERROR");
                        return new ResultModel<T> { Status = false };
                    }
                }
            }
            catch (Exception ex)
            {
                WeakReferenceMessenger.Default.Send(new ErrorProgess { Message = "Bağlantı hatası, lütfen sonra tekrar deneyiniz" });
                //MessagingCenter.Send(new Global.ErrorProgess { Message = "Bağlantı hatası, lütfen sonra tekrar deneyiniz" }, "APIERROR");
                return new ResultModel<T> { Status = false };
            }
        }
        public async Task<ResultModel> Post(string prm, string json)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(25);

                    string url = GlobalValues.BaseURL + prm;

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Parameters.ActiveUser.Token);
                    client.DefaultRequestHeaders.Add("Device", Parameters.DeviceId);
                    client.DefaultRequestHeaders.Add("App-Version", Parameters.AppVersion);

                    var requestContent = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(url, requestContent);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        var jsonvalue = JsonConvert.DeserializeObject<RequestModel>(responseData);
                        Parameters.ActiveUser.Token = jsonvalue.Token;
                        if (jsonvalue.Status)
                        {
                            return new ResultModel { Status = true, Message = jsonvalue.Message, StringValue1 = jsonvalue.StringValue1, LongValue1 = jsonvalue.LongValue1 };
                        }
                        else
                        {
                            WeakReferenceMessenger.Default.Send(new ErrorProgess { Message = jsonvalue.Message });
                            //MessagingCenter.Send(new Global.ErrorProgess { Message = jsonvalue.Message }, "APIERROR");
                            return new ResultModel { Status = false };
                        }
                    }
                    else
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized || response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                        {
                            string error = (response.StatusCode == System.Net.HttpStatusCode.Unauthorized ? "Oturumunuz zaman aşımına uğramış, lütfen tekrar giriş yapınız" :
                                "Uygulamanız en güncel sürüme sahip değil, uygulamayı kullanmak için güncelleme yapmanız gerekiyor");
                            WeakReferenceMessenger.Default.Send(new ErrorProgess { Message = error });
                            //MessagingCenter.Send(new Global.ErrorProgess { Message = error }, "APIERROR");
                            WeakReferenceMessenger.Default.Send("NEEDLOGIN");
                            //MessagingCenter.Send(new Global.LoginProgess(), "NO");
                            return new ResultModel { Status = false };
                        }
                        string message = "";
                        try
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var jsonvalue = JsonConvert.DeserializeObject<RequestModel>(content);
                            message = jsonvalue.Message;
                            Parameters.ActiveUser.Token = jsonvalue.Token;
                        }
                        catch (Exception)
                        {
                            try
                            {
                                message = await response.Content.ReadAsStringAsync();
                            }
                            catch (Exception)
                            {

                            }
                        }
                        WeakReferenceMessenger.Default.Send(new ErrorProgess { Message = message });
                        //MessagingCenter.Send(new Global.ErrorProgess { Message = message }, "APIERROR");
                        return new ResultModel { Status = false };
                    }
                }
            }
            catch (Exception ex)
            {
                WeakReferenceMessenger.Default.Send(new ErrorProgess { Message = "Bağlantı hatası, lütfen sonra tekrar deneyiniz" });
                //MessagingCenter.Send(new Global.ErrorProgess { Message = "Bağlantı hatası, lütfen sonra tekrar deneyiniz" }, "APIERROR");
                return new ResultModel { Status = false };
            }
        }
        public async Task<ResultModel> Login(string mail, string psw)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(25);
                    string url = GlobalValues.BaseURL + "noauth/login";

                    var requestData = new { mail = mail.Trim(), psw = psw.Trim() };
                    var json = JsonConvert.SerializeObject(requestData);
                    var requestContent = new StringContent(json, Encoding.UTF8, "application/json");

                    client.DefaultRequestHeaders.Add("Device", Parameters.DeviceId);
                    client.DefaultRequestHeaders.Add("App-Version", Parameters.AppVersion);

                    HttpResponseMessage response = await client.PostAsync(url, requestContent);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        var jsonvalue = JsonConvert.DeserializeObject<RequestModel<User>>(responseData);
                        if (jsonvalue.Status)
                        {
                            Parameters.ActiveUser = jsonvalue.Data;
                            return new ResultModel { Status = true, StringValue1 = jsonvalue.StringValue1 };
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(jsonvalue.StringValue1))
                            {
                                WeakReferenceMessenger.Default.Send(new ErrorProgess { Message = jsonvalue.Message });
                                //MessagingCenter.Send(new Global.ErrorProgess { Message = jsonvalue.Message }, "APIERROR");
                            }
                            return new ResultModel { Status = false, StringValue1 = jsonvalue.StringValue1 };
                        }
                    }
                    else
                    {
                        string message = "", stringvalue = "";
                        try
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var jsonvalue = JsonConvert.DeserializeObject<RequestModel>(content);
                            message = jsonvalue.Message;
                            stringvalue = jsonvalue.StringValue1;
                        }
                        catch (Exception)
                        {
                            try
                            {
                                message = await response.Content.ReadAsStringAsync();
                            }
                            catch (Exception)
                            {

                            }
                        }
                        if (string.IsNullOrEmpty(stringvalue))
                        {
                            WeakReferenceMessenger.Default.Send(new ErrorProgess { Message = message });
                            //MessagingCenter.Send(new Global.ErrorProgess { Message = message }, "APIERROR");
                        }
                        return new ResultModel { Status = false, StringValue1 = stringvalue };
                    }
                }
            }
            catch (Exception ex)
            {
                WeakReferenceMessenger.Default.Send(new ErrorProgess { Message = "Bağlantı hatası, lütfen sonra tekrar deneyiniz" });
                //MessagingCenter.Send(new Global.ErrorProgess { Message = "Bağlantı hatası, lütfen sonra tekrar deneyiniz" }, "APIERROR");
                return new ResultModel { Status = false };
            }
        }
        public async Task<ResultModel> Register(string ad, string soyad, string mail, string psw)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(25);
                    string url = GlobalValues.BaseURL + "noauth/register";

                    var requestData = new { name = ad.Trim(), surname = soyad.Trim(), mail = mail.Trim(), psw };
                    var json = JsonConvert.SerializeObject(requestData);
                    var requestContent = new StringContent(json, Encoding.UTF8, "application/json");

                    client.DefaultRequestHeaders.Add("Device", Parameters.DeviceId);
                    client.DefaultRequestHeaders.Add("App-Version", Parameters.AppVersion);

                    HttpResponseMessage response = await client.PostAsync(url, requestContent);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        var jsonvalue = JsonConvert.DeserializeObject<RequestModel<User>>(responseData);
                        if (jsonvalue.Status)
                        {
                            Parameters.ActiveUser = jsonvalue.Data;
                            return new ResultModel { Status = true, StringValue1 = jsonvalue.StringValue1 };
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(jsonvalue.StringValue1))
                            {
                                WeakReferenceMessenger.Default.Send(new ErrorProgess { Message = jsonvalue.Message });
                                //MessagingCenter.Send(new Global.ErrorProgess { Message = jsonvalue.Message }, "APIERROR");
                            }
                            return new ResultModel { Status = false, StringValue1 = jsonvalue.StringValue1 };
                        }
                    }
                    else
                    {
                        string message = "", stringvalue = "";
                        try
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var jsonvalue = JsonConvert.DeserializeObject<RequestModel>(content);
                            message = jsonvalue.Message;
                            stringvalue = jsonvalue.StringValue1;
                        }
                        catch (Exception)
                        {
                            try
                            {
                                message = await response.Content.ReadAsStringAsync();
                            }
                            catch (Exception)
                            {

                            }
                        }
                        if (string.IsNullOrEmpty(stringvalue))
                        {
                            WeakReferenceMessenger.Default.Send(new ErrorProgess { Message = message });
                            //MessagingCenter.Send(new Global.ErrorProgess { Message = message }, "APIERROR");
                        }
                        return new ResultModel { Status = false, StringValue1 = stringvalue };
                    }
                }
            }
            catch (Exception ex)
            {
                WeakReferenceMessenger.Default.Send(new ErrorProgess { Message = "Bağlantı hatası, lütfen sonra tekrar deneyiniz" });
                //MessagingCenter.Send(new Global.ErrorProgess { Message = "Bağlantı hatası, lütfen sonra tekrar deneyiniz" }, "APIERROR");
                return new ResultModel { Status = false };
            }
        }
    }
}

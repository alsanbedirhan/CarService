using Request_API;

namespace CarService.Requests
{
    public class Request
    {
        Dictionary<string, string> _headers;
        public Request()
        {
            _headers = new Dictionary<string, string>
            {
                { "Authorization", "Bearer "+ Parameters.ActiveUser?.Token ?? "" },
                { "Device", Parameters.DeviceId }
            };
        }
        public async Task<RequestModel<T>> Get<T>(string prm)
        {
            return await (new Methods(_headers)).Get<T>(prm);
        }
        public async Task<RequestModel<T>> Post<T>(string prm, string json)
        {
            return await (new Methods(_headers)).Post<T>(prm, json);
        }
        public async Task<RequestModel> Post(string prm, string json)
        {
            return await (new Methods(_headers)).Post(prm, json);
        }
    }

}

using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Serializers.Json;
using System.Text.Json;

namespace Takip.Web.Code.Rest
{
    public class IndexRestClient
    {
        private string BASE_API_URI = "https://localhost:7041/api";

        public dynamic ProductCount()
        {
            RestClient client = new RestClient(BASE_API_URI, configureSerialization: s => s.UseSystemTextJson(new JsonSerializerOptions { PropertyNamingPolicy = null }));

            RestRequest req = new RestRequest("/Product/ProductCount", Method.Get);

            RestResponse resp = client.Get(req);
            string msg = resp.Content.ToString();
            dynamic result = JObject.Parse(msg);
            return result;
        }

        public dynamic ServiceCount()
        {
            RestClient client = new RestClient(BASE_API_URI, configureSerialization: s => s.UseSystemTextJson(new JsonSerializerOptions { PropertyNamingPolicy = null }));

            RestRequest req = new RestRequest("/Service/ServiceCount", Method.Get);

            RestResponse resp = client.Get(req);
            string msg = resp.Content.ToString();
            dynamic result = JObject.Parse(msg);
            return result;
        }

        public dynamic CustomerCount()
        {
            RestClient client = new RestClient(BASE_API_URI, configureSerialization: s => s.UseSystemTextJson(new JsonSerializerOptions { PropertyNamingPolicy = null }));

            RestRequest req = new RestRequest("/Customer/CustomerCount", Method.Get);

            RestResponse resp = client.Get(req);
            string msg = resp.Content.ToString();
            dynamic result = JObject.Parse(msg);
            return result;
        }

        public dynamic PersonnelCount()
        {
            RestClient client = new RestClient(BASE_API_URI, configureSerialization: s => s.UseSystemTextJson(new JsonSerializerOptions { PropertyNamingPolicy = null }));

            RestRequest req = new RestRequest("/Personnel/PersonnelCount", Method.Get);

            RestResponse resp = client.Get(req);
            string msg = resp.Content.ToString();
            dynamic result = JObject.Parse(msg);
            return result;
        }

    }
}

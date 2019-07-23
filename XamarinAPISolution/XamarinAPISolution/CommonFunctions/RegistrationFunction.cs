using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using XamarinAPISolution.Models;
using System.Net.Http;
using System.Net.Http.Headers;

namespace XamarinAPISolution.CommonFunctions
{
    public class RegistrationFunction : IRegistrationFunction
    {
        public async Task<bool> Registration(RegDataInput data)
        {
            ApiMethodsList api = new ApiMethodsList();
            // Registration and login function implementation
            string JsonData = JsonConvert.SerializeObject(data);

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            StringContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage result = await client.PostAsync(api.API_RegistrationMethod_POST(), content);

            string answer = await result.Content.ReadAsStringAsync();

            if (answer.Contains("error") || answer.Contains("false"))
            {
                return false;
            }

            return true;
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Text;
using XamarinAPISolution.Models;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using System.Net.Http.Headers;

namespace XamarinAPISolution.CommonFunctions
{
    public class LoginFunction : ILoginFunction
    {
        public async Task<bool> Login(LoginDataInput data)
        {
            ApiMethodsList api = new ApiMethodsList();
            // Json strings also can be created manualy or with other services
            string JsonData = JsonConvert.SerializeObject(data);

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            StringContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
            
            HttpResponseMessage result = await client.PostAsync(api.API_LoginMethod_POST(), content);

            string answer = await result.Content.ReadAsStringAsync();

            // some error was while login
            if(answer.Contains("error") || answer.Contains("false"))
            {
                return false;
            }

            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            }
            File.WriteAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "userToken.json"), answer);
            return true;
        }
    }
}

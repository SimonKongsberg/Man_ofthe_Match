using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace MoM.Data
{
    class ClubsManager
    {
        const string url = "http://nicolaisolutions.com/myjson.json";
        private string accesskey;

        private JsonSerializer json = new JsonSerializer();

        private async Task<HttpClient> GetClient()
        {
            HttpClient client = new HttpClient();
            
            if (string.IsNullOrEmpty(accesskey))
            {
                accesskey = await client.GetStringAsync(url + "login");
                accesskey = JsonConvert.DeserializeObject<string>(accesskey);

            }
            client.DefaultRequestHeaders.Add("Authorization", accesskey);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            
            return client;
            
        }
        public async Task<IEnumerable<Clubs>> GetAll()
        {
            HttpClient client = await GetClient();
            string result = await client.GetStringAsync(url);
            return JsonConvert.DeserializeObject<IEnumerable<Clubs>>(result);
        }
    }
}

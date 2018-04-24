using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Data;
using System.Data.SqlClient;

namespace MoM.Data
{

    public class ClubsManager
    {

        const string url = "http://www.nicolaisolutions.com/myjson.json";
        private string accesskey;

        private JsonSerializer json = new JsonSerializer();

        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            /*
            if (string.IsNullOrEmpty(accesskey))
            {
                accesskey = await client.GetStringAsync(url + "login");
                accesskey = JsonConvert.DeserializeObject<string>(accesskey);

            }
            client.DefaultRequestHeaders.Add("Authorization", accesskey);
            */
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;

        }
        public async Task<IEnumerable<Clubs>> GetAll()
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(url);
            return JsonConvert.DeserializeObject<IEnumerable<Clubs>>(result);
        }

        public async Task<Clubs> Add(string name, string cuisine)
        {
            // TODO: use POST to add a book
            Clubs club = new Clubs()
            {
                Name = name,
                Cuisine = cuisine,
                PublishDate = DateTime.Now.Date,
            };

            HttpClient client = GetClient();
            var response = await client.PostAsync(url,
                new StringContent(
                    JsonConvert.SerializeObject(club),
                    Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<Clubs>(
                await response.Content.ReadAsStringAsync());
        }

        public async Task Update(Clubs club)
        {
            // TODO: use PUT to update a book
            HttpClient client = GetClient();
            await client.PutAsync(url + "/" + club.Name,
                new StringContent(
                    JsonConvert.SerializeObject(club),
                    Encoding.UTF8, "application/json"));
        }
    }
}

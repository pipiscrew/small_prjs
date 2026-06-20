using Newtonsoft.Json;
using posokanei.Entities;
using posokanei.Interfaces.Services;
using System.Net.Http;
using System.Threading.Tasks;

namespace posokanei.Services
{
    public class APICommands : IAPICommands
    {
        private static readonly HttpClient _http = new HttpClient();

        public async Task<Root> GetAsync(string productURL)
        {
            var url = productURL;

            var json = await _http.GetStringAsync(url);

            var result = JsonConvert.DeserializeObject<Root>(json);

            return result;
        }
    }
}

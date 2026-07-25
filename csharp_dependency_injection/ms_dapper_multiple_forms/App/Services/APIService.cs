using App.Interfaces.Services;
using Domain;
using Newtonsoft.Json;
using Serilog;
using System.Net.Http;
using System.Threading.Tasks;

namespace App.Services
{
    public class APIService : IAPIService
    {
        private static readonly HttpClient _http = new HttpClient();
        private readonly ILogger _logger;

        public APIService(ILogger logger)
        {
            this._logger = logger;
        }

        public async Task LogUserIPAsync()
        {   //error swallowed due 'Task.Run' use try..catch to show the error
            var ip = await _http.GetStringAsync("https://api.ipify.org/");

            _logger.Information(ip);
        }

        public async Task<Root> GetAsync(string productURL)
        {
            var url = productURL;

            var json = await _http.GetStringAsync(url);

            var result = JsonConvert.DeserializeObject<Root>(json);

            return result;
        }

    }
}

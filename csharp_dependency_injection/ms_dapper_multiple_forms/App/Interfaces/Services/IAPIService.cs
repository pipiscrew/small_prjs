using Domain;
using System.Threading.Tasks;

namespace App.Interfaces.Services
{
    public interface IAPIService
    {
        Task LogUserIPAsync();
        Task<Root> GetAsync(string productURL);
    }
}

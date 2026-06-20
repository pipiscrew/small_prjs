using posokanei.Entities;
using System.Threading.Tasks;

namespace posokanei.Interfaces.Services
{
    public interface IAPICommands
    {
        Task<Root> GetAsync(string productURL);
    }
}

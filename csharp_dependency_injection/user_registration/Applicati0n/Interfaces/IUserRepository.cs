using System.Collections.Generic;
using System.Threading.Tasks;
using WindowsFormsApplication37.Applicati0n.Domain;

namespace WindowsFormsApplication37.Applicati0n.Interfaces
{
    public interface IUserRepository
    {
        Task AddUser(string name);
        Task<IEnumerable<User>> GetAllUsers();
        Task<string> GetCodeA(string name);
        Task<string> GetCodeB(int field, int userId);
    }
}

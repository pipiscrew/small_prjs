using System.Threading.Tasks;
using WindowsFormsApplication37.Applicati0n.Interfaces;

namespace WindowsFormsApplication37.Applicati0n.Services
{
    public interface IServiceB
    {
        Task<bool> Validate(string inputName, string dbCode);
    }

    public class ServiceB : IServiceB
    {
         private readonly IUserRepository _u;

        public ServiceB(IUserRepository u)
        {
            _u = u;
        }

        
        public async Task<bool> Validate(string inputName, string dbCode)
        {
            return await Task.Run(() =>
            {
                return string.Format("HHHHHo{0}", inputName).Equals(dbCode);
            });
        }
    }
}

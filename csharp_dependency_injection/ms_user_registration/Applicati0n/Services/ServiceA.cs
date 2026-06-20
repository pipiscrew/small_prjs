using System.Threading.Tasks;
using WindowsFormsApplication37.Applicati0n.Domain;
using WindowsFormsApplication37.Applicati0n.Interfaces;

namespace WindowsFormsApplication37.Applicati0n.Services
{
    public interface IServiceA
    {
        Task<string> Validate(User user, string inputCode);
    }

    public class ServiceA : IServiceA
    {
        private readonly IUserRepository _u;

        public ServiceA(IUserRepository u)
        {
            _u = u;
        }

        
        public async Task<string> Validate(User user, string inputCode)
        {
            if (string.IsNullOrEmpty(inputCode))
                return null;

            return await _u.GetCodeA(user.nam3);
        }
    }
}

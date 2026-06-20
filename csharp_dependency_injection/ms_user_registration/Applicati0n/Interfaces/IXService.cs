using System.Threading.Tasks;
using WindowsFormsApplication37.Applicati0n.Domain;

namespace WindowsFormsApplication37.Applicati0n.Interfaces
{
    public interface IXService
    {
        Task<DaLock> Validate();
        string username { get; set; }
    }
}

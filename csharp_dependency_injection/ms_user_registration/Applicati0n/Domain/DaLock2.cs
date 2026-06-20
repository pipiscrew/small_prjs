using System.Threading.Tasks;
using WindowsFormsApplication37.Applicati0n.Interfaces;

namespace WindowsFormsApplication37.Applicati0n.Domain
{
    public class DaLock2 : IXService
    {
        public string username { get; set; }
        
        public async Task<DaLock> Validate()
        {
            return await Task.Run(() =>
            {
                uint crc = 0xFFFFFFFF;
                uint crc2 = 0xFFFFFFFF;
                foreach (char c in this.username)
                {
                    crc ^= (uint)c;
                    crc2 ^= (uint)c;
                    for (int i = 0; i < 8; i++)
                    {
                        if ((crc & 1) == 1)
                        {
                            crc = (crc >> 1) ^ 0x7B2F4A1C;
                            crc2 = (crc2 >> 1) ^ 0xD9E8C3F7;
                        }
                        else
                        {
                            crc >>= 1; crc2 >>= 1;
                        }
                    }
                }

                return new DaLock() { usercode1 = crc, usercode2 = crc2 };
            });
        }
    }
}

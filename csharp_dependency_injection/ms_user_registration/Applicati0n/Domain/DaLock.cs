using System.Threading.Tasks;
using WindowsFormsApplication37.Applicati0n.Interfaces;

namespace WindowsFormsApplication37.Applicati0n.Domain
{
    public class DaLock : IXService
    {
        public string username { get; set; }
        public uint usercode1 { get; set; }
        public uint usercode2 { get; set; }

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
                            crc = (crc >> 1) ^ 0xEDB88320;
                            crc2 = (crc2 >> 1) ^ 0xA3F5C7E9;
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

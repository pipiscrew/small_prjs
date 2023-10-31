//original article - https://www.pipiscrew.com/threads/windows-performance-data-helper-pdh-api.95086/
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

class Program
{
    [DllImport("pdh.dll")]
    public static extern PdhStatus PdhOpenQuery(string szDataSource, IntPtr dwUserData, out IntPtr phQuery);

    [DllImport("pdh.dll")]
    public static extern PdhStatus PdhAddCounter(IntPtr hQuery, string szFullCounterPath, IntPtr dwUserData, out IntPtr phCounter);

    [DllImport("pdh.dll")]
    public static extern PdhStatus PdhCollectQueryData(IntPtr hQuery);

    [DllImport("pdh.dll")]
    public static extern PdhStatus PdhGetFormattedCounterValue(IntPtr hCounter, PdhValueFormat dwFormat, out uint lpdwType, out PdhCounterValue pValue);

    public enum PdhStatus : uint
    {
        PDH_CSTATUS_VALID_DATA = 0x0,
        PDH_INVALID_ARGUMENT = 0xC0000BB8,
        PDH_NO_MORE_DATA = 0xC0000BC1,
        PDH_END_OF_LOG_FILE = 0xC0000BC2,
        PDH_ENTRY_NOT_IN_LOG_FILE = 0xC0000BC3,
        PDH_NO_DATA = 0xC0000BC5,
        PDH_INSUFFICIENT_BUFFER = 0xC0000BC6,
        PDH_MORE_DATA = 0xC0000BC7
    }

    public enum PdhValueFormat : uint
    {
        PDH_FMT_LONG = 0x00000100,
        PDH_FMT_DOUBLE = 0x00000200,
        PDH_FMT_LARGE = 0x00000400,
        PDH_FMT_NOSCALE = 0x00001000,
        PDH_FMT_1000 = 0x00002000,
        PDH_FMT_NODATA = 0x00004000
    }

    public struct PdhCounterValue
    {
        public PdhValueFormat CStatus;
        public double value;
    }

    static void Main(string[] args)
    {
        IntPtr hQuery = IntPtr.Zero;
        IntPtr hCounter = IntPtr.Zero;

        // Open a query
        PdhStatus status = PdhOpenQuery(null, IntPtr.Zero, out hQuery);
        if (status != PdhStatus.PDH_CSTATUS_VALID_DATA)
        {
            Console.WriteLine("PdhOpenQuery failed with status: " + status);
            return;
        }

        // Add a performance counter (e.g., Processor\% Processor Time)
        status = PdhAddCounter(hQuery, "\\Processor(_Total)\\% Processor Time", IntPtr.Zero, out hCounter);
        if (status != PdhStatus.PDH_CSTATUS_VALID_DATA)
        {
            Console.WriteLine("PdhAddCounter failed with status: " + status);
            return;
        }

        // Collect data
        status = PdhCollectQueryData(hQuery);
        if (status != PdhStatus.PDH_CSTATUS_VALID_DATA)
        {
            Console.WriteLine("PdhCollectQueryData failed with status: " + status);
            return;
        }

        //// Continuously collect and monitor CPU usage
        //while (true)
        //{
            // Collect data
            status = PdhCollectQueryData(hQuery);
            if (status != PdhStatus.PDH_CSTATUS_VALID_DATA)
            {
                Console.WriteLine("PdhCollectQueryData failed with status: " + status);
                return;
            }

            uint lpdwType;
            PdhCounterValue counterValue = new PdhCounterValue();
            status = PdhGetFormattedCounterValue(hCounter, PdhValueFormat.PDH_FMT_DOUBLE, out  lpdwType, out counterValue);
            if (status != PdhStatus.PDH_CSTATUS_VALID_DATA)
            {
                Console.WriteLine("PdhGetFormattedCounterValue failed with status: " + status);
                return;
            }

            Console.WriteLine("Processor Usage: " + counterValue.value + "%");

            // Check if CPU usage exceeds 5%
            if (counterValue.value > 5)
            {
                Console.WriteLine("CPU usage exceeds 5%. Exiting.");
                //break;
            }

        //    // Sleep for a while before collecting the next data point
        //    Thread.Sleep(1000); // Sleep for 1 second
        //}

        // Clean up
        if (hCounter != IntPtr.Zero)
        {
            Marshal.Release(hCounter);
        }

        if (hQuery != IntPtr.Zero)
        {
            Marshal.Release(hQuery);
        }
    }
}

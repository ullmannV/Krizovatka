using System.Runtime.InteropServices;

namespace Krizovatka
{
    public class K8055N
    {
        [DllImport("k8055d.dll")]
        public static extern int OpenDevice(int CardAddress);
        [DllImport("k8055d.dll")]
        public static extern void CloseDevice();
        [DllImport("k8055d.dll")]
        public static extern int ReadAnalogChannel(int Channel);
        [DllImport("k8055d.dll")]
        public static extern void ReadAllAnalog(ref int Data1, ref int Data2);
        [DllImport("k8055d.dll")]
        public static extern void OutputAnalogChannel(int Channel, int Data);
        [DllImport("k8055d.dll")]
        public static extern void OutputAllAnalog(int Data1, int Data2);
        [DllImport("k8055d.dll")]
        public static extern void ClearAnalogChannel(int Channel);
        [DllImport("k8055d.dll")]
        public static extern void SetAllAnalog();
        [DllImport("k8055d.dll")]
        public static extern void ClearAllAnalog();
        [DllImport("k8055d.dll")]
        public static extern void SetAnalogChannel(int Channel);
        [DllImport("k8055d.dll")]
        public static extern void WriteAllDigital(int Data);
        [DllImport("k8055d.dll")]
        public static extern void ClearDigitalChannel(int Channel);
        [DllImport("k8055d.dll")]
        public static extern void ClearAllDigital();
        [DllImport("k8055d.dll")]
        public static extern void SetDigitalChannel(int Channel);
        [DllImport("k8055d.dll")]
        public static extern void SetAllDigital();
        [DllImport("k8055d.dll")]
        public static extern bool ReadDigitalChannel(int Channel);
        [DllImport("k8055d.dll")]
        public static extern int ReadAllDigital();
        [DllImport("k8055d.dll")]
        public static extern int ReadCounter(int CounterNr);
        [DllImport("k8055d.dll")]
        public static extern void ResetCounter(int CounterNr);
        [DllImport("k8055d.dll")]
        public static extern void SetCounterDebounceTime(int CounterNr, int
        DebounceTime);
        [DllImport("k8055d.dll")]
        public static extern int Version();
        [DllImport("k8055d.dll")]
        public static extern int SearchDevices();
        [DllImport("k8055d.dll")]
        public static extern int SetCurrentDevice(int lngCardAddress);
    }
}

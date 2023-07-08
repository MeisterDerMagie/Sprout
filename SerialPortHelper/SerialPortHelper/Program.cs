using System;
using SerialPortHelper;

namespace SerialPortHelper {
internal static class Program
{
    public static void Main(string[] args)
    {
        foreach (PortsUtility.SerialPortInfo portInfo in PortsUtility.GetAvailablePorts())
        {
            Console.WriteLine($"{portInfo.Name}");
        }
    }
}
}
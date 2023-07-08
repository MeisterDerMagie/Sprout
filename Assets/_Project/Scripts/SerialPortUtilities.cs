//(c) copyright by Martin M. Klöckener
using System.Diagnostics;
using System.IO;
using UnityEngine;
using Wichtel.Utilities;

public static class SerialPortUtilities
{
    public static string[] GetSerialPortsInfos()
    {
        string fileName = "SerialPortHelper.exe";
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        var listPortsApp = new ProcessStartInfo
        {
            FileName = filePath,
            Arguments = "",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true
        };

        string consoleOutput;
        using (Process process = Process.Start(listPortsApp))
        {
            using (StreamReader reader = process.StandardOutput)
            {
                consoleOutput = reader.ReadToEnd();
            }
        }

        return StringUtilities.SplitAtLineBreaks(consoleOutput);
    }
}
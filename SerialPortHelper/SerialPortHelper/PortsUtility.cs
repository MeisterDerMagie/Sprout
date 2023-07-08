//(c) copyright by Martin M. Klöckener
using System;
using System.Collections.Generic;
using System.Management;
using System.Runtime.Versioning;
using Microsoft.Win32;

namespace SerialPortHelper {
//https://stackoverflow.com/questions/2837985/getting-serial-port-information
public static class PortsUtility
{
    public struct SerialPortInfo
    {
        public int Availability { get; }
        public string Caption { get; } //Caption = description
        public string ClassGuid { get; }
        public string[] CompatibleID { get; }
        public int ConfigManagerErrorCode { get; }
        public bool ConfigManagerUserConfig { get; }
        public string CreationClassName { get; }
        public string Description { get; }
        public string DeviceID { get; }
        public bool ErrorCleared { get; }
        public string ErrorDescription { get; }
        public string[] HardwareID { get; }
        public DateTime InstallDate { get; }
        public int LastErrorCode { get; }
        public string Manufacturer { get; }
        public string Name { get; }
        public string PNPClass { get; }
        public string PNPDeviceID { get; }
        public int[] PowerManagementCapabilities { get; }
        public bool PowerManagementSupported { get; }
        public bool Present { get; }
        public string Service { get; }
        public string Status { get; }
        public int StatusInfo { get; }
        public string SystemCreationClassName { get; }
        public string SystemName { get; }

        public SerialPortInfo(ManagementObject property)
        {
            Availability = property.GetPropertyValue("Availability") as int? ?? 0;
            Caption = property.GetPropertyValue("Caption") as string ?? string.Empty;
            ClassGuid = property.GetPropertyValue("ClassGuid") as string ?? string.Empty;
            CompatibleID = property.GetPropertyValue("CompatibleID") as string[] ?? new string[] { };
            ConfigManagerErrorCode = property.GetPropertyValue("ConfigManagerErrorCode") as int? ?? 0;
            ConfigManagerUserConfig = property.GetPropertyValue("ConfigManagerUserConfig") as bool? ?? false;
            CreationClassName = property.GetPropertyValue("CreationClassName") as string ?? string.Empty;
            Description = property.GetPropertyValue("Description") as string ?? string.Empty;
            DeviceID = property.GetPropertyValue("DeviceID") as string ?? string.Empty;
            ErrorCleared = property.GetPropertyValue("ErrorCleared") as bool? ?? false;
            ErrorDescription = property.GetPropertyValue("ErrorDescription") as string ?? string.Empty;
            HardwareID = property.GetPropertyValue("HardwareID") as string[] ?? new string[] { };
            InstallDate = property.GetPropertyValue("InstallDate") as DateTime? ?? DateTime.MinValue;
            LastErrorCode = property.GetPropertyValue("LastErrorCode") as int? ?? 0;
            Manufacturer = property.GetPropertyValue("Manufacturer") as string ?? string.Empty;
            Name = property.GetPropertyValue("Name") as string ?? string.Empty;
            PNPClass = property.GetPropertyValue("PNPClass") as string ?? string.Empty;
            PNPDeviceID = property.GetPropertyValue("PNPDeviceID") as string ?? string.Empty;
            PowerManagementCapabilities =
                property.GetPropertyValue("PowerManagementCapabilities") as int[] ?? new int[] { };
            PowerManagementSupported = property.GetPropertyValue("PowerManagementSupported") as bool? ?? false;
            Present = property.GetPropertyValue("Present") as bool? ?? false;
            Service = property.GetPropertyValue("Service") as string ?? string.Empty;
            Status = property.GetPropertyValue("Status") as string ?? string.Empty;
            StatusInfo = property.GetPropertyValue("StatusInfo") as int? ?? 0;
            SystemCreationClassName = property.GetPropertyValue("SystemCreationClassName") as string ?? string.Empty;
            SystemName = property.GetPropertyValue("SystemName") as string ?? string.Empty;
        }
    }

    public static List<SerialPortInfo> GetAvailablePorts()
    {
        var portInfos = new List<SerialPortInfo>();

        using (var entity = new ManagementClass("Win32_PnPEntity"))
        {
            foreach (ManagementBaseObject managementBaseObject in entity.GetInstances())
            {
                var instance = (ManagementObject)managementBaseObject;
                object guid = instance.GetPropertyValue("ClassGuid");
                if (guid == null || guid.ToString().ToUpper() != "{4D36E978-E325-11CE-BFC1-08002BE10318}")
                    continue; // Skip all devices except device class "PORTS"

                var portInfo = new SerialPortInfo(instance);
                portInfos.Add(portInfo);

                /*
                string caption = instance.GetPropertyValue("Caption").ToString(); //caption = description
                string manufacturer = instance.GetPropertyValue("Manufacturer").ToString();
                string deviceID = instance.GetPropertyValue("PnpDeviceID").ToString();
                string regPath = "HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Enum\\" + deviceID +
                                 "\\Device Parameters";
                string portName = Registry.GetValue(regPath, "PortName", "").ToString();

                int pos = caption.IndexOf(" (COM");
                if (pos > 0) // remove COM port from description
                    caption = caption.Substring(0, pos);

                Console.WriteLine("Port Name:    " + portName);
                Console.WriteLine("Description:  " + caption);
                Console.WriteLine("Manufacturer: " + manufacturer);
                Console.WriteLine("Device ID:    " + deviceID);
                */
            }
        }

        return portInfos;
    }
}
}
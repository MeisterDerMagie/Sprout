using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using Wichtel.Extensions;
using Wichtel.Utilities;


public class ArduinoConnection : MonoBehaviour
{
    private SerialPort _dataStream;
    private bool _isStreaming = false;
    [SerializeField] private int readTimeout = 50;

    public static event Action<string> onNewMessage = delegate(string message) {  };
    public static event Action<string> onNewArduinoError = delegate(string errorMessage) {  };

    private void Start()
    {
        string serialPort = string.Empty;
        
        //try to automatically get the right COM port
        bool foundArduinoPort = false;
        #if UNITY_STANDALONE_WIN
        try
        {
            string[] portInfos = SerialPortUtilities.GetSerialPortsInfos();
            foreach (string portInfo in portInfos)
            {
                if (!portInfo.Contains("Arduino") || !portInfo.Contains("COM")) continue;
                
                int comNumber = int.MinValue;
                string textAfterCom = portInfo.TextAfter("COM");
                for (int i = textAfterCom.Length - 1; i >= 1; i--)
                {
                    string substring = textAfterCom.Substring(0, i);
                    int.TryParse(substring, out comNumber);
                }
                    
                //if was able to parse com number
                if (comNumber != int.MinValue)
                {
                    serialPort = $"COM{comNumber}";
                    foundArduinoPort = true;
                    Debug.Log($"Found Arduino at port {serialPort}");
                }
            }
        }
        catch (Exception e)
        {
            // ignored
        }
        #endif

        //if it wasn't possible to find the arduino port, use the one from the streaming assets instead
        if(!foundArduinoPort) serialPort = StreamingAssetsUtilities.ReadTextFile("serialPort.txt");
        
        //create data stream and open connection
        _dataStream = new SerialPort(serialPort, 9600);
        OpenConnection();
    }

    private void Update()
    {
        if (!_isStreaming) return;
        
        string value = ReadSerialPort();
        if (value != null)
        {
            onNewMessage?.Invoke(value);
        }
    }

    private void OnDestroy()
    {
        _dataStream.Close();
    }

    public void SendMessageToArduino(string message)
    {
        _dataStream.WriteLine(message);
    }

    
    private void OpenConnection()
    {
        try
        {
            _dataStream.ReadTimeout = readTimeout;
            _dataStream.Open();
            _isStreaming = true;
        }
        catch (Exception exception)
        {
            Debug.LogError($"ArduinoError: {exception}");
            onNewArduinoError?.Invoke(exception.Message);
        }
    }

    private string ReadSerialPort()
    {
        try
        {
            _dataStream.ReadTimeout = readTimeout;
            string message = _dataStream.ReadLine();
            return message;
        }
        catch (TimeoutException timeoutException)
        {
            //TimeoutException isn't an error in this case because the Arduino doesn't respond as often as every frame
            return null;
        }
        catch (Exception exception)
        {
            Debug.LogError($"ArduinoError: {exception}");
            onNewArduinoError?.Invoke(exception.Message);
            return null;
        }
    }
}

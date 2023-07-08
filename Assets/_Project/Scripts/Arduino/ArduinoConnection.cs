using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
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
        string serialPort = StreamingAssetsUtilities.ReadTextFile("serialPort.txt");
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
        _isStreaming = true;
        _dataStream.ReadTimeout = readTimeout;
        _dataStream.Open();
    }

    private string ReadSerialPort()
    {
        _dataStream.ReadTimeout = readTimeout;

        try
        {
            string message = _dataStream.ReadLine();
            return message;
        }
        catch (TimeoutException exception)
        {
            return null;
        }
    }
}

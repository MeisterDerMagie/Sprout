using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class ArduinoConnection : MonoBehaviour
{
    private readonly SerialPort _dataStream = new SerialPort("COM5", 9600);
    private bool _isStreaming = false;
    [SerializeField] private int readTimeout = 100;

    private void Start()
    {
        OpenConnection();
    }

    private void Update()
    {
        if (_isStreaming)
        {
            string value = ReadSerialPort();
            if (value != null)
            {
                Debug.Log(value);
            }
        }
    }

    private void OnDestroy()
    {
        _dataStream.Close();
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

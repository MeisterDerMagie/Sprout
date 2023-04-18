using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class UnityToArduino : MonoBehaviour
{
    private readonly SerialPort _dataStream = new SerialPort("COM5", 9600);
    private bool _ledOn = false;

    private void SwitchLEDState()
    {
        _ledOn = !_ledOn;
        _dataStream.WriteLine("L" + (_ledOn ? "1" : "0"));
    }
}

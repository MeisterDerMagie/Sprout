using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class ArduinoRGBLED : MonoBehaviour
{
    private readonly SerialPort _dataStream = new SerialPort("COM5", 9600);
    [SerializeField] private ArduinoConnection connection;
    [SerializeField, ReadOnly] private Color currentColor;

    private void OnApplicationQuit()
    {
        //turn the led off when the program shuts down (OnApplicationQuit is called before OnDestroy)
        SetLEDColor(Color.black);
    }

    [Button, DisableInEditorMode]
    public void SetLEDColor(Color color)
    {
        currentColor = (Color32)color;
        int r = Mathf.FloorToInt(currentColor.r * 255);
        int g = Mathf.FloorToInt(currentColor.g * 255);
        int b = Mathf.FloorToInt(currentColor.b * 255);
        SetLEDColor(r, g, b);
    }
    
    [Button, DisableInEditorMode]
    public void SetLEDColor(int r, int g, int b)
    {
        if (r > 255 || g > 255 || b > 255)
        {
            Debug.LogWarning($"The rgb values need to range from 0 to 255. The values were ({r}, {g}, {b}).", this);
            return;
        }

        float red = r / 255f;
        float green = g / 255f;
        float blue = b / 255f;
        currentColor = new Color(red, green, blue);
        
        connection.SendMessageToArduino("R" + r);
        connection.SendMessageToArduino("G" + g);
        connection.SendMessageToArduino("B" + b);
    }
}

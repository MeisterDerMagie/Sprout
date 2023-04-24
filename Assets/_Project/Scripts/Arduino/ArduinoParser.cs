using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArduinoParser : MonoBehaviour
{
    private void Start()
    {
        ArduinoConnection.onNewMessage += OnNewArduinoMessage;
    }

    private void OnDestroy()
    {
        ArduinoConnection.onNewMessage -= OnNewArduinoMessage;
    }

    private void OnNewArduinoMessage(string message)
    {
        Debug.Log("Arduino message: " + message);
        
        //soil humidity
        //if(message)
        
        //air temperature
        
        
        //air humidity
        
        
    }
}

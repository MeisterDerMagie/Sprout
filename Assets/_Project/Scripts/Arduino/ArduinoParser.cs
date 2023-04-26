using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using Wichtel;

public class ArduinoParser : MonoBehaviour
{
    [SerializeField] private float zeroPercentSoilHumidity, hundredPercentSoilHumidity;
    public static event Action<float, float> onNewSoilHumidityValue = delegate(float absoluteValue, float percentValue) {  };
    public static event Action<float> onNewAirTemperatureValue = delegate(float newValue) {  };
    public static event Action<float> onNewAirHumidityValue = delegate(float newValue) {  };
    
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
        //Debug.Log("Arduino message: " + message);
        
        //soil humidity
        if (TryReadValue(message, "soilhum", out float soilHumidity))
        {
            float percentValue = MathW.Remap(soilHumidity, zeroPercentSoilHumidity, hundredPercentSoilHumidity, 0f, 1f);
            onNewSoilHumidityValue?.Invoke(soilHumidity, percentValue);
        }

        //air temperature
        if (TryReadValue(message, "airtmp", out float airTemperature))
        {
            onNewAirTemperatureValue?.Invoke(airTemperature);
        }
        
        //air humidity
        if (TryReadValue(message, "airhum", out float airHumidity))
        {
            onNewAirHumidityValue?.Invoke(airHumidity);
        }
    }

    //values must be sent in the format like airtemperature:25.6
    //where airtemperature is the valueName
    private bool TryReadValue(string arduinoMessage, string valueName, out float value)
    {
        if (!arduinoMessage.StartsWith(valueName))
        {
            value = float.MinValue;
            return false;
        }
        
        int index = arduinoMessage.IndexOf(':');
        if (index >= 0)
        {
            string stringValue = arduinoMessage.Substring(index + 1);
            
            var style = NumberStyles.AllowDecimalPoint;
            var culture = CultureInfo.InvariantCulture;
            
            return float.TryParse(stringValue, style, culture, out value);
        }
        
        //if it fails, return false
        value = float.MinValue;
        return false;
    }
}

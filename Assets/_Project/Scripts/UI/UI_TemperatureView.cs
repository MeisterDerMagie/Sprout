using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_TemperatureView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI temperatureViewTextField;
    
    private void Start() => ArduinoParser.onNewAirTemperatureValue += OnNewAirTemperatureValue;
    private void OnDestroy() => ArduinoParser.onNewAirTemperatureValue -= OnNewAirTemperatureValue;

    private void OnNewAirTemperatureValue(float airTemperature)
    {
        temperatureViewTextField.SetText($"Temperature {airTemperature.ToString("0")}°C");
    }
}

//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class UI_HumidityView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI humidityViewTextField;
    
    private void Start() => ArduinoParser.onNewSoilHumidityValue += OnNewSoilHumidityValue;
    private void OnDestroy() => ArduinoParser.onNewSoilHumidityValue -= OnNewSoilHumidityValue;

    private void OnNewSoilHumidityValue(float absoluteSoilHumidity, float percentSoilHumidity)
    {
        if (percentSoilHumidity > 1f)
            percentSoilHumidity = 1f;
        
        humidityViewTextField.SetText($"Humidity {(percentSoilHumidity * 100).ToString("0")}%");
    }
}
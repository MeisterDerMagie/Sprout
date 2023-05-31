using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class UI_DisplayIdealConditions : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI idealTemperature, idealHumidity;
    
    private void Start() => GameFlow.onRestart += OnRestart;
    private void OnDestroy() => GameFlow.onRestart -= OnRestart;

    private void OnRestart(Plant plant)
    {
        if (plant == null)
        {
            idealTemperature.SetText("- °C");
            idealHumidity.SetText("- %");
            return;
        }
        
        string idealTemperatureInfo = $"{plant.idealTemperatureRange.minimum.ToString(CultureInfo.InvariantCulture)}-{plant.idealTemperatureRange.maximum.ToString(CultureInfo.InvariantCulture)} °C";
        string idealHumidityInfo = $"{(plant.idealSoilHumidityRange.minimum * 100).ToString(CultureInfo.InvariantCulture)}-{(plant.idealSoilHumidityRange.maximum * 100).ToString(CultureInfo.InvariantCulture)} %";
        idealTemperature.SetText(idealTemperatureInfo);
        idealHumidity.SetText(idealHumidityInfo);
    }
}

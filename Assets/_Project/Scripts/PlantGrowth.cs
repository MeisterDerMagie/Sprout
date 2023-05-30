using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using Sirenix.OdinInspector;
using UnityEngine;
using Wichtel;

public class PlantGrowth : MonoBehaviour
{
    [Title("References")]
    [SerializeField] private PlantGrowthVisuals visuals;
    [SerializeField] private ArduinoRGBLED led;
    [SerializeField] private GameFlow gameFlow;

    [Title("LED Colors")]
    [SerializeField] private Color idealColor;
    [SerializeField] private Color toleratedColor;
    [SerializeField] private Color stagnantColor;
    [SerializeField] private Color shrinkingColor;
    [SerializeField] private Color fullyGrownColor;

    [Title("Plant")]
    public Plant plant;
    
    [Title("Debug")]
    [SerializeField][LabelText("Current Condition")][DisplayAsString]
    private Condition _previousCondition = Condition.NONE;

    [SerializeField][DisplayAsString] private float _growth = 0f;
    private float _currentAirTemperature = 0f;
    private float _currentSoilHumidity = 0f;
    private bool fullyGrown;
    private float _ledRefreshCounter = 0f; //refresh the led every 2 seconds

    private void Start()
    {
        ArduinoParser.onNewAirTemperatureValue += OnNewAirTemperature;
        ArduinoParser.onNewSoilHumidityValue += OnNewSoilHumidity;
        GameFlow.onFullyGrown += () => fullyGrown = true;
        GameFlow.onRestart += OnRestart;
    }

    private void OnDestroy()
    {
        ArduinoParser.onNewAirTemperatureValue -= OnNewAirTemperature;
        ArduinoParser.onNewSoilHumidityValue -= OnNewSoilHumidity;
        GameFlow.onRestart -= OnRestart;

    }

    private void Update()
    {
        if (fullyGrown)
            return;
        
        bool hasIdealConditions = plant.idealTemperatureRange.ContainsValue(_currentAirTemperature) &&
                                  plant.idealSoilHumidityRange.ContainsValue(_currentSoilHumidity);

        bool hasToleratedConditions = !hasIdealConditions &&
                                      plant.toleratedTemperatureRange.ContainsValue(_currentAirTemperature) &&
                                      plant.toleratedSoilHumidityRange.ContainsValue(_currentSoilHumidity);

        bool hasStagnantConditions = !hasIdealConditions &&
                                     !hasToleratedConditions &&
                                     plant.stagnantTemperatureRange.ContainsValue(_currentAirTemperature) &&
                                     plant.stagnantSoilHumidityRange.ContainsValue(_currentSoilHumidity);

        Condition newCondition;
        
        if (hasIdealConditions)
            newCondition = Condition.Ideal;
        
        else if (hasToleratedConditions)
            newCondition = Condition.Tolerated;
        
        else if (hasStagnantConditions)
            newCondition = Condition.Stagnating;

        else
            newCondition = Condition.Shrinking;

        bool conditionChanged = _previousCondition != newCondition;
        
        //also refresh every 2 seconds
        _ledRefreshCounter += Time.deltaTime;
        if (_ledRefreshCounter >= 2f)
        {
            conditionChanged = true;
            _ledRefreshCounter = 0f;
        }

        //Grow ideal
        if (hasIdealConditions)
        {
            _growth += plant.growthSpeedIdeal / 1000f * Time.deltaTime;
            if(conditionChanged) led.SetLEDColor(idealColor);
        }
        
        //Grow tolerated
        else if (hasToleratedConditions)
        {
            _growth += plant.growthSpeedTolerated / 1000f * Time.deltaTime;
            if(conditionChanged) led.SetLEDColor(toleratedColor);
        }
        
        //Stagnate
        else if (hasStagnantConditions)
        {
            //do nothing except showing the right led color
            if(conditionChanged) led.SetLEDColor(stagnantColor);
        }

        //Shrink
        else
        {
            _growth -= plant.shrinkSpeed / 1000f * Time.deltaTime;
            if(conditionChanged) led.SetLEDColor(shrinkingColor);
        }
        
        //check if fully grown or full shrunk
        _growth = Mathf.Clamp(_growth, 0f, 1f);

        if (_growth >= 1f)
        {
            //it's fully grown, do stuff here
            newCondition = Condition.FullyGrown;
            gameFlow.FullyGrown();
            led.SetLEDColor(fullyGrownColor);
        }

        //update visuals
        visuals.UpdateVisuals(_growth);
        
        //set previous condition
        _previousCondition = newCondition;
    }

    private void OnRestart()
    {
        fullyGrown = false;
        _growth = 0f;
    }
    private void OnNewAirTemperature(float airTemperature) => _currentAirTemperature = airTemperature;
    private void OnNewSoilHumidity(float soilHumidity, float soilHumidityPercent) => _currentSoilHumidity = soilHumidityPercent;
}

public enum Condition
{
    NONE,
    Shrinking,
    Stagnating,
    Tolerated,
    Ideal,
    FullyGrown
}
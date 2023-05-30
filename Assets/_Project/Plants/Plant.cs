//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Wichtel;

[CreateAssetMenu(fileName = "Plant", menuName = "Sprout/Plant", order = 0)]
public class Plant : ScriptableObject
{
    [Title("Ideal Conditions")]
    public Range<float> idealTemperatureRange;
    public Range<float> idealSoilHumidityRange;
    
    [Title("Tolerated Conditions")]
    public Range<float> toleratedTemperatureRange;
    public Range<float> toleratedSoilHumidityRange;

    [Title("Stagnant Conditions")]
    public Range<float> stagnantTemperatureRange;
    public Range<float> stagnantSoilHumidityRange;

    [Title("Growth Speed")]
    public float growthSpeedIdeal = 25f;
    public float growthSpeedTolerated = 12.5f;
    public float shrinkSpeed = 8f;
}
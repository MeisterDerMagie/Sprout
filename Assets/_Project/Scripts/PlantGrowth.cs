using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlantGrowth : MonoBehaviour
{
    [SerializeField] private float _growth = 0f;
    [SerializeField] private float startYPos, endYPos;
    [SerializeField] private float startScale, endScale;

    private void UpdateVisuals()
    {
        
    }

    private void SetGrowth(float growth)
    {
        _growth = growth;
        UpdateVisuals();
    }
    
    private void OnValidate() => UpdateVisuals();
}

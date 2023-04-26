using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Wichtel;
using Wichtel.Extensions;

public class PlantGrowthVisuals : MonoBehaviour
{
    
    [SerializeField] private float startYPos, endYPos;
    [SerializeField] private float startScale, endScale;
    
    //Debug
    [SerializeField] [Range(0f, 1f)] private float debugGrowth;

    public void UpdateVisuals(float growth)
    {
        //y position
        float yPos = MathW.Remap(growth, 0f, 1f, startYPos, endYPos);
        transform.localPosition = transform.localPosition.With(y: yPos);

        //scale
        float scale = MathW.Remap(growth, 0f, 1f, startScale, endScale);
        transform.localScale = new Vector3(scale, scale, scale);
    }
    
    private void OnValidate() => UpdateVisuals(debugGrowth);
}

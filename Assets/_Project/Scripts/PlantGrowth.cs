using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wichtel;
using Wichtel.Extensions;

public class PlantGrowth : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] private float _growth = 0f;
    [SerializeField] private float startYPos, endYPos;
    [SerializeField] private float startScale, endScale;

    private void UpdateVisuals()
    {
        //y position
        float yPos = MathW.Remap(_growth, 0f, 1f, startYPos, endYPos);
        transform.localPosition = transform.localPosition.With(y: yPos);

        //scale
        float scale = MathW.Remap(_growth, 0f, 1f, startScale, endScale);
        transform.localScale = new Vector3(scale, scale, scale);
    }

    private void SetGrowth(float growth)
    {
        _growth = growth;
        UpdateVisuals();
    }
    
    private void OnValidate() => UpdateVisuals();
}

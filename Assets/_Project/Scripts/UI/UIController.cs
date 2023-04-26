//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private List<Transform> showWhileNotFullyGrown = new List<Transform>();
    [SerializeField] private List<Transform> showWhileFullyGrown = new List<Transform>();

    private void Start()
    {
        GameFlow.onFullyGrown += OnFullyGrown;
        GameFlow.onRestart += OnRestart;
        
        OnRestart();
    }

    private void OnDestroy()
    {
        GameFlow.onFullyGrown -= OnFullyGrown;
        GameFlow.onRestart -= OnRestart;
    }

    private void OnFullyGrown()
    {
        foreach (Transform trans in showWhileFullyGrown)
            trans.gameObject.SetActive(true);

        foreach (Transform trans in showWhileNotFullyGrown)
            trans.gameObject.SetActive(false);
    }
    
    private void OnRestart()
    {
        foreach (Transform trans in showWhileFullyGrown)
            trans.gameObject.SetActive(false);

        foreach (Transform trans in showWhileNotFullyGrown)
            trans.gameObject.SetActive(true);
    }
}
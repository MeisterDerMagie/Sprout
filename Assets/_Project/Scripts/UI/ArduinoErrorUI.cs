//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ArduinoErrorUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _errorView;

    [SerializeField]
    private TextMeshProUGUI _errorMessageTextField;

    [SerializeField]
    private TextMeshProUGUI _foundPortsTextField;
    
    private bool _errorViewIsShown;

    private void Awake()
    {
        //hide view at start of the game
        _errorView.SetActive(false);
        
        //subscribe to event
        ArduinoConnection.onNewArduinoError += OnNewArduinoError;
    }

    private void OnDestroy() => ArduinoConnection.onNewArduinoError -= OnNewArduinoError;

    private void OnNewArduinoError(string errorMessage)
    {
        //only show the message for the first error
        if (_errorViewIsShown) return;

        _errorViewIsShown = true;
        _errorView.SetActive(true);
        _errorMessageTextField.SetText(errorMessage);
        
        #if UNITY_STANDALONE_WIN
        string[] ports = SerialPortUtilities.GetSerialPortsInfos();
        string portsCombined = ports.Aggregate(string.Empty, (current, port) => current + (port + "\n"));

        _foundPortsTextField.SetText(portsCombined);
        #endif
    }
}
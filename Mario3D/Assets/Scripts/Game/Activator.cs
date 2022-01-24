using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class Activator : MonoBehaviour
{
    [SerializeField] private Transform cameraCenter;

    private void OnTriggerEnter(Collider other){
        other.GetComponentInParent<IActivatorSensitive>()?.TurnOn();
    }

    private void OnTriggerExit(Collider other){
        other.GetComponentInParent<IActivatorSensitive>()?.TurnOff();
    }
}

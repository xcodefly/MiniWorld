using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[System.Serializable]
public enum LightStatus { off,marker,on}
public class VehicleAction : MonoBehaviour
{
    // Start is called before the first frame update
    public static Action<bool> OnVehicleStart;
    public static Action<bool> OnVehicleLight;



    public EngineStatus engineStatus;
    public bool running;
    [SerializeField] LightStatus lightStatus;
    [SerializeField] bool marker;

    void Start()
    {
        running = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(running && engineStatus == EngineStatus.Off)
        {
            OnVehicleStart?.Invoke(running);
            engineStatus = EngineStatus.Running;
        }
        if (!running && engineStatus == EngineStatus.Running)
        {
            OnVehicleStart?.Invoke(running);
            engineStatus = EngineStatus.Off;
        }

        if(marker && lightStatus !=LightStatus.marker)
        {
            OnVehicleLight?.Invoke(marker);
            lightStatus = LightStatus.marker;
        }

        if (!marker && lightStatus != LightStatus.off)
        {
            OnVehicleLight?.Invoke(marker);
            lightStatus = LightStatus.off;
        }

    }
}

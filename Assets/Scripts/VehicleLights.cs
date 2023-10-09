using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VehicleLights : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform brakeLights;
    [SerializeField] Material brakeLights_Prefab_Material;
    [SerializeField] Material brakeLights_Material;
    [SerializeField] float marker, brakeLight;
    private void Awake()
    {
        VehicleController.OnBrakeStatus += BrakeLights;
        VehicleAction.OnVehicleLight += MarkerLights;
    }
    void Start()
    {
     //   brakeLights_Material = new Material(brakeLights_Prefab_Material);
        brakeLights.GetComponent<Renderer>().material = brakeLights_Prefab_Material;
        brakeLights_Prefab_Material.EnableKeyword("_EmissionColor");
    }

    private void OnDisable()
    {
        VehicleController.OnBrakeStatus -= BrakeLights;
    }

    void MarkerLights(LightStatus _status)
    {
        if (_status>0)
        {
            marker = 0.5f;
        }
        else
        {
            marker = 0;
        }
        brakeLights_Prefab_Material.SetColor("_EmissionColor", new Color(1f, 0, 0) * (brakeLight + marker));
    }
    void BrakeLights (bool _status)
    {
        if(_status)
        {
            brakeLight = 5;
        }else
        {
            brakeLight = 0;
        }            
        brakeLights_Prefab_Material.SetColor("_EmissionColor", new Color(1f, 0, 0) * (brakeLight+marker));
       
    }
}

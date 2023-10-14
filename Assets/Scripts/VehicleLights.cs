using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using NUnit.Framework.Constraints;

public class VehicleLights : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform brakeLights;
    [SerializeField] Transform headLights;
    [SerializeField] Material brakeLights_Prefab_Material;
   
    [SerializeField] Material brakeLights_Material;
    [SerializeField] Material headLight_Material;


    [SerializeField] float marker, brakeLight,lights;
    private void Awake()
    {
        VehicleController.OnBrakeStatus += BrakeLights;
        VehicleAction.OnVehicleLight += LightsUpdate;
    }
    void Start()
    {
        brakeLights_Material = new Material(brakeLights_Prefab_Material);
        brakeLights.GetComponent<Renderer>().material = brakeLights_Material;
        brakeLights_Material.EnableKeyword("_EmissionColor");
        
        
        
        headLight_Material = new Material(headLights.GetComponent<Renderer>().material);
        headLights.GetComponent<Renderer>().material = headLight_Material;
        headLight_Material.EnableKeyword("_EmissionColor");


    }

    private void OnDisable()
    {
        VehicleController.OnBrakeStatus -= BrakeLights;
        
    }

    void LightsUpdate(LightStatus _status)
    {
        switch((int)_status)

        {
            case 0:
                marker = 0;
                lights = 0.3f;
                break;
            case 1:
                marker = 0.5f;
                lights = 0.7f;
                break;
            case 2:
                marker = 0.5f;
                lights = 7f;
                break;

        }
        headLight_Material.SetColor("_EmissionColor", new Color(1, 1, 1) * (lights));
        brakeLights_Material.SetColor("_EmissionColor", new Color(1f, 0, 0) * (brakeLight + marker));

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

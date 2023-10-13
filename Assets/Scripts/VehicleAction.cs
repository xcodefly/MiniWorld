using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

[System.Serializable]
public enum LightStatus { off,marker,on}
public class VehicleAction : MonoBehaviour
{
    // Start is called before the first frame update
    public static Action<bool> OnVehicleStart;
    public static Action<LightStatus> OnVehicleLight;
    


    public EngineStatus engineStatus;
    public bool running;
    [SerializeField] LightStatus stauts_Light;

    void Start()
    {
        OnVehicleStart?.Invoke(running);  
    }

    // Update is called once per frame
    void Update()
    {
        


        if (Input.GetButtonDown("Engine"))
        {
            running = !running; 
            OnVehicleStart?.Invoke(running);

        }
        if(Input.GetButtonDown("Light"))
        {
            int temp = (int)stauts_Light + 1;
            if(temp< Enum.GetValues(typeof(LightStatus)).Length)
            {
                stauts_Light = (LightStatus)temp;
            }else
            {
                temp = 0;
                stauts_Light = (LightStatus)temp;
            }            
            OnVehicleLight?.Invoke(stauts_Light); 
        


        }

      

    }
}

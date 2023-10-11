using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PowerTrain : MonoBehaviour
{
    // Start is called before the first frame update
    public static Action<int> OnGearChange;
    

    // Varaibles.
    public float engineTorque;
    public float engiebrakeTroque;
    public float wheelTorque;
    public float RPMLimit;
    public float engineRPM;
    public float[] gearRatio;
    public float finalGearRatio;
    public float reversRatio;
    public AnimationCurve engineCurve;
    public AnimationCurve engineBrake;
    public int gear;

    public PlayerInput playerInput;
    public VehicleController vehicleController;
    public float rpmRatio;
    public float multiplier;
    
    void Start()
    {
        vehicleController=GetComponent<VehicleController> ();

    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetButtonDown("GearUp"))
        {
            if(gear+1<gearRatio.Length)
            {
                gear++;
            }
            UpdateGear();
           
        }
        if(Input.GetButtonDown("GearDown"))
        {
            if (gear - 1 >= -1)
            {
                gear--;
            }
            UpdateGear();
        }
        engineRPM =    Mathf.Lerp(engineRPM,      (vehicleController.wheelRPM ) * multiplier,Time.deltaTime*25) ;
        rpmRatio = engineRPM / RPMLimit;
        wheelTorque = (playerInput.throttle * engineTorque * engineCurve.Evaluate(rpmRatio) + engineBrake.Evaluate(rpmRatio)*engiebrakeTroque ) * multiplier;

        vehicleController.torque = wheelTorque;
    }

    public void UpdateGear()
    {
        if (gear >= 0)
        {
            multiplier = gearRatio[gear] * finalGearRatio;
        }
        else
        {
            multiplier = reversRatio * finalGearRatio;
        }
        OnGearChange?.Invoke(gear);
    }
}

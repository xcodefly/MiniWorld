using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[System.Serializable]
public class Gear
{
    public float speedMultiplier, torqueMultiplier;

}
public class PowerTrain : MonoBehaviour
{
    // Start is called before the first frame update
    public static Action<int> OnGearChange;
    

    // Varaibles.
    public float engineTorque;    
    public float wheelTorque;

    public Gear[] gears;
    public float finalGearRatio;
    public float reversRatio;  
    public int gear;
    public PlayerInput playerInput;
    public VehicleController vehicleController;
    public float engineRPM;
    
    public float torqueMultiplier,speedMultiplier;
    
    void Start()
    {
        vehicleController=GetComponent<VehicleController> ();
        engineTorque = 0;

    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetButtonDown("GearUp"))
        {
            if(gear+1<gears.Length)
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
        
    }

    private void FixedUpdate()
    {
        engineRPM = vehicleController.wheelRPM * speedMultiplier;
        wheelTorque = engineTorque * torqueMultiplier;
        vehicleController.torque = wheelTorque;
    }
    public void UpdateGear()
    {
        if (gear >= 0)
        {
            torqueMultiplier = gears[gear].torqueMultiplier * finalGearRatio;
            speedMultiplier = gears[gear].speedMultiplier * finalGearRatio;
        }
        else
        {
            
        }
        OnGearChange?.Invoke(gear);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[System.Serializable]

public class VehicleController : MonoBehaviour
{   
    // Actions.
    public static Action<int> OnGearChange;
    public static Action<bool> OnBrakeStatus;
    // Start is called before the first frame update
    [SerializeField] WheelCollider [] steeringWheels;
    [SerializeField] WheelCollider [] driveWheels;
    [SerializeField] WheelCollider [] allwheels;
    [SerializeField] WheelInfo[] wheelInfo;
    [SerializeField]float maxSteerAngle;
    [SerializeField] float torque,brakeTorque;
    [SerializeField] Transform cog;
    [SerializeField] Rigidbody rb;
    [SerializeField] Movement movement;
    
    public float engineRPM;
    
    [SerializeField]int gear;
    [Header("Vehicle Paraemter")]
    public float throttle, brake;
    public float wheelSpeed;
    public float airspeed;
    public float ratio;
    public bool brakes;
    public bool running;
    private void Awake() {

        rb=GetComponent<Rigidbody>();
        if(cog!=null)
        {
            rb.centerOfMass=cog.localPosition;
        }    
        OnGearChange.Invoke(gear);
    }
    void Start()
    {
        VehicleAction.OnVehicleStart += VehicleStart;


        allwheels = GetComponentsInChildren<WheelCollider>();
        wheelInfo = new WheelInfo[allwheels.Length];
        int i = 0;
        foreach(WheelCollider wheels in allwheels)
        {
            wheels.ConfigureVehicleSubsteps(0.1f,5,10);
            wheelInfo[i]=    wheels.GetComponent<WheelInfo>();
            i++;    
        }
    }
    private void OnDisable()
    {
        VehicleAction.OnVehicleStart -= VehicleStart;
    }
    private void VehicleStart(bool _status)
    {
        running = _status;
    }
    // Update is called once per frame
    void Update()
    {

        if(Input.GetButtonDown("Reverse"))
        {
            gear=gear*-1;
            OnGearChange.Invoke(gear);
        }
        if(movement!=null)
        {
            if(movement.vertical>0)
            {
                throttle=movement.vertical;
                brake=0;               
            }else
            {
                throttle=0;
                brake=-movement.vertical;
            }
            if(brake>0 && !brakes)
            {
                brakes=true;
                OnBrakeStatus?.Invoke(brakes);
                
            }else if(brake==0 && brakes) 
            {
                brakes=false;
                OnBrakeStatus?.Invoke(brakes);
            }           
        }
        airspeed=Vector3.Dot(rb.velocity,this.transform.forward);
        ratio=wheelSpeed/airspeed;
    }

    private void FixedUpdate() {
        if(movement !=null)
        {
            foreach(WheelCollider wheel in steeringWheels)
            {
                wheel.steerAngle=movement.horizontal*maxSteerAngle;
            }
            engineRPM = 0;
            if(running)
            {
                foreach (WheelCollider wheel in driveWheels)
                {
                    engineRPM = engineRPM + wheel.rpm;
                    wheel.motorTorque = gear * throttle * torque / driveWheels.Length;
                }
            }           
            wheelSpeed = engineRPM  * 2 * Mathf.PI * driveWheels[0].radius / driveWheels.Length * Time.fixedDeltaTime;
            float brakeForce = brake * brakeTorque;
            foreach (WheelInfo wheel in wheelInfo)
            {
                wheel.SetBrake(brakeForce); 
            }       
        }
    }    
}

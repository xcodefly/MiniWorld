using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[System.Serializable]

public class VehicleController : MonoBehaviour
{   
    // Actions.
 
    public static Action<bool> OnBrakeStatus;

    // Start is called before the first frame update
    [SerializeField] WheelCollider [] steeringWheels;
    [SerializeField] WheelCollider [] driveWheels;
    [SerializeField] WheelCollider [] allwheels;
    [SerializeField] WheelInfo[] wheelInfo;
    [SerializeField]float maxSteerAngle;
    public float torque,brakeTorque;
    [SerializeField] Transform cog;
    [SerializeField] Rigidbody rb;
    [SerializeField] PlayerInput playerInput;
    
  
    
    [SerializeField]int gear;
    [Header("Vehicle Paraemter")]
    public float brake;
    public float wheelRPM;
   
    public float airspeed;
  
    public bool brakeLights;
    public bool running;
    private void Awake() {

        rb=GetComponent<Rigidbody>();
        if(cog!=null)
        {
            rb.centerOfMass=cog.localPosition;
        }    
      
    }
    void Start()
    {
        
        allwheels = GetComponentsInChildren<WheelCollider>();
        wheelInfo = new WheelInfo[allwheels.Length];
        int i = 0;
        foreach(WheelCollider wheels in allwheels)
        {
            wheels.ConfigureVehicleSubsteps(0.1f,5,10);
            wheelInfo[i]=wheels.GetComponent<WheelInfo>();
            i++;    
        }
    }
  
    private void VehicleStart(bool _status)
    {
        running = _status;
    }
    // Update is called once per frame
    void Update()
    {
        airspeed = Vector3.Dot(rb.velocity,this.transform.forward);
        brake = playerInput.brake;
        if(brake>0 && !brakeLights)
        {
            brakeLights = true;
            OnBrakeStatus?.Invoke(brakeLights);
        }else if (brakeLights && brake == 0)
        {
            brakeLights = false;
            OnBrakeStatus?.Invoke(brakeLights);           
        }
    }

    private void FixedUpdate() {
        if(playerInput !=null)
        {
            foreach(WheelCollider wheel in steeringWheels)
            {
                wheel.steerAngle=playerInput.steer*maxSteerAngle;
            }
            wheelRPM = 0;
            foreach (WheelCollider wheel in driveWheels)
            {
                wheelRPM = wheelRPM + wheel.rpm;
                wheel.motorTorque = torque / driveWheels.Length;
            }
            wheelRPM=wheelRPM/driveWheels.Length;
        }           
         //   wheelSpeed = engineRPM  * 2 * Mathf.PI * driveWheels[0].radius / driveWheels.Length * Time.fixedDeltaTime;
        float brakeForce = brake * brakeTorque;
        foreach (WheelInfo wheel in wheelInfo)
        {
            wheel.SetBrake(brakeForce); 
        }       
       
    }    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] WheelCollider [] steeringWheels;
    [SerializeField] WheelCollider [] driveWheels;
    [SerializeField] WheelCollider [] allwheels;
    [SerializeField]float maxSteerAngle;
    [SerializeField] float torque,brakeTorque;
    [SerializeField] Transform cog;
    [SerializeField] Rigidbody rb;
    [SerializeField] Movement movement;
   



    [SerializeField]float throttle,brake,gear;
    [Header("Vehicle Paraemter")]
    public float wheelSpeed;
    public float airspeed;
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
        foreach(WheelCollider wheels in allwheels)
        {
            wheels.ConfigureVehicleSubsteps(0.1f,5,10);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(movement!=null)
        {
            if(gear>=0)
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
            }else
            {
                if(movement.vertical<0)
                {
                    throttle=-movement.vertical;
                    brake=0;
                }else
                {
                    throttle=0;
                    brake=movement.vertical;
                }
            }
        }
        airspeed=rb.velocity.z*3.6f;
    }

    private void FixedUpdate() {
        if(movement !=null)
        {
            foreach(WheelCollider wheel in steeringWheels)
            {
                wheel.steerAngle=movement.horizontal*maxSteerAngle;
            }

            
            foreach(WheelCollider wheel in driveWheels)
            {
                wheel.motorTorque=gear*throttle*torque/driveWheels.Length;
            }
        
        
            foreach(WheelCollider wheel in allwheels)
            {
                wheel.brakeTorque=brake*brakeTorque;
            }
       
        }
       
        


    }

    
}

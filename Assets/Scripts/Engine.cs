using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Properties;
using UnityEngine;

public class Engine : MonoBehaviour
{
    // Start is called before the first frame update


    public float maxRPM;
    public float idleRPM;
    public float setRPM;
    public float allowedRPM;
    [Header("RPM control PID")]
    public float rpm;
    public PID engineController;

    [Header("Engine Parameters")]
    public float engineDrag;
    public float w;
    public PowerTrain powerTrain;
    public AnimationCurve slipCurve;
    public float maxSlip;




    [Header("Speed Control")]

    public PID idleManager;

    [Header("Theromodynamic")]
    public float maxTorque;
    public float torque,idleTorque;
    public float a;
    public float rpmT;
    public float targetRPMT;
    public float angularVelocity;
    public float i;
    public float workdone;
    
    public float fuelBurn; 
    public float radius, mass, eDrag;
    public AnimationCurve pumpingLosses;


    
    private void Awake()
    {
        VehicleAction.OnVehicleStart += StartVehicle;
        maxSlip=4000;
        i = mass * radius * radius*0.5f;
    }
    void Start()
    {
        powerTrain = GetComponent<PowerTrain>();
    }
    void StartVehicle(bool _status)
    {
        if(_status )
        {
            idleRPM = 750;
            eDrag = 300;
            maxTorque = 300;
        }
        else
        {
            idleRPM = 0;
            maxTorque = 0;
            StartCoroutine(ShutdownEngine());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        setRPM = idleRPM + PlayerInput.instance.throttle * (maxRPM - idleRPM);
        allowedRPM =powerTrain.engineRPM +  slipCurve.Evaluate(powerTrain.engineRPM/maxRPM)*maxSlip;       
        if(powerTrain.gear!=0)
        {
            setRPM=Mathf.Min(allowedRPM,Mathf.Max(powerTrain.engineRPM*0.85f,setRPM));
        }
        engineDrag = engineController.Update(setRPM, rpm , Time.deltaTime);
        w = w + engineDrag * Time.deltaTime;
        rpm = w * 60 / (2 * Mathf.PI);
        // Simulation.
        // 

        // v = u+at;
        // v2- u2= 2as;
        
        idleTorque =Mathf.Max(0, idleManager.Update(idleRPM, rpmT, Time.deltaTime));
        torque = PlayerInput.instance.throttle * maxTorque;
        
        torque = torque + idleTorque;
        
        if(rpmT>maxRPM)
        {
            torque = 0;
        }
        workdone = Mathf.Lerp(workdone, torque * angularVelocity * Time.deltaTime *3600/ 33526,Time.deltaTime);
       
        i = mass * radius * radius * 0.5f;
        a = torque / i;

        angularVelocity = angularVelocity + (a-eDrag*pumpingLosses.Evaluate(rpmT/maxRPM))*Time.deltaTime ;
        rpmT = angularVelocity * 60 / (2* Mathf.PI);

    }

    IEnumerator ShutdownEngine()
    {
        while(angularVelocity>0) {
            yield return null;
        }
        eDrag = 0;
        angularVelocity = 0;
    }
    
}

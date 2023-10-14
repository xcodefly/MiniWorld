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

   
   

    [Header("Engine Parameters")]
    public PowerTrain powerTrain;
    public AnimationCurve slipCurve;
    public AnimationCurve engineTorque;
    public float slipValue;
    public float slipVelocity;
    public float maxSlip;

    [Header("Speed Control")]
    public PID idleManager;

    [Header("Theromodynamic")]
    public float maxTorque;
    public float torque,idleTorque;
    public float a;
    public float rpm;
    public float targetRPMT;
    public float engineVelocity;
    public float powerTrainVelocity;
    public float i;
    public float workdone;    
    public float fuelBurn; 
    public float radius, mass, eDrag;
    public AnimationCurve pumpingLosses;
    
    private void Awake()
    {
        VehicleAction.OnVehicleStart += StartVehicle;
        maxSlip=200;
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
            idleRPM = 850;
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
    void FixedUpdate()
    {   // Simulation.
        // v = u+at;
        // v2- u2= 2as;

        torque = PlayerInput.instance.throttle * maxTorque;
             
        if(rpm>maxRPM)
        {
            torque = 0;
        }

        powerTrain.engineTorque = engineTorque.Evaluate(rpm / maxRPM) * torque;            
        idleTorque = Mathf.Max(0, idleManager.Update(idleRPM, rpm, Time.deltaTime));
        torque = torque + idleTorque;   
        a =torque / i;
        engineVelocity = engineVelocity + (a-eDrag*pumpingLosses.Evaluate(rpm/maxRPM))*Time.deltaTime ;
        powerTrainVelocity = powerTrain.engineRPM / 9.544f;        
        slipValue = slipCurve.Evaluate(rpm/maxRPM);
        slipVelocity = maxSlip * slipValue ; 
        if(powerTrain.gear>0)
        {
            powerTrainVelocity = powerTrain.engineRPM / 9.544f;
            engineVelocity = Mathf.Lerp(engineVelocity, powerTrainVelocity + slipVelocity, PlayerInput.instance.throttle);
            engineVelocity = Mathf.Max(engineVelocity, powerTrainVelocity);
        }
        else if (powerTrain.gear == -1)
        {
            powerTrainVelocity = - powerTrain.engineRPM / 9.544f;
            engineVelocity = Mathf.Lerp(engineVelocity, powerTrainVelocity + slipVelocity, PlayerInput.instance.throttle);
            engineVelocity = Mathf.Max(engineVelocity, powerTrainVelocity);
        }
        // Linking engine to transmission;
        rpm = (engineVelocity ) * 60 / (2* Mathf.PI);

        //    workdone = Mathf.Lerp(workdone, torque * engineVelocity * Time.deltaTime * 3600 / 33526, Time.deltaTime * 5);

    }

    IEnumerator ShutdownEngine()
    {
        while(engineVelocity >0) {
            yield return null;
        }
        eDrag = 0;
        engineVelocity = 0;
    }
    
}

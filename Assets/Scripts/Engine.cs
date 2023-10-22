using UnityEngine;
using System;
using System.Collections;

public class Engine : MonoBehaviour
{
    // Start is called before the first frame update
    public static Action<float> OnEngineTemps;

    public float maxRPM;
    public float idleRPM;
    public float horsePower;
    [Header("Engine Parameters")]
    public PowerTrain powerTrain;
    public AnimationCurve slipCurve;
    public float rpmSlip;
    public AnimationCurve engineTorque;
    [Header("Coolant Management")]
    public float specifitHeat;
    public float coolantMass;
    public float coolantTemp,coolantDelta;
    [Header("Radiater")]
    public float thermostat;
     public float radiaterCapacity;
     public float targetValue;
    [Header("Speed Control")]
    public PID idleManager;

    [Header("Theromodynamic")]
    public float maxTorque;
    public float torque;
    public float rpm;
    public float fuelBurn; 
    public float radius, mass, eDrag;
    public AnimationCurve pumpingLosses;
    
    private void Awake()
    {
        VehicleAction.OnVehicleStart += StartVehicle;
        StartCoroutine(TransmitEngineTemp());
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
       
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {   

        torque = engineTorque.Evaluate(rpm/maxRPM  )*maxTorque*  PlayerInput.instance.throttle;
       
        // allowing slip for realistic rpm jump.
        
        rpmSlip=Mathf.Lerp(rpmSlip,slipCurve.Evaluate(rpm/maxRPM)*maxRPM*PlayerInput.instance.throttle,Time.deltaTime*3);
        rpm = powerTrain.engineRPM;
        rpm=Mathf.Clamp(rpm + rpmSlip,idleRPM,maxRPM+700);
        powerTrain.engineTorque=torque;
        horsePower=torque*rpm/5252;
        thermostat = Mathf.Lerp(0,2,(coolantTemp-targetValue)/15);
      
        coolantDelta = (((horsePower+15)*3-thermostat*radiaterCapacity)  *Time.fixedDeltaTime)/(coolantMass*specifitHeat);
        coolantTemp=coolantTemp+coolantDelta;
    }

    IEnumerator TransmitEngineTemp()
    {
        while (true)
        {
            yield return new WaitForSeconds (0.5f);

            radiaterCapacity = 250 + VehicleController.instance.airspeed*4;
            OnEngineTemps?.Invoke(coolantTemp);


        }
    }
    

}

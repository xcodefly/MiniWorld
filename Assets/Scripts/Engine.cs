using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class Engine : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxRPM;
    public float idleRPM;
    public float setRPM;
    public float allowedSlip;
    [Header ("RPM control PID")]
    public float rpm;  
    public PID engineController;

    [Header("Engine Parameters")]
    public float flywheelMass;
    public float radius;
    public float movementInertia;
    public float engineDrag;

    public float w;
    public VehicleController vehicle;
    public AnimationCurve slipCurve;
    private void Awake()
    {
        VehicleAction.OnVehicleStart += StartVehicle;
    }
    void Start()
    {
        vehicle = GetComponent<VehicleController>();
        movementInertia = flywheelMass * radius * radius;
    }

    void StartVehicle(bool _status)
    {
        if(_status )
        {
            idleRPM = 750;            
        }
        else
        {
            idleRPM = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {

        setRPM = idleRPM + PlayerInput.instance.throttle * (maxRPM - idleRPM);
        engineDrag = engineController.Update(setRPM, rpm , Time.deltaTime);

        w = w + engineDrag * Time.deltaTime;
        rpm = w * 60 / (2 * Mathf.PI);
        

    }
}

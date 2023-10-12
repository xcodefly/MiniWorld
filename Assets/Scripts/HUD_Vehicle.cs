using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HUD_Vehicle : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]VehicleController vehicle;
    [SerializeField]Text speed_T,gear_T;
    [SerializeField] bool testMode;
    [Header ("RPM")]
    [SerializeField] RectTransform rpmNeedle;
    [SerializeField] float ratio;
    [SerializeField] float MaxAngle;
    [SerializeField] float rpmsmooth;
    [SerializeField] Engine engine;

    [Header ("Engine Parameters")]
    [SerializeField] float coolant;
    [SerializeField] float coolantSweepAngle;
    [SerializeField] RectTransform coolantNeedle;
    [SerializeField] float fuel;


    void Awake()
    {
        PowerTrain.OnGearChange += UpdateGear;

        testMode = false;
    }
    private void Start()
    {
        StartCoroutine(UpdateSpeed());
       
    }
    private void OnDisable() {
        PowerTrain.OnGearChange -= UpdateGear;
    }

    // Update is called once per frame
    void Update()
    {
        if(engine!=null)
        {
            if(!testMode)
            {
                rpmsmooth = Mathf.Lerp(rpmsmooth, engine.rpm / engine.maxRPM, Time.deltaTime * 5);
            }
           
            rpmNeedle.localEulerAngles = new Vector3(0, 0, rpmsmooth * MaxAngle);
            coolantNeedle.localEulerAngles=new Vector3(0, 0, coolant * coolantSweepAngle);
        }
                  
    }

    IEnumerator UpdateSpeed()
    {
        while(true)
        {
            speed_T.text = (Mathf.Abs(vehicle.airspeed)*3.6f).ToString("0");
            yield return new WaitForSeconds(0.1f);
        }
    }
    void UpdateGear(int _gear)
    {
        gear_T.text=_gear.ToString();
       
    }
}

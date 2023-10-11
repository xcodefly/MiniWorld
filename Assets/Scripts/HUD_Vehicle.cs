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
    [SerializeField] bool airspeed;
    [Header ("RPM")]
    [SerializeField] RectTransform rpmNeedle;
    [SerializeField] float ratio;
    [SerializeField] float MaxAngle;
    [SerializeField] float rpmsmooth;
    [SerializeField] Engine engine;


    void Awake()
    {
        PowerTrain.OnGearChange += UpdateGear;

        airspeed = true;
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
            rpmsmooth = Mathf.Lerp(rpmsmooth, engine.rpm/engine.maxRPM, Time.deltaTime *5);
            rpmNeedle.localEulerAngles = new Vector3(0, 0, rpmsmooth * MaxAngle);
        }
                  
    }

    IEnumerator UpdateSpeed()
    {
        while(true)
        {
            if(airspeed)
            {
                speed_T.text = (Mathf.Abs(vehicle.airspeed)*3.6f).ToString("0");
            }
            else
            {
            //    speed_T.text = (Mathf.Abs(vehicle.wheelSpeed) * 3.6f).ToString("0");               
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    void UpdateGear(int _gear)
    {
        gear_T.text=_gear.ToString();
       
    }
}

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
    [Range(0,1)]
    [SerializeField] float coolant;
    [Range(0,1)]
    [SerializeField] float fuel;
    [SerializeField] Slider fuelGauge;
    [SerializeField] Slider coolantGauge;

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

        if(!testMode)
        {
            rpmsmooth = Mathf.Lerp(rpmsmooth,engine.rpm,Time.deltaTime*7);
        }
        rpmNeedle.localEulerAngles = new Vector3(0,0,-rpmsmooth/8000*200);  
        coolantGauge.value=coolant*0.25f;
        fuelGauge.value=fuel*0.25f;
                  
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

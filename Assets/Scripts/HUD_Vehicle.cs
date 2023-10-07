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
    void Awake()
    {
        VehicleController.OnGearChange+=UpdateGear;
    }
    private void Start()
    {
        StartCoroutine(UpdateSpeed());
       
    }
    private void OnDisable() {
        VehicleController.OnGearChange-=UpdateGear;
    }

    // Update is called once per frame
    void Update()
    {

             
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
                speed_T.text = (Mathf.Abs(vehicle.wheelSpeed) * 3.6f).ToString("0");               
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    void UpdateGear(int _gear)
    {
        if(_gear>=0)
        {
            gear_T.text="D";
            
            
        }else
        {
             gear_T.text="R";
        }
       
    }
}

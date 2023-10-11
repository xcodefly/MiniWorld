using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public enum EngineStatus { Off,Start,Running}
public class VehiclEngineSound : MonoBehaviour
{

    
    // Start is called before the first frame update
    [SerializeField] AudioClip[] engineClips;   
    [SerializeField] AudioSource engineSound;
    [SerializeField] Engine engine;
   
    
    void Start()
    {
        VehicleAction.OnVehicleStart += StartVechileSound;
        engine=GetComponent<Engine>();
    }
    private void OnDisable()
    {
        VehicleAction.OnVehicleStart -= StartVechileSound;
    }
    // Update is called once per frame
    void Update()
    {
        engineSound.volume = Mathf.Lerp(0.3f, 1, PlayerInput.instance.throttle);
        engineSound.pitch = Mathf.Lerp(0.9f, 1.8f, engine.rpm/engine.maxRPM );

    }
    void StartVechileSound(bool _status)
    {
        if(_status)
        {
            StartCoroutine(StartEngine());
        }else
        {
            StopEngine();
        }
       
    }

    IEnumerator StartEngine()
    {
        
        engineSound.clip = engineClips[0];
        engineSound.Play();
        yield return new WaitForSeconds(engineClips[0].length);
        engineSound.clip = engineClips[1];
        engineSound.Play();
        engineSound.loop = true;
       

    }
    void StopEngine()    {
        
        engineSound.clip = engineClips[2];
        engineSound.Play();
        engineSound.loop=false;
   
    }
    
  

}

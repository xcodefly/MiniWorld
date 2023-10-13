using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : MonoBehaviour
{
    [Range  (0,6000)]
    public float engineSpeed, transmissionSpeed;
   
    
    
    public float difference;
    public AnimationCurve slipCurve;
    public float lockSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        difference = (engineSpeed - transmissionSpeed)/lockSpeed;
        transmissionSpeed = Mathf.Lerp(transmissionSpeed, engineSpeed, Time.deltaTime);
    }
}

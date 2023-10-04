using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Mathematics;
using UnityEngine;

public class WheelInfo : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]WheelCollider wheel;
    [SerializeField]Transform wheelMesh;
    
    void Start()
    {
        wheel=GetComponent<WheelCollider>();
        wheelMesh = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        
        Quaternion rot;
        Vector3 pos;
        wheel.GetWorldPose(out pos,out rot);
        wheelMesh.position=pos;
        wheelMesh.rotation=rot;
    }
}

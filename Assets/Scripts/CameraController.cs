using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;        // The target (character) to follow
    public float distance = 5.0f;
    public float distanceSmooth;   // Distance from the character
    [SerializeField]float smoothSpeed =10;
    public float zoomSpeed = 2.0f;  // Speed of zooming
    // Speed of camera rotation
   
    [SerializeField] Vector2 mouseDownPostion,mouseDelta;  
    [SerializeField] float rotationAngle,pitchAngle;
    [SerializeField]Quaternion rotationSmooth;
    private void Start() {
        rotationAngle=  PlayerPrefs.GetFloat("Rotation", 0);
        pitchAngle=  PlayerPrefs.GetFloat("Angle", 20);
        distance=  PlayerPrefs.GetFloat("Distance", 10);
        distanceSmooth=distance;
    }
  
    private void LateUpdate()
    {
        if (target == null)
            return;

        // Calculate the desired position for the camera
        float zoomInput = Input.GetAxis("Mouse ScrollWheel");
        distance -= zoomInput * zoomSpeed * Time.fixedDeltaTime;
        float desiredRotationAngle = target.eulerAngles.y;

        // Calculate zoom input
       
        distanceSmooth=Mathf.Lerp(distanceSmooth,distance,Time.deltaTime *smoothSpeed);

        // Calculate rotation input
        if(Input.mouseScrollDelta.y!=0)
        {
            PlayerPrefs.SetFloat("Distance", distance);
           
        }

        if(Input.GetMouseButtonDown(1))
        {
            mouseDownPostion=Input.mousePosition;
        }
        if(Input.GetMouseButton(1))
        {
            mouseDelta=mouseDownPostion-(Vector2)Input.mousePosition;
            rotationAngle=rotationAngle-mouseDelta.x*0.2f;
            pitchAngle=pitchAngle+mouseDelta.y*0.1f;
            pitchAngle=Mathf.Max(10,Mathf.Min(45,pitchAngle));
            mouseDownPostion=Input.mousePosition;
        }
        if(Input.GetMouseButtonUp(1))
        {
            PlayerPrefs.SetFloat("Rotation", rotationAngle);
            PlayerPrefs.SetFloat("Angle", pitchAngle);        
        }  
        // Calculate desired position
        Quaternion rotation = Quaternion.Euler(pitchAngle, rotationAngle+target.eulerAngles.y, 0) ;
        rotationSmooth=Quaternion.Lerp(rotationSmooth,rotation  ,Time.deltaTime *smoothSpeed);
        Vector3 offset = new Vector3(0, 0, -distanceSmooth);
        transform.position = target.position + rotationSmooth * offset;

        // Smoothly move the camera
        //  transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Make the camera look at the target
        transform.LookAt(target);
    }
}

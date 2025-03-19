using System.Collections;

using UnityEngine;


public class CameraController : MonoBehaviour
{
    public Transform player;    // The target (character) to follow
    public float zoomDistance = -8.0f;
    // Distance from the character
    [SerializeField]float smoothSpeed =20;
    public float zoomSpeed = 0.5f;  // Speed of zooming
    // Speed of camera rotation   
    [SerializeField] Vector2 mouseDownPostion,mouseDelta;  
    [SerializeField] float rotationAngle,pitchAngle;  
    [SerializeField]Camera cam;    
    [SerializeField]Vector2 mousePositionStart;
    [SerializeField]float camHeight,camOffest,camDistance,viewTilt,viewRotate;

    private void Awake()
    {
        Application.targetFrameRate=60;
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }
    private void Start() 
    {
        rotationAngle=  PlayerPrefs.GetFloat("Rotation", 0);
        pitchAngle=  PlayerPrefs.GetFloat("Angle", 20);
        zoomDistance=  PlayerPrefs.GetFloat("zoomDistance", 10);      
        LoadCamPostion();
        CalculateCamPosition();
    }

     void Update()
    {
        if(Input.mouseScrollDelta.y!=0 )
        {
           // if(Input.GetMouseButtonDown(1))
         //   {   
                zoomDistance=zoomDistance-Input.mouseScrollDelta.y*zoomSpeed;
                zoomDistance=Mathf.Max(5,Mathf.Min(15,zoomDistance));
            
                CalculateCamPosition();
        //    }
          
        }
        
        if(Input.GetMouseButtonDown(1))
        {
            mousePositionStart=Input.mousePosition;
        }
        if(Input.GetMouseButton(1))
        {
            mouseDelta =mousePositionStart-(Vector2)Input.mousePosition;
            viewTilt = viewTilt + mouseDelta.y*0.3f;
            viewTilt=Mathf.Max(10,Mathf.Min(45,viewTilt));

            viewRotate = viewRotate +mouseDelta.x*0.25f;
            mousePositionStart=Input.mousePosition;
            CalculateCamPosition();
        }

        if(Input.GetMouseButtonUp(1))
        {
            mousePositionStart=Vector2.zero;
            
        }

    }
    void SaveCamPosition()
    {
        PlayerPrefs.SetFloat("zoomDistance",zoomDistance);
        PlayerPrefs.SetFloat("viewTilt",viewTilt);
        PlayerPrefs.SetFloat("viewRotate",viewRotate);
    }
    void LoadCamPostion()
    {
        zoomDistance= PlayerPrefs.GetFloat("zoomDistance",10);
        viewTilt=PlayerPrefs.GetFloat("viewTilt",15);
        viewRotate=PlayerPrefs.GetFloat("viewRotate",0);
    }
    void CalculateCamPosition()
    {
        camHeight = zoomDistance*Mathf.Sin(viewTilt*Mathf.Deg2Rad);
        camOffest=zoomDistance*Mathf.Sin(viewRotate*Mathf.Deg2Rad);
        camDistance = zoomDistance*Mathf.Cos(viewTilt*Mathf.Deg2Rad);
        camDistance=camDistance*Mathf.Cos(viewRotate*Mathf.Deg2Rad);   
    }
   
    private void LateUpdate()
    {
        cam.transform.position=Vector3.Lerp(cam.transform.position, player.position  + player.TransformDirection(camOffest,camHeight,-camDistance) ,Time.deltaTime*smoothSpeed);
        cam.transform.LookAt(player.position,Vector3.up);
    }
    IEnumerator CenterViewC()
    {
        while(Mathf.Abs(rotationAngle)>0.1)
        {
            rotationAngle = Mathf.Lerp(rotationAngle, 0, Time.deltaTime*10);
            yield return null;
        }
    }
    
}

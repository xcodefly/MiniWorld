using System.Collections;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // Start is called before the first frame update
    public static PlayerInput instance;
    public float forward,rotate,steer,brake,throttle,speed;
    public float rotationSpeed;
    [SerializeField]bool run;
   
    [SerializeField] CharacterController character;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        character=GetComponent<CharacterController>();
       
    }
    // Update is called once per frame
    void LateUpdate()
     {
       
        forward = Input.GetAxis("Vertical");
        rotate = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up,rotate*rotationSpeed);
        character.SimpleMove(transform.forward*forward*speed);
    }
}

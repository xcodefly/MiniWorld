using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public enum PlayerActions { walking,driving}

public class PlayerInput : MonoBehaviour
{
    // Start is called before the first frame update
    public static PlayerInput instance;
    public float forward,rotate,throttle,brake,steer;
    
    public PlayerActions action;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        action = PlayerActions.driving;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Action"))
        {
            Debug.Log("Action Button");
            if(action == PlayerActions.driving)
            {
                action = PlayerActions.walking;
            }else
            {
                action = PlayerActions.driving;
            }
        }

        if(action== PlayerActions.driving)
        {
            if(Input.GetAxis("Vertical")>0)
            {
                throttle = Input.GetAxis("Vertical");
                brake = 0;
            }else
            {
                brake=-Input.GetAxis("Vertical");
                throttle = 0;
            }
            steer = Input.GetAxis("Horizontal");
        }
        else
        {
            forward = Input.GetAxis("Vertical");
            rotate = Input.GetAxis("Horizontal");
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    public float vertical,horizontal;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        vertical=Input.GetAxis("Vertical");
        horizontal=Input.GetAxis("Horizontal");
    }
}

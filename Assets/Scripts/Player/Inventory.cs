using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]Stock[] slots=new Stock[5];
    public static Inventory Instance ;
    void Start()
    {
        if (Instance == null)   
        {
            Instance = this;
        }
        
        for (int i = 0;i<5;i++)
        {  
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool AvaialbeItem(Stock _stock)
    {
        return false;
    }

    public bool AcceptItem(Stock _stock)
    {
        
        return false;
    }

    public bool FindItem(Stock _stock)
    {
        for (int i = 0;i<5;i++)
        {
            if(_stock.GetType()==slots[i].GetType())
            {
                Debug.Log("Found Item.");
                return true;
            }
        }    
        return false;
    }
}

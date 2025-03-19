using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


[System.Serializable]
public struct Stock
{   
    [SerializeField] ResourceType resType;
    [SerializeField] float quantity;
    [SerializeField] float maxQuantity;

    
    public bool AddRemove(float _amount)
    {
       if(_amount>0)
       {
            if (quantity+_amount<=maxQuantity)
            {
                quantity+=_amount;                
                return true;
            }else
            {
                return false;
            }
       }else
       {
            if (quantity+_amount<0)
            {
                return false;
            }else
            {
                
                 quantity+=_amount;
                return true;
            }
       }
       
    } 
    
    
    public ResourceType GetStockType()
    {
        return resType;
    }

    public float StockQuantity()
    {
        return quantity;
    }
   
}

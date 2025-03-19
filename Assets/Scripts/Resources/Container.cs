using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;



public class Container : MonoBehaviour, I_ActionType, I_UIStatus
{   
    // Start is called before the first frame update
    public static Action <List<ActionsSO>> PlayerActions;
    public Stock stock;
    [SerializeField]bool active;
    public List<ActionsSO> actionList;
    [SerializeField]UIManagerContainer containerUI;
  
    [SerializeField]Inventory inventory;
    
    void Start()
    {
        containerUI=UIManagerContainer.instance;
        inventory = Inventory.Instance;
    }

    // Update is called once per frame
 
    public void UI_Update(UI_status _status)
    {
        switch (_status)
        {
            case UI_status.show:              
                containerUI.ShowResourceUI(transform,this);
            break;
            case UI_status.hide:             
                containerUI.HideResourceUI();
            break;
            case UI_status.update:
                containerUI.UpdateContainerUI(this);            
            break;
        }
    }

    public List<ActionsSO> ActionAvailable()
    {
        foreach(ActionsSO _action in actionList)
        {
            Debug.Log(_action.actionType);
        }
        PlayerActions.Invoke(actionList);
        return actionList;
    }
    public void PlayerLeaves()
    {
        PlayerActions.Invoke(null);
    }
    public bool Action(int index)
    {       
         
        return false;
    }
    


}

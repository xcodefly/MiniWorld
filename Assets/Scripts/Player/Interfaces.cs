

using System.Collections.Generic;
using UnityEngine;
using System;



public interface I_ActionType
{
    public bool Action(int index);  

    public List<ActionsSO> ActionAvailable ( );
    public void PlayerLeaves();

    

}






[System.Serializable]
public enum UI_status {hide,show,update}
public interface I_UIStatus
{
    public void UI_Update(UI_status _action);
}




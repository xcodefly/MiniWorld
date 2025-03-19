using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActionsUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]Button [] actionButtons;
    void Start()
    {
        Container.PlayerActions +=PossiblePlayerActions;
       
    }
    void OnDisable()                                
    {
         Container.PlayerActions -=PossiblePlayerActions;
    }

    // Update is called once per frame
    public void PossiblePlayerActions(List<ActionsSO> actionList)
    {
      
        if(actionList!=null)
        {
            for(int i = 0;i<actionList.Count;i++)
            {
                if(actionList[i].actionType!=ActionType.none)
                {
                    actionButtons[i].transform.GetChild(0).GetComponent<Image>().sprite = actionList[i].sprite;
                    actionButtons[i].gameObject.SetActive(true);
                }
            }
        }
        else
        {
            for(int i = 0;i<actionButtons.Length;i++)
            {
                actionButtons[i].gameObject.SetActive(false);
            }
        }
      
        
    }

    
}

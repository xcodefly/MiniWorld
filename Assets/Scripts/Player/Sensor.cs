using UnityEngine;


public class Sensor : MonoBehaviour
{
    // Start is called before the first frame update\
    float cooldown;
    [SerializeField] float cooldownTime=0.2f;  
    [SerializeField] I_ActionType actionAvaialbe;
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {  


        if(Input.GetButton("B"))
        {
            if(cooldown<0)
            {
                Debug.Log("Button B");
                cooldown =cooldownTime;
            }
        }
        cooldown-=Time.deltaTime;
        if(cooldown<-1)
        {
            cooldown=-1;
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<I_ActionType>(out I_ActionType _actionAvaialbe))
        {
            _actionAvaialbe.ActionAvailable();
         
        }

        if(other.TryGetComponent<I_UIStatus>(out I_UIStatus _uiAction))
        {
            _uiAction.UI_Update(UI_status.show);            
        }
    }
  
    void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent<I_ActionType>(out I_ActionType _actionAvaialbe))
        {
            _actionAvaialbe.PlayerLeaves();
         
        }
        if(other.TryGetComponent<I_UIStatus>(out I_UIStatus _uiAction))
        {
            _uiAction.UI_Update(UI_status.hide); 
             
        }
    }   

   
}

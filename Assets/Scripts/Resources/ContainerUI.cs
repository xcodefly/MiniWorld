

using UnityEngine;
using UnityEngine.UI;

public class ContainderUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Image icon;
    [SerializeField] Text value;   
    

    // Update is called once per frame
    public void ShowInfo(float _amount,Sprite _icon)
    {
       icon.sprite=_icon;
       value.text=_amount.ToString();
    }

    public void UpdateAmount(float _amount)
    {
         value.text=_amount.ToString();
    }   
}

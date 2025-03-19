
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]Transform [] slots = new Transform[5];
    [SerializeField]Text [] _quantity= new Text[5];
    [SerializeField]Image [] _icons = new Image[5];
    [SerializeField]UIManagerContainer uiManager;
   
    void Start()
    {
        for(int i=0; i<slots.Length; i++)
        {
            slots[i]= transform.GetChild(i);
            _icons[i]=slots[i].GetChild(0).GetComponent<Image>();
            _quantity[i]=slots[i].GetChild(1).GetComponent<Text>();
            
        }
      
    }
   
    void OnDisable()
    {
     
    }

    void PlayerStartInteraction(int _index,Stock _playerStock)
    {
        _icons[_index].sprite=    uiManager.FindIcon(_playerStock.GetStockType());
        _quantity[_index].text=_playerStock.StockQuantity().ToString();  
    }
    void UpdatePlayerUpdate(int _index,Stock _playerStock)
    {     
      
        _quantity[_index].text=_playerStock.StockQuantity().ToString();
    }

    void PlayerStopInteraction(int _index, Stock _playerStock)
    {
        if(_playerStock.StockQuantity()==0)
        {
            _quantity[_index].text="";
            _icons[_index].sprite = uiManager.FindIcon(ResourceType.none);

            
        }
    }
   
    
}

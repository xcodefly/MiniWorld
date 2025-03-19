using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;


public class UIManagerContainer : MonoBehaviour
{
    // Start is called before the first frame update
    public static UIManagerContainer instance;
    [SerializeField] Canvas canvas;
    [SerializeField] bool visible;    
    [SerializeField] ContainderUI containderUI;
    public List<ResourceSO> resourceSOs;
    
    void Awake()
    {
       instance=this;
    }
  

   public void UpdateContainerUI(Container _container)
   {
        containderUI.UpdateAmount(_container.stock.StockQuantity());
   }
    public void ShowResourceUI(Transform _transfrom, Container _container)
    {
        canvas.gameObject.SetActive(true);
        canvas.transform.position=_transfrom.position;
      
        Sprite _sprite=null;
        foreach(ResourceSO res in resourceSOs)
        {
            if (res.resName == _container.stock.GetStockType())
            {
                _sprite = res.icon;
                break;
            }
        }
        containderUI.ShowInfo(_container.stock.StockQuantity(),_sprite);
        visible=true;
    }

    public void HideResourceUI()
    {
        
        canvas.gameObject.SetActive(false);        
        visible=false;
    }
    // Update is called once per frame
    void Update()
    {
        if(visible)
        {
            canvas.transform.rotation=Camera.main.transform.rotation;
        }
    }

    public Sprite FindIcon(ResourceType _type)
    {
        Sprite _icon=resourceSOs[0].icon;
        for(int i=0;i<resourceSOs.Count;i++)
        {
            if(_type==resourceSOs[i].resName)
            {
                _icon= resourceSOs[i].icon;               
            }
        }
        return _icon;
    }
}

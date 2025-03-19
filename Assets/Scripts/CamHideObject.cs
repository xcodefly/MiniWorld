using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamHideObject : MonoBehaviour
{
    // Start is called before the first frame update
    RaycastHit hit;
    [SerializeField]string hitobject;
    [SerializeField]Transform hitTransfrom;
    [SerializeField]List<Transform> transparentList;
    void Awake()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        Physics.Raycast(transform.position,transform.forward,out hit,20);                     
        if(hit.transform.tag=="CamObject")
        {
            if(hitTransfrom!=hit.transform)           
            {
                // Find the object in list
                if(transparentList.Count>0)
                {
                    int _count=-1;
                    _count=transparentList.IndexOf(hit.transform);
                    if(_count==-1)
                    {
                        FadeTranfrom(hit.transform);
                        transparentList.Add(hit.transform);
                        hitTransfrom=hit.transform;
                    }
                    else{
                        RestoreTranfrom(hitTransfrom);
                        transparentList.Remove(hitTransfrom);
                        hitTransfrom=hit.transform;
                    }
                }else
                {
                    FadeTranfrom(hit.transform);
                    transparentList.Add(hit.transform);
                    hitTransfrom=hit.transform;
                }
            }
            
        }
        else
        {
            foreach(Transform t in transparentList)
            {
                RestoreTranfrom(t);
            }
            transparentList.Clear();
            hitTransfrom=null;
        }      
        
         
    }

    public void FadeTranfrom(Transform _temp)
    {
        Material[]materials= _temp.GetComponent<Renderer>().materials;
        foreach(Material m in materials)
        {
            m.color = m.color*0.5f;
        }
    }

    public void RestoreTranfrom(Transform _temp)
    {
        Material[]materials= _temp.GetComponent<Renderer>().materials;
        foreach(Material m in materials)
        {
            m.color = m.color*2f;
        }
    }
}


using System.Collections.Generic;

using UnityEngine;

using System.Collections;

public class FishMovement : MonoBehaviour
{
    // Start is called before the first frame update
    // Find the dimentions of a collider.
    [SerializeField]BoxCollider tank;
    [SerializeField]Vector3 tankCenter,tankSize;
    [SerializeField]Transform waypoint;
    [SerializeField]List<Vector3>   path;
    float rotatespeed, movespeed;
    [SerializeField]LineRenderer pathLine;
    [SerializeField]Transform end,control;
    [SerializeField]bool move;
    
    void Start()
    {
        tank = GetComponentInParent<BoxCollider>();
        if(tank!=null)
        {
            tankCenter = tank.center;
            tankSize =tank.size;
        }
        move=false;
        pathLine =GetComponentInParent<LineRenderer>();
        StartCoroutine(FishSpeed());

    }
    IEnumerator FishSpeed()
    {
        while(true)
        {
            float dtime = Random.Range(0.1f,2);
            ChagneFishSpeed();
       
            yield return new WaitForSeconds(dtime);
        }
      
    }
    void ChagneFishSpeed()
    {
        movespeed = Random.Range(-0.1f, 1f);
        Debug.Log(movespeed);
    } 
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            FindNewPosition();
        }
     
        if (move)
        {
            // rotate towardds;
            transform.LookAt(path[0]);
            transform.position = transform.position + transform.forward*Time.deltaTime*movespeed;
            if((path[0]-this.transform.position).magnitude<0.02)
            {
                path.RemoveAt(0);
            }
            if(path.Count<1)
            {
                FindNewPosition();
            }
            
            // move to point;
        }


    }
    void FindNewPosition()
    {
        Vector3 controlPoint = GetRandomPointInBounds()+transform.parent.position;
        Vector3 destination = GetRandomPointInBounds()+transform.parent.position;
        MakeCurve(this.transform.position, controlPoint,destination);
        ChagneFishSpeed();
    }

    void MakeCurve(Vector3 _s, Vector3 _c, Vector3 _f)
    {
        Vector3 a,b,c;
        int count =0;
        pathLine.positionCount=21;
        for(float i =0 ;i<=1;i=i+0.05f)
        {
            a = Vector3.Lerp( _s,_c,i);
            b = Vector3.Lerp(_c,_f,i);
            c = Vector3.Lerp(a,b,i);
            pathLine.SetPosition(count,c);
            path.Add(c);
            count++;
        }
     //   path.Add(_f);
        move=true;
    //    pathLine.SetPosition(count,_f);
    //    pathLine.positionCount=count;
    }
     Vector3 GetRandomPointInBounds()
    {
        Vector3 minBounds = tankCenter - tankSize / 2f;
        Vector3 maxBounds = tankCenter + tankSize / 2f;
        float randomX = Random.Range(minBounds.x, maxBounds.x);
        float randomY = Random.Range(minBounds.y, maxBounds.y);
        float randomZ = Random.Range(minBounds.z, maxBounds.z);
        return new Vector3(randomX, randomY, randomZ);
    }
}

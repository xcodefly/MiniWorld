using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    // Start is called before the first frame update
    // Find the dimentions of a collider.
    [SerializeField]BoxCollider tank;
    [SerializeField]Vector3 tankCenter,tankSize;
    [SerializeField]Transform waypoint;
    [SerializeField]List<Vector3>  path;
    [SerializeField]float rotatespeed, movespeed;

    void Start()
    {
        tank = GetComponentInParent<BoxCollider>();
        if(tank!=null)
        {
            tankCenter = tank.center;
            tankSize =tank.size;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Find New position"); 
            Debug.Log("Tank world position : "+tankCenter);
            FindNewPosition();
        }

        if (path.Count>1)
        {
            // rotate towardds;
            transform.LookAt(path[0]);
            transform.position = transform.position + transform.forward*Time.deltaTime*movespeed;
            if ((path[0]-transform.position).magnitude<0.2f)
            {
                path.RemoveAt(0);
            }
            // move to point;
        }


    }

    void FindNewPosition()
    {
        Vector3 newPosition = GetRandomPointInBounds()+transform.parent.position;

        Instantiate(waypoint,newPosition,transform.rotation,this.transform.parent);
        path.Add(newPosition);
       
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

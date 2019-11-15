using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Follower : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed =5;
    float distanceTravelled;
    public float startX=0,startZ=0;
    
   Transform headTf;
    // Start is called before the first frame update
    void Start()
    {
    headTf = transform.Child(0);
    }

    // Update is called once per frame
    void Update()
    {
        distanceTravelled += speed*Time.deltaTime;
        headTf.transform.position= pathCreator.path.GetPointAtDistance(distanceTravelled)+new Vector3(startX,0.5f,startZ);
        headTf.transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("obs"))
        {
           speed = 0f;
        }
         if(other.gameObject.CompareTag("blue"))
        {
           Destroy(gameObject);
        }
        if(other.gameObject.CompareTag("red"))
        {
           Destroy(gameObject);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class asd22 : MonoBehaviour
{
    public PathCreator pathCreator;
    public PathCreator pathCreator2;
    public PathCreator pathCreator3,pathCreator4;
    public EndOfPathInstruction endOfPathInstruction;
    public EndOfPathInstruction endOfPathInstruction2;
    public float speed=15f;
    float distanceTravelled, distanceTravelled2, distanceTravelled3,distanceTravelled4,distanceTravelled5;
    bool isObsEnter = false;
    public GameObject ahead;
    float distance, distance2,distance3,distance4;
    int pathIdx = 0;
    void Start()
    {
    }
    void FixedUpdate()
    {
        if (pathIdx == 0)
        {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
            distance = Vector3.Distance(pathCreator.path.GetPointAtDistance(distanceTravelled), pathCreator2.path.GetPointAtDistance(distanceTravelled2));
            if (A.Apx(pathCreator2.transform.parent.eulerAngles.y, 0, 0.1f) && distance < 0.4f)
            {
                pathIdx++;
            }
        }
        else if (pathIdx == 1)
        {
            distanceTravelled2 += speed * Time.deltaTime;
            transform.position = pathCreator2.path.GetPointAtDistance(distanceTravelled2, endOfPathInstruction2);
            transform.rotation = pathCreator2.path.GetRotationAtDistance(distanceTravelled2);
            distance2 = Vector3.Distance(transform.position, pathCreator3.path.GetPointAtDistance(distanceTravelled3));
            if (distance2 < 0.5f)
            {
                pathIdx++;
            }
        }
        else if (pathIdx == 2)
        {
            distanceTravelled3 += speed*Time.deltaTime;
            transform.position = pathCreator3.path.GetPointAtDistance(distanceTravelled3);
            distance3 = Vector3.Distance(transform.position, pathCreator4.path.GetPointAtDistance(distanceTravelled4));
            if (distance3 < 0.5f)
            {
                pathIdx++;
            }
        }
        else if(pathIdx == 3)
        {
            distanceTravelled4 += speed*Time.deltaTime;
            transform.position = pathCreator4.path.GetPointAtDistance(distanceTravelled4);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("obs"))
            Stop();
        if (other.gameObject.CompareTag("blue"))
        {
            if (other.GC<asd22>().isObsEnter)
            {
                speed = 0f;
                isObsEnter = true;
            }
        }
        if (other.gameObject.CompareTag("red"))
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionStay(Collision other)
    {
        if (other.Tag("obs"))
        {
            isObsEnter = true;
            speed = 0f;
        }
        if (other.gameObject.CompareTag("blue"))
        {
            if (other.GC<asd22>().isObsEnter)
            {
                speed = 0f;
                isObsEnter = true;
            }
        }
    }
    void Stop()
    {
        speed = 0f;
        isObsEnter = true;
    }
    void Play()
    {
        speed = 15;
        isObsEnter = false;
    }
}


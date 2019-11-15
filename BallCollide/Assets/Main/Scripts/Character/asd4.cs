using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class asd4 : MonoBehaviour
{
    public PathCreator pathCreator;
    public PathCreator pathCreator2;
    public PathCreator pathCreator3,pathCreator4,pathCreator5,pathCreator6,pathCreator7,pathCreator8,pathCreator9,pathCreator10,pathCreator11,pathCreator12;
    public EndOfPathInstruction endOfPathInstruction;
    public EndOfPathInstruction endOfPathInstruction2;
    public float speed=15f;
    float distanceTravelled,distanceTravelled16,distanceTravelled15,distanceTravelled14, distanceTravelled2, distanceTravelled3,distanceTravelled4,distanceTravelled5,distanceTravelled6,distanceTravelled7,distanceTravelled10,distanceTravelled12,distanceTravelled13;
    bool isObsEnter = false;
    bool isClosedLoop = false;
    public GameObject ahead;
    float distance, distance12,distance16,distance2,distance14,distance3,distance4,distance5,distance6,distance10,distance11,distance15,distance7,distance8,distance9;
    public int pathIdx = 0;
    void Start()
    {
    }
    void FixedUpdate()
    {
        if (pathIdx == 0)
        {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled,endOfPathInstruction=EndOfPathInstruction.Stop);
            distance = Vector3.Distance(transform.position, pathCreator2.path.GetPointAtDistance(0));
            distance10 = Vector3.Distance(transform.position, pathCreator3.path.GetPointAtDistance(0));
            distance9 = Vector3.Distance(transform.position, pathCreator11.path.GetPointAtDistance(0));
            if (A.Apx(pathCreator2.transform.parent.eulerAngles.y, 0, 0.1f) && distance < 0.4f)
            {
                pathIdx=1;
                distanceTravelled2=0;
            }
            else if(A.Apx(pathCreator2.transform.parent.eulerAngles.y, 90, 0.1f)&& distance10<0.4f)
            {
                pathIdx = 10;
                distanceTravelled10 = 0;
            }
             else if(A.Apx(pathCreator2.transform.parent.eulerAngles.y, 270, 0.1f)&& distance9<0.4f)
            {
                pathIdx = 14;
                distanceTravelled15  = 0;
            }
        }
             else if (pathIdx == 14)
        {
            distanceTravelled15 += speed * Time.deltaTime;
            transform.position = pathCreator11.path.GetPointAtDistance(distanceTravelled15);
            distance14=Vector3.Distance(transform.position,pathCreator.path.GetPointAtDistance(0));
            if(distance14<0.4)
            {
                pathIdx=0;
                distanceTravelled = 0;
            }
        }
           else if (pathIdx == 10)
        {
            distanceTravelled10 += speed * Time.deltaTime;
            transform.position = pathCreator3.path.GetPointAtDistance(distanceTravelled10);
            distance11=Vector3.Distance(transform.position,pathCreator.path.GetPointAtDistance(0));
            if(distance11<0.4)
            {
                pathIdx=0;
                distanceTravelled = 0;
            }
        }
        else if (pathIdx == 1)
        {
            distanceTravelled2 += speed * Time.deltaTime;
            transform.position = pathCreator2.path.GetPointAtDistance(distanceTravelled2);
            distance2 = Vector3.Distance(transform.position, pathCreator4.path.GetPointAtDistance(0));
            if (distance2 < 0.5f)
            {
                pathIdx=2;
            }
        }
        else if (pathIdx == 2)
        {
            distanceTravelled3 += speed*Time.deltaTime;
            transform.position = pathCreator4.path.GetPointAtDistance(distanceTravelled3);
            distance3 = Vector3.Distance(transform.position, pathCreator5.path.GetPointAtDistance(0));
            if (distance3 < 0.5f)
            {
                pathIdx=3;
            }
        }
        else if(pathIdx == 3)
        {
            distanceTravelled4 += speed*Time.deltaTime;
            transform.position = pathCreator5.path.GetPointAtDistance(distanceTravelled4,endOfPathInstruction=EndOfPathInstruction.Stop);
            distance4=Vector3.Distance(transform.position,pathCreator6.path.GetPointAtDistance(0));
            distance15=Vector3.Distance(transform.position,pathCreator7.path.GetPointAtDistance(0));
            distance16=Vector3.Distance(transform.position,pathCreator12.path.GetPointAtDistance(0));
            if(A.Apx(pathCreator6.transform.parent.eulerAngles.y, 90, 0.1f)&&distance4<0.5f)
            {
                pathIdx=4;
            }
            else if (A.Apx(pathCreator7.transform.parent.eulerAngles.y,0, 0.1f)&&distance15<0.5f)
            {
                pathIdx=12;
            }
             else if (A.Apx(pathCreator7.transform.parent.eulerAngles.y,180, 0.1f)&&distance16<0.5f)
            {
                pathIdx=13;
            }
        }
         else if(pathIdx == 4)
        {
            distanceTravelled5 += speed*Time.deltaTime;
            transform.position = pathCreator6.path.GetPointAtDistance(distanceTravelled5);
            distance5=Vector3.Distance(transform.position,pathCreator8.path.GetPointAtDistance(0));
            if(distance5<0.5)
            {
                pathIdx=5;
            }
        }
         else if(pathIdx == 5)
        {
            distanceTravelled6 += speed*Time.deltaTime;
            transform.position = pathCreator8.path.GetPointAtDistance(distanceTravelled6);
            distance6=Vector3.Distance(transform.position,pathCreator9.path.GetPointAtDistance(0));
            if(distance6<0.5)
            {
                pathIdx=6;
            }
        }
         else if(pathIdx==6)
        {
            distanceTravelled13 +=speed*Time.deltaTime;
            transform.position = pathCreator9.path.GetPointAtDistance(distanceTravelled13);
        }
        else if(pathIdx==12)
        {
            distanceTravelled12 +=speed*Time.deltaTime;
            transform.position = pathCreator7.path.GetPointAtDistance(distanceTravelled12);
            distance7=Vector3.Distance(transform.position,pathCreator10.path.GetPointAtDistance(0));
            if(distance7<0.5)
            {
                pathIdx=7;
            }
        }
        else if(pathIdx==13)
        {
            distanceTravelled16 +=speed*Time.deltaTime;
            transform.position = pathCreator12.path.GetPointAtDistance(distanceTravelled16);
            distance12=Vector3.Distance(transform.position,pathCreator12.path.GetPointAtDistance(0));
            if(distance12<0.5)
            {
                pathIdx=7;
            }
        }
        else if(pathIdx==7)
        {
            distanceTravelled14 +=speed*Time.deltaTime;
            transform.position = pathCreator10.path.GetPointAtDistance(distanceTravelled14);
            distance8=Vector3.Distance(transform.position,pathCreator5.path.GetPointAtDistance(0));
            if(distance8<0.5)
            {
                pathIdx=3;
                distanceTravelled4=0;
            }
        }
    }
    
    

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("obs"))
            Stop();
        if (other.gameObject.CompareTag("blue"))
        {
            if (other.GC<asd4>().isObsEnter)
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
            if (other.GC<asd4>().isObsEnter)
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


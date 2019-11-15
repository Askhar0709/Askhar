using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Allfollow : MonoBehaviour
{
    public PathCreator pathCreatorB1,pathCreatorB2,pathCreatorB3,pathCreatorR1,pathCreatorR2,pathCreatorR3;
    public GameObject Blue1,Blue2,Blue3,Red1,Red2,Red3;
    public float speed =5;
    float distanceTravelled;
 
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        distanceTravelled += speed*Time.deltaTime;
        Blue1.transform.position= pathCreatorB1.path.GetPointAtDistance(distanceTravelled)+new Vector3(0,0.5f,0);
        Blue1.transform.rotation = pathCreatorB1.path.GetRotationAtDistance(distanceTravelled);
        Blue2.transform.position = pathCreatorB2.path.GetPointAtDistance(distanceTravelled)+new Vector3(0,0.5f,0);
        Blue2.transform.rotation = pathCreatorB2.path.GetRotationAtDistance(distanceTravelled);
        Blue3.transform.position = pathCreatorB3.path.GetPointAtDistance(distanceTravelled)+new Vector3(0,0.5f,0);
        Blue3.transform.rotation = pathCreatorB3.path.GetRotationAtDistance(distanceTravelled);
        Red1.transform.position = pathCreatorR1.path.GetPointAtDistance(distanceTravelled)+new Vector3(0,0.5f,0);
        Red1.transform.rotation = pathCreatorR1.path.GetRotationAtDistance(distanceTravelled);
        Red2.transform.position = pathCreatorR2.path.GetPointAtDistance(distanceTravelled)+new Vector3(0,0.5f,0);
        Red2.transform.rotation = pathCreatorR2.path.GetRotationAtDistance(distanceTravelled);
        Red3.transform.position = pathCreatorR3.path.GetPointAtDistance(distanceTravelled)+new Vector3(0,0.5f,0);
        Red3.transform.rotation = pathCreatorR3.path.GetRotationAtDistance(distanceTravelled);

    }
}

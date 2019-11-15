using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class TestinAshit : MonoBehaviour
{
    public PathCreator pathCreator;
    public PathCreator pathCreator2;
    public EndOfPathInstruction endOfPathInstruction;
    public float speed = 5;
    public float time=0.5f;
    public BoxCollider bc;
    float distanceTravelled;
    bool isObsEnter = false;
    public GameObject ahead;
    // Start is called before the first frame update
    void Start()
    {
        // transform.position=pathCreator.path.GetPoint(25);
    }

    // Update is called once per frame
    void Update()
    {
        distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled ) + new Vector3(0, 0.5f, 0);
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
        //,endOfPathInstruction = EndOfPathInstruction.Stop//
        if(ahead==null)
        {
            speed = 15f;
        }
    }
      private void OnMouseDown()
    {
      bc.isTrigger =!bc.isTrigger;
      transform.Rotate(0,90,0);
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled,endOfPathInstruction=EndOfPathInstruction.Stop);
        transform.position = pathCreator2.path.GetPointAtDistance(distanceTravelled)+new Vector3(0,0.5f,0);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("obs"))
            Stop();
        if (other.gameObject.CompareTag("blue"))
        {
            // if (transform.InverseTransformPoint(other.transform.position).z > 0)
                // Stop();
                 if (other.GC<TestinAshit>().isObsEnter){
                speed = 0f;
                isObsEnter=true;
            }
        }
        if (other.gameObject.CompareTag("red"))
        {
            Destroy(gameObject);
        }
    }
        private void OnCollisionStay(Collision other) {
        if(other.Tag("obs")){
            isObsEnter = true;
            speed = 0f;
        }
        if (other.gameObject.CompareTag("blue"))
        {
            if (other.GC<TestinAshit>().isObsEnter){
                speed = 0f;
                isObsEnter=true;
            }
        }
    }
    // private void OnCollisionExit(Collision other)
    // {

    //     if (other.gameObject.CompareTag("obs"))
    //         Play();
    //     if (other.gameObject.CompareTag("blue"))
    //     {
    //         // if (transform.InverseTransformPoint(other.transform.position).z > 0)
    //             Play();
    //     }
    // }
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
    // private void OnCollisionExit(Collision other) {
    //       if(other.gameObject.CompareTag("obs"))
    //     {
    //        isObsEnter = false;
    //         speed = 15f;
    //     }
    //     if(other.Tag("blue"))
    //     {
    //         if (other.GC<FlwBlue>().isObsEnter){
    //             speed = 15f;
    //             isObsEnter=false;
    //         }
    //     }
    // }

}

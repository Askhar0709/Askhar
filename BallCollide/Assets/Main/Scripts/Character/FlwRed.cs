using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class FlwRed : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = 5;
    float distanceTravelled;
    bool isObsEnter = false;
    public ParticleSystem sparkle;
    public GameObject ahead;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled) + new Vector3(0, 0.5f, 0);
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
        if(ahead == null)
        {
            speed = 15f;
        }
    }
    private void OnCollisionStay(Collision other) {
        if(other.Tag("obs")){
            isObsEnter = true;
            speed = 0f;
        }
        if (other.gameObject.CompareTag("red"))
        {
            if (other.GC<FlwRed>().isObsEnter){
                speed = 0f;
                isObsEnter=true;
            }
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("obs"))
        {
            isObsEnter = true;
            speed = 0f;

        }
        if (other.gameObject.CompareTag("red"))
        {
            if (other.GC<FlwRed>().isObsEnter){
                speed = 0f;
                isObsEnter=true;
            }
        }
        if (other.gameObject.CompareTag("blue"))
        {
            Destroy(gameObject);
            sparkle.transform.position = transform.position;
            sparkle.Play();
        }
    }
    // private void OnCollisionExit(Collision other) {
    //     if(other.gameObject.CompareTag("obs"))
    //     {
    //        isObsEnter = false;
    //         speed = 15f;
    //     }
    //     if(other.Tag("red"))
    //     {
    //         if (other.GC<FlwRed>().isObsEnter){
    //             speed = 15f;
    //             isObsEnter=false;
    //         }
    //     }
    // }

}

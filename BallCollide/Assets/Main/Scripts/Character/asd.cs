using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using System;

public class asd : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed=15f;
    float distanceTravelled=1;
    bool isObsEnter = false;
    void Start()
    {
    }
    void FixedUpdate()
    {
     distanceTravelled = distanceTravelled+ speed * Time.deltaTime;
     transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("obs"))
            Stop();
        if (other.gameObject.CompareTag("blue"))
        {
            if (other.GC<asd>().isObsEnter)
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
            if (other.GC<asd>().isObsEnter)
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


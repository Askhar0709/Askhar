using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BotRandomMoveController : MonoBehaviour
{
    public float velocity = 10, rotSpeed = 5, minTime = 1.5f, maxTime = 3;
    float curAngle = 0, angle = 0, t, dt = 0;
    Vector3 mp, vel;
    Rigidbody rb;
    void Start()
    {
        rb = gameObject.GC<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
    }
    void Update()
    {
        if (A.IsPlaying)
        {
            dt += Time.deltaTime;
            if (dt > t)
            {
                dt = 0;
                t = Random.Range(minTime, maxTime);
                angle = A.RndAng;
            }
            curAngle = A.Lerp(curAngle, angle, Time.deltaTime * rotSpeed);
            transform.rotation = Quaternion.Euler(0, curAngle, 0);
            rb.velocity = transform.forward * velocity;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }
}

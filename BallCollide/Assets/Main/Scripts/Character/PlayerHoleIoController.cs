using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class PlayerHoleIoController : MonoBehaviour {
    public float speed = 10, radius = 100;
    public bool isPlaying = false;
    float dis;
    Vector3 mp;
    Rigidbody rb;
    void Start () {
        rb = gameObject.GC<Rigidbody> ();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
    }
    void Update () {
        if (A.IsPlaying || isPlaying) {
            if (Input.GetMouseButtonDown (0)) {
                mp = Input.mousePosition;
            }
            if (Input.GetMouseButton (0)) {
                dis = Vector3.Distance (Input.mousePosition, mp);
                if (dis > radius) {
                    mp = Vector3.MoveTowards (Input.mousePosition, mp, radius);
                }
                transform.rotation = Quaternion.Euler (0, A.AngLookForward (mp, Input.mousePosition), 0);
                rb.velocity = transform.forward * Mathf.Clamp01 (dis / radius) * speed;
            }
            if (Input.GetMouseButtonUp (0)) {
                rb.velocity = Vector3.zero;
            }
        } else {
            rb.velocity = Vector3.zero;
        }
    }
}
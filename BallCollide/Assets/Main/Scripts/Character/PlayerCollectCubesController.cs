using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class PlayerCollectCubesController : MonoBehaviour {
    public float speed = 10, posScl = 0.02f, disTh = 10;
    public bool isPlaying = false;
    float dis;
    Vector3 mp, staPos, pos;
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
                staPos = transform.position;
            }
            if (Input.GetMouseButton (0)) {
                pos = staPos + new Vector3 (Input.mousePosition.x - mp.x, 0, Input.mousePosition.y - mp.y) * posScl;
            }
            dis = Vector3.Distance (transform.position, pos);
            if (dis < 0.1f) {
                transform.position = pos;
                rb.velocity = Vector3.zero;
            } else {
                transform.LookAt (pos);
                rb.velocity = transform.forward * Mathf.Clamp01 (dis / disTh) * speed;
            }
        } else {
            rb.velocity = Vector3.zero;
        }
    }
    private void OnDrawGizmos () {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere (pos, 0.5f);
    }
}
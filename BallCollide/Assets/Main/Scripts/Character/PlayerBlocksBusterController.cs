using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class PlayerBlocksBusterController : MonoBehaviour {
    public float speed = 10, posScl = 0.02f;
    public bool isPlaying = false;
    float dis;
    Vector3 mp, pos;
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
                pos = transform.position + new Vector3 (Input.mousePosition.x - mp.x, 0, Input.mousePosition.y - mp.y) * posScl;
                // transform.LookAt (pos);
                rb.MovePosition (pos);
                mp = Input.mousePosition;
            }
            if (Input.GetMouseButtonUp (0)) {
                rb.velocity = Vector3.zero;
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
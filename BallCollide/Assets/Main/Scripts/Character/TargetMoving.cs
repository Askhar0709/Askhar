using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMoving : MonoBehaviour
{
    public Transform[] target;
    public float speed;
    private int current;
    public float radius;
    private Rigidbody rig;
    private void Start() {
        rig= GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       if(transform.position != target[current].position)
       {
           Vector3 pos = Vector3.MoveTowards(transform.position, target[current].position,speed*Time.deltaTime);
           
            rig.MovePosition(pos);
            
           
       } 
       else
           current = (current+1)%target.Length;
       
    }
}

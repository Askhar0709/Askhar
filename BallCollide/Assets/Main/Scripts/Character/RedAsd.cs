using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
public class RedAsd : MonoBehaviour
{
   
    public PathCreator pathCreator;
    public float speed;
    float distanceTravelled;
    public ParticleSystem sparke;
    void Start()
    {
    }
    void FixedUpdate()
    {
    distanceTravelled += speed * Time.deltaTime;
    transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
    transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("blue"))
        {
            Destroy(gameObject);
            sparke.transform.position=transform.position;
            sparke.Play();
        }
    }
}
 

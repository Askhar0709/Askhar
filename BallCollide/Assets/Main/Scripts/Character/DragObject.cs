using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class DragObject : MonoBehaviour

{
    public Mesh mesh;
    public GameObject zam;
    public bool isLoop;
    public List<Vector3> points;
   public Vector3 dpos;
    public Vector2 size;
    // public BoxCollider colliders;

    // private Vector3 mOffset;



    // private float mZCoord;

    private void FixedUpdate()
    {
        A.MeshInit(ref mesh,zam);
        A.MeshRoad(ref mesh,isLoop,points,dpos,size);
        // colliders.enabled = false;
    }

    // void OnMouseDown()

    // {

    //     mZCoord = Camera.main.WorldToScreenPoint(

    //         gameObject.transform.position).z;



    //     // Store offset = gameobject world pos - mouse world pos

    //     mOffset = gameObject.transform.position - GetMouseAsWorldPoint();
    //     colliders.enabled = true;


    // }



    // private Vector3 GetMouseAsWorldPoint()

    // {

    //     // Pixel coordinates of mouse (x,y)

    //     Vector3 mousePoint = Input.mousePosition;



    //     // z coordinate of game object on screen

    //     mousePoint.z = mZCoord;



    //     // Convert it to world points

    //     return Camera.main.ScreenToWorldPoint(mousePoint);

    // }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mousebutton : MonoBehaviour
{
    public BoxCollider bc;
    private void OnMouseDown()
    {
      bc.isTrigger =!bc.isTrigger;
      transform.Rotate(0,90,0);
    }
}

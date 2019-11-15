using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    Vector3 mp;
    private void Awake()
    {
        Init();
    }
    void Start() { }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseButtonDown();
        }
    }
    public void MouseButtonDown()
    {
        mp = Input.mousePosition;
    }
}
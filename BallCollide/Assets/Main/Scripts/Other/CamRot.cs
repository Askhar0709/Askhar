﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRot : MonoBehaviour {
    void Update () {
        if (Camera.main.NotNull ())
            transform.rotation = Camera.main.transform.rotation;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkText : MonoBehaviour {
    public float duration = 1;
    Text text;
    bool isAlphaDown = true;
    float t;
    void Start () {
        t = duration;
        text = gameObject.GC<Text> ();
    }
    void Update () {
        t += isAlphaDown ? -Time.deltaTime : Time.deltaTime;
        isAlphaDown = t < 0 || t > duration ? !isAlphaDown : isAlphaDown;
        text.color = A.ColA (text.color, t / duration);
    }
}
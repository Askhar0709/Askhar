using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHand : MonoBehaviour {
    public Transform handTf;
    public List<Vector3> points;
    List<Vector3> lis = new List<Vector3> ();
    public int pointSpacing = 10;
    public float speed = 1;
    float dis = 0;
    void Start () {
        CreatePoints ();
        ConstSpacing ();
    }
    void Update () {
        handTf.position = transform.TransformPoint (GetPoint (dis));
        dis += Time.deltaTime * speed;
    }
    void CreatePoints () {
        lis.Clear ();
        for (int i = -1; i < points.Count - 1; i++) {
            lis = A.Add2List<Vector3> (lis,
                A.CatmullRomSpline (
                    points[A.RepIdx (i, points.Count)],
                    points[A.RepIdx (i + 1, points.Count)],
                    points[A.RepIdx (i + 2, points.Count)],
                    points[A.RepIdx (i + 3, points.Count)],
                    10, false, true));
        }
    }
    void ConstSpacing () {
        Vector3 p = lis[0];
        List<Vector3> tmpLis = new List<Vector3> () { p };
        for (int i = 0; i < lis.Count;) {
            if (Vector3.Distance (p, lis[i]) >= pointSpacing) {
                p = Vector3.MoveTowards (p, lis[i], pointSpacing);
                tmpLis.Add (p);
            } else {
                i++;
            }
        }
        lis = tmpLis;
    }
    Vector3 GetPoint (float dis) {
        int i = Mathf.FloorToInt (dis / pointSpacing) % lis.Count, i2 = (i + 1) % lis.Count;
        return Vector3.Lerp (lis[i], lis[i2], dis % pointSpacing / pointSpacing);
    }
}
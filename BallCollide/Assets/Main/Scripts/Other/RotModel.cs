using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class RotModel : MonoBehaviour
{
    public bool isFill = true, isDisUv = true;
    public int n = 8;
    public List<Vector2> points = new List<Vector2>() {
        new Vector2 (-0.5f, -0.5f),
        new Vector2 (-0.5f, 0.5f),
    };
    public Tf tf = new Tf(Vector3.zero, Quaternion.identity, Vector3.one);
    Mesh mesh;
    public void UpdateMesh()
    {
        A.MeshInit(ref mesh, gameObject);
        A.MeshRotModel(ref mesh, isFill, isDisUv, n, points, tf);
    }
}
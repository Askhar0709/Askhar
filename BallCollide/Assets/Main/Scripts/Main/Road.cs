using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[ExecuteInEditMode]
public class Road : MonoBehaviour
{
    public List<Vector3> pnts = new List<Vector3>() { Vector3.zero, Vector3.forward };
    public bool isLoop = false;
    public Vector2 size = Vector2.one;
    public Vector3 dPos = Vector3.zero;
    public int smooth = 3;
    public float betDis = 0.5f;
    Mesh mesh;
    // private void OnEnable() {
    //     UpdateMesh();
    // }
    // private void OnValidate() {
    //     UpdateMesh();
    // }
    private void Update()
    {
        UpdateMesh();
    }
    void UpdateMesh()
    {
        A.MeshInit(ref mesh, gameObject);
        List<Vector3> lis = new List<Vector3>();
        for (int i = 0; i <= pnts.Count - 4; i++)
        {
            lis.Add<Vector3>(A.CatmullRomSpline(pnts[i], pnts[i + 1], pnts[i + 2], pnts[i + 3], smooth, true, false));
        }
        lis.Add(pnts[pnts.Count - 2]);
        lis.Insert(0, pnts[0]);
        lis.Add(pnts[pnts.Count - 1]);
        lis = A.PntsSameDisBetPnts(lis, betDis);
        A.MeshRoad(ref mesh, isLoop, lis, dPos, size);
    }
}

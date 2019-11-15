using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ForwardHouseSpawner : MonoBehaviour {
    public float roadWidth = 30;
    float leftLen = 0, rightLen = 0, startZ = 0;
    List<GameObject> buildings = new List<GameObject> ();
    private void Awake () {
        buildings = Resources.LoadAll<GameObject> ("Shop").ToList ();
    }
    private void Start () {
        CreateLeftBuildings ();
    }
    void CreateLeftBuildings () {
        while (leftLen < 100) {
            GameObject buildingPf = A.RndList<GameObject> (buildings);
            Vector3 size = GetSize (buildingPf);
            Vector3 pos = new Vector3 (-roadWidth / 2, 0, startZ + leftLen) + new Vector3 (-size.x / 2, 0, size.z / 2);
            GameObject building = Instantiate (buildingPf, pos, Quaternion.Euler (0, 0, 0), transform);
            leftLen += size.z;
        }
        while (rightLen < 100) {
            GameObject buildingPf = A.RndList<GameObject> (buildings);
            Vector3 size = GetSize (buildingPf);
            Vector3 pos = new Vector3 (roadWidth / 2, 0, startZ + rightLen) + new Vector3 (size.x / 2, 0, size.z / 2);
            GameObject building = Instantiate (buildingPf, pos, Quaternion.Euler (0, 180, 0), transform);
            rightLen += size.z;
        }
    }
    Vector3 GetSize (GameObject go) {
        return Vector3.Scale (go.transform.lossyScale, go.GetComponent<BoxCollider> ().size);
    }
}
using UnityEngine;
using System.Collections;

public class HWPRayHelper : MonoBehaviour {

    public float DistanceCheck = 50f;

    private HWP cacheHud = null;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        
        Vector3 fwr = this.transform.forward;
        Debug.DrawRay(this.transform.position,fwr, Color.green);

        if (Physics.Raycast(this.transform.position, fwr, out hit, DistanceCheck))
        {
            if (hit.transform.GetComponent<HWP>() != null)
            {
                if (hit.transform.GetComponent<HWP>().info.ShowDynamically)
                {
                    cacheHud = hit.transform.GetComponent<HWP>();
                    cacheHud.Show();
                }
            }
        }
        else
        {
            if (cacheHud != null)
            {
                cacheHud.Hide();
                cacheHud = null;
            }
        }
    }
}
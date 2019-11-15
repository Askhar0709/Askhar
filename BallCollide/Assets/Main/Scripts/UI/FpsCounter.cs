using UnityEngine;
using UnityEngine.UI;

public class FpsCounter : MonoBehaviour
{
    public UIData fps;
    int n = 0;
    float dt = 0f;
    void Update()
    {
        dt += Time.deltaTime;
        n++;
        if (dt >= 0.999f)
        {
            fps.DataHudData(n);
            dt = 0f;
            n = 0;
        }
    }
}
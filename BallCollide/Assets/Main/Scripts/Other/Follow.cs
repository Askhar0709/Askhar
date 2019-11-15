using UnityEngine;

public class Follow : MonoBehaviour {
    public Transform followTf;
    private void Update () {
        if (followTf.NotNull ())
            transform.position = followTf.position;
    }
}
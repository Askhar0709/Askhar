using UnityEngine;
using UnityEngine.UI;
public enum UIButtonType { Pause, Settings, Replay, Resume, Play, Save, Done, Clear }
public class UIButton : MonoBehaviour {
    public UIButtonType type;
    public void Click () {
        print ("Button click " + type);
        switch (type) {
            case UIButtonType.Pause:
                A.CC.Pause ();
                break;
            case UIButtonType.Settings:
                A.GS.Settings ();
                break;
            case UIButtonType.Replay:
                A.CC.Replay ();
                break;
            case UIButtonType.Resume:
                A.CC.Resume ();
                break;
            case UIButtonType.Play:
                A.CC.Play ();
                break;
            case UIButtonType.Save:
                A.GS.Save ();
                break;
            case UIButtonType.Done:
                A.GS.Done ();
                break;
            case UIButtonType.Clear:
                A.GS.Clear ();
                break;
        }
    }
}
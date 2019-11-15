using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public static class EditorUtils
{
    [MenuItem("Utils/Screenshot " + Alt + "1")]
    public static void Screenshot() { A.Screenshot("Screenshots/", "Screenshot" + System.DateTime.Now.ToString("_yyyy-MM-dd_hh-mm-ss")); }
    [MenuItem("Utils/Focus Player " + Alt + "2")]
    public static void FocusPlayer() { ActiveFocus(A.Player.gameObject); }
    [MenuItem("Utils/GameOver Replay " + Alt + "3")]
    public static void Enter()
    {
        if (A.IsGameOver || A.IsLevelCompleted) A.CC.Replay();
        else if (A.IsStarting)
            if (A.GC.isStartButton) A.CC.Play();
            else A.GC.Play();
        else if (A.IsPause) A.CC.Resume();
        else if (A.IsSettings) A.GS.Done();
        else if (A.IsPlaying) A.CC.Pause();
    }
    [MenuItem("Utils/DeletePlayerPrefs " + Alt + "4")]
    public static void DeletePlayerPrefs() { PlayerPrefs.DeleteAll(); }
    static float dAng = 10;
    static void Active(GameObject go) { Selection.activeGameObject = go; }
    static void Focus() { SceneView.FrameLastActiveSceneView(); }
    static void ActiveFocus(GameObject go) { Active(go); Focus(); }
    // static void SceneViewNavigation(Vector3 v) { SceneView.lastActiveSceneView.rotation = Quaternion.Euler(v); }
    // static void SceneViewNavigationRotate(Vector3 v) { SceneViewNavigation(SceneView.lastActiveSceneView.rotation.eulerAngles + v); }

    // [MenuItem("Utils/Scene View Navigation/Left " + CtrlAlt + Left, false, 0)]
    // public static void SceneViewNavigationLeft() { SceneViewNavigation(Vector3.up * 90); }

    // [MenuItem("Utils/Scene View Navigation/Right " + CtrlShiftAlt + Right, false, 0)]
    // public static void SceneViewNavigationRight() { SceneViewNavigation(Vector3.up * -90); }

    // [MenuItem("Utils/Scene View Navigation/Front " + CtrlShiftAlt + Up, false, 0)]
    // public static void SceneViewNavigationFront() { SceneViewNavigation(Vector3.up * 180); }

    // [MenuItem("Utils/Scene View Navigation/Back " + CtrlShiftAlt + Down, false, 0)]
    // public static void SceneViewNavigationBack() { SceneViewNavigation(Vector3.zero); }

    // [MenuItem("Utils/Scene View Navigation/Top " + CtrlShiftAlt + "'", false, 0)]
    // public static void SceneViewNavigationTop() { SceneViewNavigation(Vector3.right * 90); }

    // [MenuItem("Utils/Scene View Navigation/Bottom " + CtrlShiftAlt + ";", false, 0)]
    // public static void SceneViewNavigationBottom() { SceneViewNavigation(Vector3.right * -90); }

    // [MenuItem("Utils/Scene View Navigation/Rotate Left " + Ctrl + Left, false, 100)]
    // public static void SceneViewNavigationRotateLeft() { SceneViewNavigationRotate(Vector3.up * dAng); }

    // [MenuItem("Utils/Scene View Navigation/Rotate Right " + Ctrl + Right, false, 100)]
    // public static void SceneViewNavigationRotateRight() { SceneViewNavigationRotate(Vector3.down * dAng); }

    // [MenuItem("Utils/Scene View Navigation/Rotate Up " + Ctrl + "'", false, 100)]
    // public static void SceneViewNavigationRotateUp() { SceneViewNavigationRotate(Vector3.right * dAng); }

    // [MenuItem("Utils/Scene View Navigation/Rotate Down " + Ctrl + ";", false, 100)]
    // public static void SceneViewNavigationRotateDown() { SceneViewNavigationRotate(Vector3.left * dAng); }
    public const string Ctrl = "%",
        Shift = "#",
        Alt = "&",
        Left = "LEFT",
        Right = "RIGHT",
        Up = "UP",
        Down = "DOWN",
        F1 = "F1",
        F2 = "F2",
        F3 = "F3",
        F4 = "F4",
        F5 = "F5",
        F6 = "F6",
        F7 = "F7",
        F8 = "F8",
        F9 = "F9",
        F10 = "F10",
        F11 = "F11",
        F12 = "F12",
        Home = "HOME",
        End = "END",
        PgUp = "PGUP",
        PgDn = "PGDN",
        CtrlShiftAlt = Ctrl + Shift + Alt,
        CtrlShift = Ctrl + Shift,
        CtrlAlt = Ctrl + Alt,
        ShiftAlt = Shift + Alt;
}
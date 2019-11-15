using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider), typeof(SphereCollider))]
public class Test : MonoBehaviour
{
    public AnimationCurve HermiteAc;
    public AnimationCurve SinerpAc;
    public AnimationCurve CoserpAc;
    public AnimationCurve BerpAc;
    public AnimationCurve SmoothStepAc;
    public AnimationCurve LerpAc;
    public AnimationCurve BounceAc;
    public AnimationCurve ClerpAc;
    public AnimationCurve MSmoothStepAc;
    [HideInInspector]
    public bool hideInInspector;
    [Range(0, 100)]
    public float range0_100;
    [Multiline(4)]
    public string multiline4;
    [TextArea]
    public string textArea;
    [ColorUsage(false, false)]
    public Color colorUsageFalseFalse;
    [ColorUsage(false, true)]
    public Color colorUsageFalseTrue;
    [ColorUsage(true, false)]
    public Color colorUsageTrueFalse;
    [ColorUsage(true, true)]
    public Color colorUsageTrueTrue;
    public int spaceStart10;
    [Space(10)]
    public int spaceEnd10;
    [Header("Header")]
    public int header;
    [Tooltip("Tooltip")]
    public int toolTip;
    [SerializeField]
    private int serializeField;
    public SystemSerializable systemSerializable;
    [System.Serializable]
    public class SystemSerializable { public int id = 0; }
    public void Vibrate(int i)
    {
        A.Vibrate((Vib)i);
    }

    [ContextMenu("ContextMenu")]
    public void ContextMenu()
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }
    //Ease in out
    public static float SmoothStep(float x, float min, float max)
    {
        x = Mathf.Clamp(x, min, max);
        float v1 = (x - min) / (max - min);
        float v2 = (x - min) / (max - min);
        return -2 * v1 * v1 * v1 + 3 * v2 * v2;
    }
    public static float Lerp(float start, float end, float value) { return ((1.0f - value) * start) + (value * end); }

    /*
      * CLerp - Circular Lerp - is like lerp but handles the wraparound from 0 to 360.
      * This is useful when interpolating eulerAngles and the object
      * crosses the 0/360 boundary.  The standard Lerp function causes the object
      * to rotate in the wrong direction and looks stupid. Clerp fixes that.
      */
    public static float Clerp(float start, float end, float value)
    {
        float min = 0.0f;
        float max = 360.0f;
        float half = Mathf.Abs((max - min) / 2.0f);//half the distance between min and max
        float retval = 0.0f;
        float diff = 0.0f;

        if ((end - start) < -half)
        {
            diff = ((max - start) + end) * value;
            retval = start + diff;
        }
        else if ((end - start) > half)
        {
            diff = -((max - end) + start) * value;
            retval = start + diff;
        }
        else retval = start + (end - start) * value;

        // Debug.Log("Start: "  + start + "   End: " + end + "  Value: " + value + "  Half: " + half + "  Diff: " + diff + "  Retval: " + retval);
        return retval;
    }
    public void Button()
    {
        HermiteAc.keys = new Keyframe[0];
        SinerpAc.keys = new Keyframe[0];
        CoserpAc.keys = new Keyframe[0];
        BerpAc.keys = new Keyframe[0];
        SmoothStepAc.keys = new Keyframe[0];
        LerpAc.keys = new Keyframe[0];
        BounceAc.keys = new Keyframe[0];
        ClerpAc.keys = new Keyframe[0];
        MSmoothStepAc.keys = new Keyframe[0];
        for (int i = 0, n = 100; i <= n; i++)
        {
            HermiteAc.AddKey(i / (float)n, A.Hermite(0, 10, i / (float)n));
            SinerpAc.AddKey(i / (float)n, A.Sinerp(1, 10, i / (float)n));
            CoserpAc.AddKey(i / (float)n, A.Coserp(1, 11, i / (float)n));
            BerpAc.AddKey(i / (float)n, A.Berp(1, 10, i / (float)n));
            SmoothStepAc.AddKey(i / (float)n, SmoothStep(i / (float)n, 0, 1));
            LerpAc.AddKey(i / (float)n, Lerp(0, 1, i / (float)n));
            BounceAc.AddKey(i / (float)n, A.Bounce(1, 10, i / (float)n));
            ClerpAc.AddKey(i / (float)n, Clerp(-360, 360, i / (float)n));
            MSmoothStepAc.AddKey(i / (float)n, Mathf.SmoothStep(0, 1, i / (float)n));
        }
        // print(A.RepIdx(idx, n));
        // transform.Translate()
        // transform.LookAt(Vector3.zero)
        //     string[] arr = new string[] { "", "l", "r", "b7.5", "b7.5l", "b7.5r" };
        //     string prvPath = "Main\\Resources\\Rect\\";
        //     for (int i = 5; i <= 100; i += i == 5 ? 5 : 10) {
        //         for (int j = 0; j < arr.Length; j++) {
        //             if ((i == 5 && j == 0) || (i == 10 && j <= 2) || i > 10) {
        //                 Vector4 border = Vector4.zero;
        //                 switch (j) {
        //                     case 0:
        //                         border = A.SprBorder (i, i, i, i);
        //                         break;
        //                     case 1:
        //                         border = A.SprBorder (i, 0, i, i);
        //                         break;
        //                     case 2:
        //                         border = A.SprBorder (0, i, i, i);
        //                         break;
        //                     case 3:
        //                         border = A.SprBorder (i, i, i, i);
        //                         break;
        //                     case 4:
        //                         border = A.SprBorder (i, 0, i, i);
        //                         break;
        //                     case 5:
        //                         border = A.SprBorder (0, i, i, i);
        //                         break;
        //                 }
        //                 A.SetSprBorder (prvPath + "r" + i + arr[j] + ".png.meta", border);
        //             }
        //         }
        //     }
    }
}
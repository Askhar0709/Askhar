using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Test))]
public class TestEditor : Editor
{
    int selectionGridIdx = 0;
    string[] selectionGridArr = new string[] { "SelectionGrid 0", "SelectionGrid 1", "SelectionGrid 2", "SelectionGrid 3" };
    int toolbarIdx = 0;
    string[] toolbarArr = new string[] { "Toolbar 0", "Toolbar 1", "Toolbar 2", "Toolbar 3" };
    string textArea = "TextArea TextArea TextArea TextArea TextArea TextArea TextArea TextArea TextArea TextArea ";
    string passwordField = "PasswordField";
    string textField = "TextField";
    bool isToggle = true;
    float horizontalSlider = 0, verticalSlider = 0;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Label("Label");

        GUILayout.Space(20);

        isToggle = GUILayout.Toggle(isToggle, "Toggle");
        if (GUILayout.Button("Button")){
            print("Button");
            ((Test)target).Button();
        }
        if (GUILayout.RepeatButton("RepeatButton"))
            print("RepeatButton");

        GUILayout.Space(20);

        selectionGridIdx = GUILayout.SelectionGrid(selectionGridIdx, selectionGridArr, 2);
        toolbarIdx = GUILayout.Toolbar(toolbarIdx, toolbarArr);

        GUILayout.Space(20);

        textField = GUILayout.TextField(textField, 25);
        textArea = GUILayout.TextArea(textArea, 200);
        passwordField = GUILayout.PasswordField(passwordField, "*"[0], 25);

        GUILayout.Space(20);

        horizontalSlider = GUILayout.HorizontalSlider(horizontalSlider, 0, 10);
        verticalSlider = GUILayout.VerticalSlider(verticalSlider, 10, 0);
    }
    void print(object obj)
    {
        Debug.Log(obj);
    }
}

[CustomEditor(typeof(GameController))]
public class GameControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Sprites Screenshot"))
            A.Screenshot("Assets/Main/Sprites", A.GC.screenshotName);
        if (GUILayout.Button("Sprites Read"))
        {
            A.GC.icon = A.LoadAsset<Texture2D>("Assets/Main/Sprites/icon.png");
            A.GC.iPhoneLaunchScreen = A.LoadAsset<Texture2D>("Assets/Main/Sprites/iphone.png");
            A.GC.iPadLaunchScreen = A.LoadAsset<Texture2D>("Assets/Main/Sprites/ipad.png");
        }
        if (GUILayout.Button("Player Settings Change"))
            A.PlayerSettingsChange();
    }
}

[CustomEditor(typeof(RotModel))]
public class RotModelEditor : Editor
{
    RotModel model;
    void OnEnable()
    {
        model = (RotModel)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Button"))
        {
            RotModel obj = (RotModel)target;
            ObjExporter.MeshToFile(obj.GC<MeshFilter>(), "Assets/" + obj.name + ".obj");
        }
    }
    void OnSceneGUI()
    {
        List<Vector2> lis = new List<Vector2>(model.points);
        for (int i = 0; i < model.points.Count; i++)
        {
            model.points[i] = model.transform.InverseTransformPoint(Handles.PositionHandle(
                model.transform.TransformPoint(model.points[i]),
                Quaternion.identity
            ));
        }
        model.UpdateMesh();
    }
}

[CustomEditor(typeof(TutorialHand))]
public class TutorialHandEditor : Editor
{
    TutorialHand hand;
    void OnEnable()
    {
        hand = (TutorialHand)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Button")) { }
    }
    void OnSceneGUI()
    {
        for (int i = 0; i < hand.points.Count; i++)
        {
            hand.points[i] = hand.transform.InverseTransformPoint(
                Handles.PositionHandle(
                    hand.transform.TransformPoint(hand.points[i]),
                    Quaternion.identity
                ));
        }
    }
}

[CustomEditor(typeof(UIChange))]
public class TutorialUIChangeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Init")) ((UIChange)target).Alpha();
        if (GUILayout.Button("Color")) ((UIChange)target).Color();
        if (GUILayout.Button("Font")) ((UIChange)target).Font();
    }
}

[CustomPropertyDrawer(typeof(DrawIfAttribute))]
public class DrawIfPropertyDrawer : PropertyDrawer
{
    DrawIfAttribute attr;
    SerializedProperty comparedField;
    private float propertyHeight;
    bool IsNumericType(Type t) { return IsInt(t) || IsLong(t) || IsFloat(t) || IsDouble(t); }
    bool IsInt(Type t) { return t == typeof(sbyte) || t == typeof(byte) || t == typeof(short) || t == typeof(ushort) || t == typeof(int) || t == typeof(uint); }
    bool IsLong(Type t) { return t == typeof(long) || t == typeof(ulong); }
    bool IsFloat(Type t) { return t == typeof(float); }
    bool IsDouble(Type t) { return t == typeof(double) || t == typeof(decimal); }
    object GetValue(Type t, SerializedProperty sp)
    {
        object res = null;
        if (t == typeof(AnimationCurve)) res = sp.animationCurveValue;
        else if (t == typeof(bool)) res = sp.boolValue;
        else if (t == typeof(BoundsInt)) res = sp.boundsIntValue;
        else if (t == typeof(Bounds)) res = sp.boundsValue;
        else if (t == typeof(Color)) res = sp.colorValue;
        else if (IsDouble(t)) res = sp.doubleValue;
        else if (IsFloat(t)) res = sp.floatValue;
        else if (IsInt(t) || t == typeof(char)) res = sp.intValue;
        else if (IsLong(t)) res = sp.longValue;
        else if (t == typeof(object)) res = sp.objectReferenceValue;
        else if (t == typeof(Quaternion)) res = sp.quaternionValue;
        else if (t == typeof(RectInt)) res = sp.rectIntValue;
        else if (t == typeof(Rect)) res = sp.rectValue;
        else if (t == typeof(string)) res = sp.stringValue;
        else if (t == typeof(Vector2Int)) res = sp.vector2IntValue;
        else if (t == typeof(Vector2)) res = sp.vector2Value;
        else if (t == typeof(Vector3Int)) res = sp.vector3IntValue;
        else if (t == typeof(Vector3)) res = sp.vector3Value;
        else if (t == typeof(Vector4)) res = sp.vector4Value;
        return res;
    }
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return propertyHeight;
    }
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        attr = attribute as DrawIfAttribute;
        object value = GetValue(attr.type, property.serializedObject.FindProperty(attr.name));
        bool active = false;
        if (attr.comparisonType == ComparisonType.Equals)
        {
            if (value.Equals(attr.value))
                active = true;
        }
        else if (attr.comparisonType == ComparisonType.NotEqual)
        {
            if (!value.Equals(attr.value))
                active = true;
        }
        else if (IsNumericType(attr.type))
        {
            double a = 0, b = 0;
            if (IsInt(attr.type))
            {
                a = (int)value;
                b = (int)attr.value;
            }
            else if (IsLong(attr.type))
            {
                a = (long)value;
                b = (long)attr.value;
            }
            else if (IsFloat(attr.type))
            {
                a = (float)value;
                b = (float)attr.value;
            }
            else if (IsDouble(attr.type))
            {
                a = (double)value;
                b = (double)attr.value;
            }
            switch (attr.comparisonType)
            {
                case ComparisonType.GreaterThan:
                    if (a > b) active = true;
                    break;
                case ComparisonType.SmallerThan:
                    if (a < b) active = true;
                    break;
                case ComparisonType.SmallerOrEqual:
                    if (a <= b) active = true;
                    break;
                case ComparisonType.GreaterOrEqual:
                    if (a >= b) active = true;
                    break;
            }
        }
        propertyHeight = base.GetPropertyHeight(property, label);
        if (active)
        {
            EditorGUI.PropertyField(position, property);
        }
        else
        {
            if (attr.disablingType == DisablingType.ReadOnly)
            {
                GUI.enabled = false;
                EditorGUI.PropertyField(position, property);
                GUI.enabled = true;
            }
            else
            {
                propertyHeight = 0f;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// ▄▀▀▀▄ ▄▀▀▀▄ █▄ ▄█ █▀▀▀▀ ▄▀▀▀▄ █▀▀▀▄   ▀█▀ █▀▀▀▀ ▄▀▀▀▄ ▀▀█▀▀ 
// █     █▄▄▄█ █ █ █ █▄▄▄  █   █ █▄▄▄▀    █  █▄▄▄  █       █   
// █  ▀█ █   █ █   █ █     █   █ █   █ ▄  █  █     █   ▄   █   
//  ▀▀▀  ▀   ▀ ▀   ▀ ▀▀▀▀▀  ▀▀▀  ▀▀▀▀   ▀▀   ▀▀▀▀▀  ▀▀▀    ▀

public static class ExtensionGameObject
{
    public static bool Null(this GameObject a) { return a == null; }
    public static bool NotNull(this GameObject a) { return a != null; }
    // get component
    public static T GC<T>(this GameObject a) { return a.GetComponent<T>(); }
    public static T GCIC<T>(this GameObject a) { return a.GetComponentInChildren<T>(); }
    public static T GCIP<T>(this GameObject a) { return a.GetComponentInParent<T>(); }
    public static T[] GCs<T>(this GameObject a) { return a.GetComponents<T>(); }
    public static T[] GCsIC<T>(this GameObject a) { return a.GetComponentsInChildren<T>(); }
    public static T[] GCsIP<T>(this GameObject a) { return a.GetComponentsInParent<T>(); }
    public static List<T> GCA<T>(this GameObject a) { return a.Contains<T>() ? a.GCs<T>().ToList().Add<T>(a.GCsIC<T>()) : a.GCsIC<T>().ToList(); }
    // contains
    public static bool Contains<T>(this GameObject a) { return a.GC<T>().NotNull(); }
    // destroy
    public static void DestroyChilds<T>(this GameObject a, int sta = 0, int end = -1)
    {
        List<T> lis = a.Childs<T>();
        for (int i = sta, n = end < 0 ? lis.Count : end; i < n; i++)
            ((MonoBehaviour)(object)lis[i]).gameObject.Destroy();
    }
    public static void DestroyChilds(this GameObject a, int sta = 0, int end = -1) { a.DestroyChilds<GameObject>(sta, end); }
    public static void Destroy(this GameObject a, float t = 0) { MonoBehaviour.Destroy(a, t); }
    // color
    public static Color UICol(this GameObject a, Color col = default(Color))
    {
        if (a.GC<Image>().NotNull()) return a.GC<Image>().color;
        else if (a.GC<Text>().NotNull()) return a.GC<Text>().color;
        else if (a.GC<TextMesh>().NotNull()) return a.GC<TextMesh>().color;
        else if (a.GC<TextMeshPro>().NotNull()) return a.GC<TextMeshPro>().color;
        else return col;
    }
    // parent
    public static Transform Parent(this GameObject a, int n = 0)
    {
        Transform tf = a.transform.parent;
        for (int i = 0; i < n; i++)
            tf = tf.parent;
        return tf;
    }
    public static GameObject ParentGo(this GameObject a, int n = 0) { return a.Parent(n).gameObject; }
    public static T Parent<T>(this GameObject a, int n = 0) { return a.ParentGo(n).GC<T>(); }
    // childs
    public static List<T> Childs<T>(this GameObject a)
    {
        List<T> lis = new List<T>();
        for (int i = 0; i < a.transform.childCount; i++)
            if (a.Child(i).Contains<T>())
                lis.Add(a.Child<T>(i));
        return lis;
    }
    public static List<GameObject> ChildGos(this GameObject a) { return a.Childs<GameObject>(); }
    public static List<Transform> Childs(this GameObject a) { return a.Childs<Transform>(); }
    // find
    public static Transform Fnd(this GameObject a, string name) { return a.transform.Find(name); }
    public static GameObject FndGo(this GameObject a, string name) { return a.Fnd(name).gameObject; }
    public static T Fnd<T>(this GameObject a, string name) { return a.Fnd(name).GC<T>(); }
    // child
    public static Transform Child(this GameObject a, params int[] childs)
    {
        Transform tf = a.transform;
        for (int i = 0; i < childs.Length; i++)
            tf = tf.GetChild(childs[i]);
        return tf;
    }
    public static Transform Child(this GameObject a, string childs) { return a.Child(childs.RegexSplit("\\D+").Parse(x => int.Parse(x))); }
    public static GameObject ChildGo(this GameObject a, params int[] childs) { return a.Child(childs).gameObject; }
    public static GameObject ChildGo(this GameObject a, string childs) { return a.Child(childs).gameObject; }
    public static T Child<T>(this GameObject a, params int[] childs) { return a.ChildGo(childs).GC<T>(); }
    public static T Child<T>(this GameObject a, string childs) { return a.ChildGo(childs).GC<T>(); }
    // active
    public static bool Enabled(this GameObject a) { return a.NotNull() && a.activeSelf; }
    public static void Active(this GameObject a, bool active) { a.SetActive(active); }
    public static void Show(this GameObject a) { a.Active(true); }
    public static void Hide(this GameObject a) { a.Active(false); }
    public static void ChildActive(this GameObject a, bool active, params int[] childs) { a.ChildGo(childs).Active(active); }
    public static void ChildActive(this GameObject a, bool active, string childs) { a.ChildGo(childs).Active(active); }
    public static void ChildHide(this GameObject a, params int[] childs) { a.ChildActive(false, childs); }
    public static void ChildHide(this GameObject a, string childs) { a.ChildActive(false, childs); }
    public static void ChildShow(this GameObject a, params int[] childs) { a.ChildActive(true, childs); }
    public static void ChildShow(this GameObject a, string childs) { a.ChildActive(true, childs); }
    // material
    public static void RenMatCol(this GameObject a, Color col) { a.GC<Renderer>().material.color = col; }
    public static void RenMatCol(this GameObject a, string name, Color col) { a.GC<Renderer>().material.SetColor(name, col); }
    public static List<Material> MatLis(this GameObject a)
    {
        List<Renderer> renLis = a.GCA<Renderer>().ToList();
        List<Material> matLis = new List<Material>();
        for (int i = 0; i < renLis.Count; i++)
            for (int j = 0; j < renLis[i].materials.Length; j++)
                matLis.Add(renLis[i].materials[j]);
        return matLis;
    }
    public static List<Material> MatLisTransparent(this GameObject a)
    {
        List<Material> matLis = a.MatLis();
        matLis.ForEach(m => A.MatRenMod(m, RenderingMode.Transparent));
        return matLis;
    }
    // tag
    public static bool Tag(this GameObject a, string tag) { return a.CompareTag(tag); }
};



// ▄▀▀▀▄ ▄▀▀▀▄ █▄ ▄█ █▀▀▀▄ ▄▀▀▀▄ █   █ █▀▀▀▀ █   █ ▀▀█▀▀ 
// █     █   █ █ █ █ █▄▄▄▀ █   █ █▀▄ █ █▄▄▄  █▀▄ █   █   
// █   ▄ █   █ █   █ █     █   █ █  ▀█ █     █  ▀█   █   
//  ▀▀▀   ▀▀▀  ▀   ▀ ▀      ▀▀▀  ▀   ▀ ▀▀▀▀▀ ▀   ▀   ▀

public static class ExtensionComponent
{
    public static bool Null(this Component a) { return a == null; }
    public static bool NotNull(this Component a) { return a != null; }
    // get component
    public static T GC<T>(this Component a) { return a.gameObject.GC<T>(); }
    public static T GCIC<T>(this Component a) { return a.gameObject.GCIC<T>(); }
    public static T GCIP<T>(this Component a) { return a.gameObject.GCIP<T>(); }
    public static T[] GCs<T>(this Component a) { return a.gameObject.GCs<T>(); }
    public static T[] GCsIC<T>(this Component a) { return a.gameObject.GCsIC<T>(); }
    public static T[] GCsIP<T>(this Component a) { return a.gameObject.GCsIP<T>(); }
    public static List<T> GCA<T>(this Component a) { return a.gameObject.GCA<T>(); }
    // contains
    public static bool Contains<T>(this Component a) { return a.gameObject.Contains<T>(); }
    // destroy
    public static void DestroyChilds<T>(this Component a, int sta = 0, int end = -1) { a.gameObject.DestroyChilds<T>(sta, end); }
    public static void DestroyChilds(this Component a, int sta = 0, int end = -1) { a.gameObject.DestroyChilds(sta, end); }
    public static void Destroy(this Component a, float t) { a.gameObject.Destroy(t); }
    // color
    public static Color UICol(this Component a, Color col = default(Color)) { return a.gameObject.UICol(col); }
    // parent
    public static Transform Parent(this Component a, int n = 0) { return a.gameObject.Parent(n); }
    public static GameObject ParentGo(this Component a, int n = 0) { return a.gameObject.ParentGo(n); }
    public static T Parent<T>(this Component a, int n = 0) { return a.gameObject.Parent<T>(n); }
    // childs
    public static List<GameObject> ChildGos(this Component a) { return a.gameObject.ChildGos(); }
    public static List<Transform> Childs(this Component a) { return a.gameObject.Childs(); }
    public static List<T> Childs<T>(this Component a) { return a.gameObject.Childs<T>(); }
    // find
    public static Transform Fnd(this Component a, string name) { return a.gameObject.Fnd(name); }
    public static GameObject FndGo(this Component a, string name) { return a.gameObject.FndGo(name); }
    public static T Fnd<T>(this Component a, string name) { return a.gameObject.Fnd<T>(name); }
    // child
    public static Transform Child(this Component a, params int[] childs) { return a.gameObject.Child(childs); }
    public static Transform Child(this Component a, string childs) { return a.gameObject.Child(childs); }
    public static GameObject ChildGo(this Component a, params int[] childs) { return a.gameObject.ChildGo(childs); }
    public static GameObject ChildGo(this Component a, string childs) { return a.gameObject.ChildGo(childs); }
    public static T Child<T>(this Component a, params int[] childs) { return a.gameObject.Child<T>(childs); }
    public static T Child<T>(this Component a, string childs) { return a.gameObject.Child<T>(childs); }
    // active
    public static bool Enabled(this Component a) { return a.gameObject.Enabled(); }
    public static void Active(this Component a, bool active) { a.gameObject.Active(active); }
    public static void Show(this Component a) { a.gameObject.Show(); }
    public static void Hide(this Component a) { a.gameObject.Hide(); }
    public static void ChildActive(this Component a, bool active, params int[] childs) { a.gameObject.ChildActive(active, childs); }
    public static void ChildActive(this Component a, bool active, string childs) { a.gameObject.ChildActive(active, childs); }
    public static void ChildHide(this Component a, params int[] childs) { a.gameObject.ChildHide(childs); }
    public static void ChildHide(this Component a, string childs) { a.gameObject.ChildHide(childs); }
    public static void ChildShow(this Component a, params int[] childs) { a.gameObject.ChildShow(childs); }
    public static void ChildShow(this Component a, string childs) { a.gameObject.ChildShow(childs); }
    // material
    public static void RenMatCol(this Component a, Color col) { a.gameObject.RenMatCol(col); }
    public static void RenMatCol(this Component a, string name, Color col) { a.gameObject.RenMatCol(name, col); }
    public static List<Material> MatLis(this Component a) { return a.gameObject.MatLis(); }
    public static List<Material> MatLisTransparent(this Component a) { return a.gameObject.MatLisTransparent(); }
    // tag
    public static bool Tag(this Component a, string tag) { return a.gameObject.Tag(tag); }
};



// ▄▀▀▀▄ █▀▀▀▄   ▀█▀ █▀▀▀▀ ▄▀▀▀▄ ▀▀█▀▀ 
// █   █ █▄▄▄▀    █  █▄▄▄  █       █   
// █   █ █   █ ▄  █  █     █   ▄   █   
//  ▀▀▀  ▀▀▀▀   ▀▀   ▀▀▀▀▀  ▀▀▀    ▀

public static class ExtensionObject
{
    public static bool Null(this object a) { return a == null; }
    public static bool NotNull(this object a) { return a != null; }
}



// ▄▀▀▀▄ ▄▀▀▀▄ █     █      ▀█▀  █▀▀▀▄ █▀▀▀▀ █▀▀▀▄ 
// █     █   █ █     █       █   █   █ █▄▄▄  █▄▄▄▀ 
// █   ▄ █   █ █     █       █   █   █ █     █ ▀▄  
//  ▀▀▀   ▀▀▀  ▀▀▀▀▀ ▀▀▀▀▀  ▀▀▀  ▀▀▀▀  ▀▀▀▀▀ ▀   ▀

public static class ExtensionCollider
{
    public static bool Tag(this Collision a, string tag) { return a.gameObject.Tag(tag); }
    public static bool Tag(this Collision2D a, string tag) { return a.gameObject.Tag(tag); }
    public static bool Tag(this Collider a, string tag) { return a.gameObject.Tag(tag); }
    public static bool Tag(this Collider2D a, string tag) { return a.gameObject.Tag(tag); }
    public static T GC<T>(this Collision a) { return a.gameObject.GC<T>(); }
    public static T GC<T>(this Collision2D a) { return a.gameObject.GC<T>(); }
    public static T GC<T>(this Collider a) { return a.gameObject.GC<T>(); }
    public static T GC<T>(this Collider2D a) { return a.gameObject.GC<T>(); }
    public static T Child<T>(this Collision a, string childs) { return a.gameObject.Child<T>(childs); }
    public static T Child<T>(this Collision2D a, string childs) { return a.gameObject.Child<T>(childs); }
    public static T Child<T>(this Collider a, string childs) { return a.gameObject.Child<T>(childs); }
    public static T Child<T>(this Collider2D a, string childs) { return a.gameObject.Child<T>(childs); }
    public static T Child<T>(this Collision a, params int[] childs) { return a.gameObject.Child<T>(childs); }
    public static T Child<T>(this Collision2D a, params int[] childs) { return a.gameObject.Child<T>(childs); }
    public static T Child<T>(this Collider a, params int[] childs) { return a.gameObject.Child<T>(childs); }
    public static T Child<T>(this Collider2D a, params int[] childs) { return a.gameObject.Child<T>(childs); }
    public static T Parent<T>(this Collision a, int n = 0) { return a.gameObject.Parent<T>(n); }
    public static T Parent<T>(this Collision2D a, int n = 0) { return a.gameObject.Parent<T>(n); }
    public static T Parent<T>(this Collider a, int n = 0) { return a.gameObject.Parent<T>(n); }
    public static T Parent<T>(this Collider2D a, int n = 0) { return a.gameObject.Parent<T>(n); }
};



// ▄▀▀▀▄ █▀▀▀▄ █▀▀▀▄ ▄▀▀▀▄ █   █ 
// █▄▄▄█ █▄▄▄▀ █▄▄▄▀ █▄▄▄█  ▀▄▀  
// █   █ █ ▀▄  █ ▀▄  █   █   █   
// ▀   ▀ ▀   ▀ ▀   ▀ ▀   ▀   ▀

public static class ExtensionArray
{
    public static V[] TypeCast<T, V>(this T[] arr, Func<T, V> func) { return arr.ToList().TypeCast<T, V>(func).ToArray(); }
    public static V[] TypeCast<T, V>(this T[] arr) { return arr.ToList().TypeCast<T, V>().ToArray(); }
    public static T Last<T>(this T[] arr) { return arr[arr.Length - 1]; }
    public static void Print<T>(this T[] arr) { arr.ToList().Print<T>(); }
    public static string String<T>(this T[] arr) { return arr.ToList().String<T>(); }
    public static T[] Add<T>(this T[] arr, T[] add) { return arr = A.Add2List<T>(arr.ToList(), add.ToList()).ToArray(); }
    public static T[] Add<T>(this T[] arr, List<T> add) { return arr = A.Add2List<T>(arr.ToList(), add).ToArray(); }
    public static T[] Parse<T>(this string[] arr, Func<string, T> func) { return arr.ToList().Parse<T>(func).ToArray(); }
    public static string[] RmvNull(this string[] arr) { return arr.Parse<string>(x => x); }
};



// █      ▀█▀  ▄▀▀▀▀ ▀▀█▀▀ 
// █       █   ▀▄▄▄    █   
// █       █       █   █   
// ▀▀▀▀▀  ▀▀▀  ▀▀▀▀    ▀

public static class ExtensionList
{
    public static List<V> TypeCast<T, V>(this List<T> lis, Func<T, V> func)
    {
        List<V> res = new List<V>();
        lis.ForEach(a => res.Add(func(a)));
        return res;
    }
    public static List<V> TypeCast<T, V>(this List<T> lis) { return lis.TypeCast<T, V>(a => (V)(object)a); }
    public static T Last<T>(this List<T> lis) { return lis[lis.Count - 1]; }
    public static void Print<T>(this List<T> lis) { Debug.Log(lis.String<T>()); }
    public static string String<T>(this List<T> lis)
    {
        string s = "List<" + typeof(T) + "> { ";
        lis.ForEach(x => s += x + ", ");
        return s.Substring(0, s.Length - 2) + " }";
    }
    public static List<T> Add<T>(this List<T> lis, List<T> add) { return lis = A.Add2List(lis, add); }
    public static List<T> Add<T>(this List<T> lis, T[] add) { return lis = A.Add2List(lis, add.ToList()); }
    public static List<T> Parse<T>(this List<string> lis, Func<string, T> func)
    {
        List<T> res = new List<T>();
        for (int i = 0; i < lis.Count; i++)
            if (!lis[i].IsNullOrEmpty())
                res.Add(func(lis[i]));
        return res;
    }
    public static List<string> RmvNull(this List<string> lis) { return lis.Parse<string>(x => x); }
};



// ▄▀▀▀▀ ▀▀█▀▀ █▀▀▀▄  ▀█▀  █   █ ▄▀▀▀▄ 
// ▀▄▄▄    █   █▄▄▄▀   █   █▀▄ █ █     
//     █   █   █ ▀▄    █   █  ▀█ █  ▀█ 
// ▀▀▀▀    ▀   ▀   ▀  ▀▀▀  ▀   ▀  ▀▀▀

public static class ExtensionString
{
    public static string[] RegexSplit(this string s, string regex) { return System.Text.RegularExpressions.Regex.Split(s, regex); }
    public static bool IsRegexMatch(this string s, string regex) { return System.Text.RegularExpressions.Regex.IsMatch(s, regex); }
    public static bool IsNullOrEmpty(this string s) { return string.IsNullOrEmpty(s); }
    public static string RplStaEndWth(this string s, string oldStr, string newStr) { return s.RplStaWth(oldStr, newStr).RplEndWth(oldStr, newStr); }
    public static string RplStaWth(this string s, string oldStr, string newStr) { return s.StartsWith(oldStr) ? newStr + s.Substring(oldStr.Length, s.Length - oldStr.Length) : s; }
    public static string RplEndWth(this string s, string oldStr, string newStr) { return s.EndsWith(oldStr) ? s.Substring(0, s.Length - oldStr.Length) + newStr : s; }
    // type cast
    public static string NumStr(this string s) { return s.IsRegexMatch(A.RegexFloat) ? s : "0"; }
    public static int Int(this string s) { return int.Parse(s.NumStr()); }
    public static float Float(this string s) { return float.Parse(s.NumStr()); }
    public static Vector2 Vec2(this string s) { return A.StrVec2(s); }
    public static Vector2Int Vec2I(this string s) { return A.StrVec2I(s); }
    public static Vector3 Vec3(this string s) { return A.StrVec3(s); }
    public static Vector3Int Vec3I(this string s) { return A.StrVec3I(s); }
    public static Vector4 Vec4(this string s) { return A.StrVec4(s); }
    public static Color Col(this string s) { return A.StrCol(s); }
    public static Color HexCol(this string s) { return A.HexCol(s); }
};
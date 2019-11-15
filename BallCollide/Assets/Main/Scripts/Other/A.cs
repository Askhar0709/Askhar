using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

[System.Serializable]
public class VarTime<T>
{
    public T value;
    public float time = 0;
    public VarTime(T value, float time) { Set(value, time); }
    public void Set(T value) { Set(value, Time.time); }
    public void Set(T value, float time) { this.value = value; this.time = time; }
    ///<summary>name хувьсагчийн нэр, V нь энэ хувьсагчийг агуулж байгаа класс харин obj обект, value өгөх утга, time хүлээх хугацаа</summary>
    public void Set<V>(V obj, string name, T value, float time) { A.GC.VarTimeSet(obj, name, value, time); }
}

[System.Serializable]
public class Tf
{
    public Vector3 t;
    public Vector3 r;
    public Vector3 s;
    public Quaternion q { get { return Quaternion.Euler(r); } }
    public Tf(Vector3 t, Quaternion r, Vector3 s) { Set(t, r.eulerAngles, s); }
    public Tf(Vector3 t, Vector3 r, Vector3 s) { Set(t, r, s); }
    public Tf(Vector3 t, Quaternion r) { Set(t, r.eulerAngles, Vector3.one); }
    public Tf(Vector3 t, Vector3 r) { Set(t, r, Vector3.one); }
    public Tf(Vector3 t) { Set(t, Vector3.zero, Vector3.one); }
    public Tf() { Set(Vector3.one, Vector3.zero, Vector3.one); }
    public void Set(Vector3 t, Vector3 r, Vector3 s) { this.t = t; this.r = r; this.s = s; }
}

[System.Serializable]
public class LevelTile
{
    public Vector2Int pos;
    public bool[] actives = new bool[5];
    public LevelTile(Vector2Int pos, bool isBody, bool isLF, bool isRF, bool isRB, bool isLB)
    {
        this.pos = pos;
        actives[0] = isBody;
        actives[1] = isLF;
        actives[2] = isRF;
        actives[3] = isRB;
        actives[4] = isLB;
    }
    public LevelTile(Vector2Int pos, bool[] actives)
    {
        this.pos = pos;
        this.actives = actives;
    }
}

public class RoadData
{
    public RoadDir dir;
    public Vector3Int pos;
    public Quaternion rot;
    public RoadData(RoadDir dir, Vector3Int pos, Quaternion rot) { this.dir = dir; this.pos = pos; this.rot = rot; }
}
public enum RenderingMode { Opaque, Cutout, Fade, Transparent }
public enum RoadDir { LD, FD, RD, L, F, R, LU, FU, RU }
public enum Vib { Success, Warning, Error, Light, Medium, Heavy }
public enum RotType { Def, Rnd, Rot };

public static class A
{
    // █▄ ▄█ ▄▀▀▀▄  ▀█▀  █   █ 
    // █ █ █ █▄▄▄█   █   █▀▄ █ 
    // █   █ █   █   █   █  ▀█ 
    // ▀   ▀ ▀   ▀  ▀▀▀  ▀   ▀

    ///<summary>Эхлэж байгааг шалгана</summary>
    public static bool IsStarting { get { return GameController.State == GameState.Starting; } }
    ///<summary>Тоглож байгааг шалгана</summary>
    public static bool IsPlaying { get { return GameController.State == GameState.Playing; } }
    ///<summary>Зогсоосон байгааг шалгана</summary>
    public static bool IsPause { get { return GameController.State == GameState.Pause; } }
    ///<summary>Хожсон эсэхийг шалгана</summary>
    public static bool IsLevelCompleted { get { return GameController.State == GameState.LevelCompleted; } }
    ///<summary>Хожигдсон эсэхийг шалгана</summary>
    public static bool IsGameOver { get { return GameController.State == GameState.GameOver; } }
    ///<summary>Тохиргоо хийж байгааг шалгана</summary>
    public static bool IsSettings { get { return GameController.State == GameState.Settings; } }
    ///<summary>Шилжиж байгааг шалгана</summary>
    public static bool IsShifting { get { return GameController.State == GameState.Shifting; } }

    ///<summary>GameController</summary>
    public static GameController GC { get { return GameController.Instance; } }
    ///<summary>CanvasController</summary>
    public static CanvasController CC { get { return CanvasController.Instance; } }
    ///<summary>CameraController</summary>
    public static CameraController CM { get { return CameraController.Instance; } }
    ///<summary>GameSettings</summary>
    public static GameSettings GS { get { return GameSettings.Instance; } }
    ///<summary>LevelSpawner</summary>
    public static LevelSpawner LS { get { return LevelSpawner.Instance; } }
    ///<summary>BotSpawner</summary>
    public static BotSpawner BS { get { return BotSpawner.Instance; } }
    ///<summary>EnvSpawner</summary>
    public static EnvSpawner ES { get { return EnvSpawner.Instance; } }

    ///<summary>Player</summary>
    public static Player Player { get { if (player == null) { player = GameObject.FindObjectOfType<Player>(); } return player; } }
    static Player player;
    ///<summary>Camera.main</summary>
    public static Camera Cam { get { return Camera.main; } }
    ///<summary>FollowType</summary>
    public static FollowType FollowType { get { return GC.followType; } }
    ///<summary>Characters</summary>
    public static List<Character> Characters { get { return BS.characters; } }
    ///<summary>Дүрийн 200-н нэр</summary>
    public static readonly List<string> Names = new List<string>() { "Guy", "Hill", "Brittany", "Townsend", "Santos", "Roberson", "Jeffrey", "Jackson", "Lindsey", "Wheeler", "Madeline", "Cobb", "Paula", "Gibbs", "Angel", "Underwood", "Judy", "Adkins", "Johnathan", "Quinn", "Isabel", "Ray", "Kerry", "Schneider", "Milton", "Martin", "Robyn", "Farmer", "Clifford", "Dennis", "Leslie", "Marshall", "Marc", "Wise", "Alfonso", "Holmes", "Tyrone", "Gilbert", "Catherine", "Simon", "Nina", "Bowers", "Darryl", "Potter", "Jennie", "Sherman", "Danny", "Caldwell", "David", "James", "Adam", "Oliver", "Doyle", "Stephens", "Cody", "Mcguire", "Gerardo", "Hanson", "Jo", "Sanders", "Owen", "Andrews", "Archie", "Garcia", "Eric", "Turner", "Ella", "Cook", "Rudolph", "Franklin", "Perry", "Lindsey", "Dianne", "Medina", "Jane", "Rhodes", "Woodrow", "Gonzales", "Lila", "Ballard", "Christian", "Walsh", "Jon", "Hall", "Kate", "Love", "Elizabeth", "Griffin", "Elsie", "Keller", "Rosalie", "Fitzgerald", "Deanna", "Harris", "Cristina", "Gregory", "Jean", "Griffith", "Lori", "Olson", "Laura", "Bass", "Rex", "Lee", "Tara", "Morrison", "Dan", "Haynes", "Frances", "Shelton", "Lucas", "Walters", "Juana", "Singleton", "Wallace", "Shaw", "Craig", "Rivera", "Juanita", "Norris", "Tommy", "Gomez", "Roman", "Castro", "Carroll", "Todd", "Jaime", "Poole", "Scott", "Craig", "Laurie", "Frank", "Emily", "Hampton", "Guillermo", "Lynch", "Andre", "Flowers", "Betty", "Gutierrez", "Josh", "Willis", "Karen", "Williamson", "Peggy", "Peters", "Timmy", "Gross", "Joan", "Gill", "Cora", "Gonzalez", "Jacquelyn", "Curtis", "Traci", "Bates", "Homer", "Larson", "Tabitha", "Butler", "Molly", "Holt", "Arthur", "Meyer", "Jessie", "Torres", "Dora", "Foster", "Marty", "Stone", "Teri", "Berry", "Spencer", "Norton", "Hazel", "Reid", "Stuart", "Goodwin", "Oscar", "Freeman", "Ross", "Weber", "Michele", "Diaz", "Bernadette", "Chapman", "Erick", "Fleming", "Shannon", "Burke", "Vicky", "Maldonado", "Nancy", "Lewis", "Nathaniel", "Mcgee", "Alex", "Ferguson", "Johanna", "Estrada" };

    ///<summary>Хожигдох</summary>
    public static void GameOver(bool isComplete = true) { GC.GameOver(isComplete); }
    ///<summary>Хожигдох</summary>
    public static void GameOver(bool isComplete, List<LeaderBoardData> datas) { GC.GameOver(isComplete, datas); }
    ///<summary>Хожих</summary>
    public static void LevelCompleted() { GC.LevelCompleted(); }
    ///<summary>Хожих</summary>
    public static void LevelCompleted(List<LeaderBoardData> datas) { GC.LevelCompleted(datas); }



    // █▀▀▀▄ █▀▀▀▀ ▄▀▀▀▄ █▀▀▀▀ █   █ 
    // █▄▄▄▀ █▄▄▄  █     █▄▄▄   ▀▄▀  
    // █ ▀▄  █     █  ▀█ █     ▄▀ ▀▄ 
    // ▀   ▀ ▀▀▀▀▀  ▀▀▀  ▀▀▀▀▀ ▀   ▀

    ///<summary>бүхэл тооны regex</summary>
    public static readonly string RegexInt = "[-+]?\\d+";
    ///<summary>бутархай тооны regex</summary>
    public static readonly string RegexFloat = "[-+]?\\d*\\.?\\d+";
    ///<summary>e-тэй бутархай тооны regex</summary>
    public static readonly string RegexEInFloat = RegexFloat + "+([eE][-+]?\\d+)?";
    ///<summary>16-н өнгөний regex</summary>
    public static readonly string RegexHexColor = "#?(([0-9A-Fa-f]{6})|([0-9A-Fa-f]{8}))";



    // ▄▀▀▀▄ █   █ ▄▀▀▀▄ █     █▀▀▀▀ 
    // █▄▄▄█ █▀▄ █ █     █     █▄▄▄  
    // █   █ █  ▀█ █  ▀█ █     █     
    // ▀   ▀ ▀   ▀  ▀▀▀  ▀▀▀▀▀ ▀▀▀▀▀

    ///<summary>b-г cw үнэн бол [a, a+180] үгүй бол [a, a-180] хооронд байна уу шалгана</summary>
    public static bool AngIsNear(float a, float b, bool cw = true) { return Dis(a, AngRep(b, a, cw)) < 180; }
    ///<summary>a, b-н хооронд f өнцөг байна уу шалгана</summary>
    public static bool AngIsBet(float f, float a, float b) { return AngDis(f, a) < AngDis(a, b) && AngDis(f, b) < AngDis(a, b); }
    ///<summary>cw үнэн бол цагийн зүүний дагуу үгүй бол эсрэг a, b-н хооронд f өнцөг байна уу шалгана</summary>
    public static bool AngIsBet(float f, float a, float b, bool cw) { return AngIsNear(a, b, cw) == IsBet(AngRep(f, a, cw), a, AngRep(b, a, cw)); }
    ///<summary>f∈[a, b] өнцөг</summary>
    public static float AngClamp(float f, float a, float b) { return AngIsNear(a, b) ? Clamp(AngRep(f, a), a, AngRep(b, a)) : Clamp(AngRep(f, b), b, AngRep(a, b)); }
    ///<summary>f-г cw үнэн бол [a, a+360] үгүй бол [a, a-360] хооронд давтана</summary>
    public static float AngRep(float f, float a = 0, bool cw = true) { return Rep(f, a, a + Sign(cw) * 360); }
    ///<summary>a, b-н зөрүү өнцөг</summary>
    public static float AngDis(float a, float b) { return Mathf.DeltaAngle(a, b); }
    ///<summary>a өнцгийг b өнцөгрүү t хувиар ойртуулна</summary>
    public static float AngLerp(float a, float b, float t) { return Mathf.LerpAngle(a, b, t); }
    ///<summary>a өнцгийг b өнцөгрүү t хувиар ойртуулна [cw үнэн бол цагийн зүүний дагуу]</summary>
    public static float AngLerp(float a, float b, float t, bool cw) { return Lerp(a, AngRep(b, a, cw), t); }
    ///<summary>a өнцгийг b өнцөгрүү d-р ойртуулна</summary>
    public static float AngMove(float a, float b, float d) { return Mathf.MoveTowardsAngle(a, b, d); }
    ///<summary>a өнцгийг b өнцөгрүү d-р ойртуулна [cw үнэн бол цагийн зүүний дагуу]</summary>
    public static float AngMove(float a, float b, float d, bool cw) { return Move(a, AngRep(b, a, cw), d); }
    ///<summary>дээрээс хархад a-с b-рүү харах өнцөг zLx [isUnity үнэн бол unity үгүй бол math дээр ашиглах өнцөг]</summary>
    public static float AngLookDown(Vector3 a, Vector3 b, bool isUnity = true) { return AngUnityOrReal(Atan2(b.z - a.z, b.x - a.x), isUnity); }
    ///<summary>урдаас хархад a-с b-рүү харах өнцөг yLx [isUnity үнэн бол unity үгүй бол math дээр ашиглах өнцөг]</summary>
    public static float AngLookForward(Vector3 a, Vector3 b, bool isUnity = true) { return AngUnityOrReal(Atan2(b.y - a.y, b.x - a.x), isUnity); }
    ///<summary>Atan2-р бодоод гарч ирсэн өнцгийг хөрвүүлнэ</summary>
    public static float AngUnityOrReal(float ang, bool isUnity) { return AngRep(isUnity ? (540 - AngRep(ang + 90)) : ang); }
    ///<summary>Өнцгийг Vector3 болгоно</summary>
    public static Vector3 AngVec(float r, float ang) { return new Vector3(Cos(ang), 0, Sin(ang)) * r; }




    // ▄▀▀▀▄ ▄▀▀▀▄ █     ▄▀▀▀▄ █▀▀▀▄ 
    // █     █   █ █     █   █ █▄▄▄▀ 
    // █   ▄ █   █ █     █   █ █ ▀▄  
    //  ▀▀▀   ▀▀▀  ▀▀▀▀▀  ▀▀▀  ▀   ▀

    ///<summary>өнгөний улаан</summary>
    public static Color ColR(Color c, float r) { return ColRGBA(c, r, -1, -1); }
    ///<summary>өнгөний ногоон</summary>
    public static Color ColG(Color c, float g) { return ColRGBA(c, -1, g, -1); }
    ///<summary>өнгөний хөх</summary>
    public static Color ColB(Color c, float b) { return ColRGBA(c, -1, -1, b); }
    ///<summary>өнгөний нэвтрэлт</summary>
    public static Color ColA(Color c, float a) { return ColRGBA(c, -1, -1, -1, a); }

    ///<summary>өнгөний hue авна</summary>
    public static float ColH(Color c) { return ColHSV(c, 0); }
    ///<summary>өнгөний saturation авна</summary>
    public static float ColS(Color c) { return ColHSV(c, 1); }
    ///<summary>өнгөний value авна</summary>
    public static float ColV(Color c) { return ColHSV(c, 2); }

    ///<summary>өнгөний hue</summary>
    public static Color ColH(Color c, float h) { return ColHSVA(c, h, -1, -1); }
    ///<summary>өнгөний saturation</summary>
    public static Color ColS(Color c, float s) { return ColHSVA(c, -1, s, -1); }
    ///<summary>өнгөний value</summary>
    public static Color ColV(Color c, float v) { return ColHSVA(c, -1, -1, v); }

    ///<summary>өнгөний hue-д dh-г нэмнэ [isRep үнэн бол давтана]</summary>
    public static Color ColDh(Color c, float dh, bool isRep = false) { return ColHSVA(c, -1, -1, -1, -1, dh, 0, 0, isRep); }
    ///<summary>өнгөний saturation-д ds-г нэмнэ [isRep үнэн бол давтана]</summary>
    public static Color ColDs(Color c, float ds, bool isRep = false) { return ColHSVA(c, -1, -1, -1, -1, 0, ds, 0, isRep); }
    ///<summary>өнгөний value-д dv-г нэмнэ [isRep үнэн бол давтана]</summary>
    public static Color ColDv(Color c, float dv, bool isRep = false) { return ColHSVA(c, -1, -1, -1, -1, 0, 0, dv, isRep); }

    ///<summary>Color-г Vector4 болгоно</summary>
    public static Vector4 ColVec4(Color c) { return new Vector4(c.r, c.g, c.b, c.a); }
    ///<summary>Vector4-г Color болгоно</summary>
    public static Color Vec4Col(Vector4 v) { return new Color(v.x, v.y, v.z, v.w); }

    ///<summary>өнгөний hsv-г авна [hsvIdx 0-h 1-s 2-v]</summary>
    public static float ColHSV(Color c, int hsvIdx)
    {
        float[] hsv = new float[3];
        Color.RGBToHSV(c, out hsv[0], out hsv[1], out hsv[2]);
        return hsv[hsvIdx];
    }
    ///<summary>өнгөний hsva-г өөрчилнө [hsva нь 0-с бага бол өөрчлөхгүй, dhsv-г нэмнэ, isRep үнэн бол давтана]</summary>
    public static Color ColHSVA(Color c, float h, float s, float v, float a = -1, float dh = 0, float ds = 0, float dv = 0, bool isRep = false)
    {
        float hh, ss, vv;
        Color.RGBToHSV(c, out hh, out ss, out vv);
        Vector4 r = new Vector4((h < 0 ? hh : h) + dh, (s < 0 ? ss : s) + ds, (v < 0 ? vv : v) + dv, a < 0 ? c.a : a);
        r = new Vector4(isRep && !Apx(dh, 0) ? Rep(r.x, 1) : Clamp01(r.x), isRep && !Apx(ds, 0) ? Rep(r.y, 1) : Clamp01(r.y), isRep && !Apx(dv, 0) ? Rep(r.z, 1) : Clamp01(r.z), r.w);
        return ColA(Color.HSVToRGB(r.x, r.y, r.z), r.w);
    }
    ///<summary>өнгөний rgba-г өөрчилнө [rgba нь 0-с бага бол өөрчлөхгүй]</summary>
    public static Color ColRGBA(Color c, float r, float g, float b, float a = -1)
    {
        return new Color(r < 0 ? c.r : r, g < 0 ? c.g : g, b < 0 ? c.b : b, a < 0 ? c.a : a);
    }

    ///<summary>string-г Color болгоно</summary>
    public static Color StrCol(string s)
    {
        string[] arr = (s.StartsWith("RGBA(") && s.EndsWith(")") ? s.Substring(5, s.Length - 6) : s).Split(',');
        return new Color(
            arr.Length > 0 ? float.Parse(arr[0]) : 0,
            arr.Length > 1 ? float.Parse(arr[1]) : 0,
            arr.Length > 2 ? float.Parse(arr[2]) : 0,
            arr.Length > 3 ? float.Parse(arr[3]) : 0
        );
    }

    ///<summary>Color-г hex string болгоно</summary>
    public static string ColHex(Color c)
    {
        return "#" +
            Mathf.RoundToInt(c.r * 255).ToString("X2") +
            Mathf.RoundToInt(c.g * 255).ToString("X2") +
            Mathf.RoundToInt(c.b * 255).ToString("X2") +
            Mathf.RoundToInt(c.a * 255).ToString("X2");
    }
    ///<summary>hex string-г Color болгоно</summary>
    public static Color HexCol(string s)
    {
        if (System.Text.RegularExpressions.Regex.IsMatch(s.ToUpper(), RegexHexColor))
        {
            Color res;
            if (ColorUtility.TryParseHtmlString(s[0] != '#' ? "#" + s : s, out res))
                return res;
        }
        return Color.clear;
    }

    ///<summary>c-н утга default утга байвал хар болгоно</summary>
    public static void ColDefBlack(ref Color c) { ColDef(ref c, Color.black); }
    ///<summary>c-н утга default утга байвал defCol болгоно</summary>
    public static void ColDef(ref Color c, Color defCol) { if (c == default(Color)) c = defCol; }



    // █   █ █▀▀▀▀ ▄▀▀▀▄ ▀▀█▀▀ ▄▀▀▀▄ █▀▀▀▄ 
    // █   █ █▄▄▄  █       █   █   █ █▄▄▄▀ 
    //  █ █  █     █   ▄   █   █   █ █ ▀▄  
    //   ▀   ▀▀▀▀▀  ▀▀▀    ▀    ▀▀▀  ▀   ▀

    ///<summary>v∈[a, b] шалгана</summary>
    public static bool VecIsBet(Vector3 v, Vector3 a, Vector3 b) { return IsBet(v.x, a.x, b.x) && IsBet(v.y, a.y, b.y) && IsBet(v.z, a.z, b.z); }
    ///<summary>v∈[a, b]</summary>
    public static Vector3 VecClamp(Vector3 v, Vector3 a, Vector3 b) { return new Vector3(Clamp(v.x, a.x, b.x), Clamp(v.y, a.y, b.y), Clamp(v.z, a.z, b.z)); }
    ///<summary>a, b-н хоорондох өнцөг</summary>
    public static float VecAng(Vector3 a, Vector3 b) { return Vector3.Angle(a, b); }
    ///<summary>v-н уртыг d-р хязгаарлана</summary>
    public static Vector3 VecClampMag(Vector3 v, float d) { return Vector3.ClampMagnitude(v, d); }
    ///<summary>axb</summary>
    public static Vector3 VecCross(Vector3 a, Vector3 b) { return Vector3.Cross(a, b); }
    ///<summary>a, b-н хоорондох зай</summary>
    public static float VecDis(Vector3 a, Vector3 b) { return Vector3.Distance(a, b); }
    ///<summary>a·b = ∑ab = a.xb.x+a.yb.y+a.zb.z</summary>
    public static float VecDot(Vector3 lhs, Vector3 rhs) { return Vector3.Dot(lhs, rhs); }
    ///<summary>a-г b-рүү t хувиар ойртуулна [a, b]</summary>
    public static Vector3 VecLerp(Vector3 a, Vector3 b, float t) { return Vector3.Lerp(a, b, t); }
    ///<summary>a-г b-рүү t хувиар ойртуулна</summary>
    public static Vector3 VecUnLerp(Vector3 a, Vector3 b, float t) { return Vector3.LerpUnclamped(a, b, t); }
    ///<summary>зэргийн lerp</summary>
    public static Vector3 VecDegLerp(float t, params Vector3[] arr)
    {
        List<float> x = new List<float>(), y = new List<float>(), z = new List<float>();
        arr.ToList().ForEach(e => { x.Add(e.x); y.Add(e.y); z.Add(e.z); });
        return new Vector3(DegLerp(t, x.ToArray()), DegLerp(t, y.ToArray()), DegLerp(t, z.ToArray()));
    }
    ///<summary>v-н урт
    public static float VecMag(Vector3 v) { return Vector3.Magnitude(v); }
    ///<summary>a, b-н ХИ утгуудаар үүсгэнэ</summary>
    public static Vector3 VecMax(Vector3 a, Vector3 b) { return Vector3.Max(a, b); }
    ///<summary>a, b-н ХБ утгуудаар үүсгэнэ</summary>
    public static Vector3 VecMin(Vector3 a, Vector3 b) { return Vector3.Min(a, b); }
    ///<summary>lis-н ХИ ХБ утгуудаар үүсгэнэ</summary>
    public static void VecLisMinMax(List<Vector3> lis, ref Vector3 min, ref Vector3 max)
    {
        if (lis.Count > 0)
        {
            min = Vector3.positiveInfinity;
            max = Vector3.negativeInfinity;
            foreach (Vector3 v in lis)
            {
                min = VecMin(min, v);
                max = VecMin(max, v);
            }
        }
    }
    ///<summary>a-г b-рүү d-р ойртуулна</summary>
    public static Vector3 VecMove(Vector3 a, Vector3 b, float d) { return Vector3.MoveTowards(a, b, d); }
    ///<summary>v-н уртыг 1 болгоно
    public static Vector3 VecNor(Vector3 v) { return Vector3.Normalize(v); }
    public static void VecOrthoNor(ref Vector3 nor, ref Vector3 tangent) { Vector3.OrthoNormalize(ref nor, ref tangent); }
    public static void VecOrthoNor(ref Vector3 nor, ref Vector3 tangent, ref Vector3 binor) { Vector3.OrthoNormalize(ref nor, ref tangent, ref binor); }
    public static Vector3 VecProject(Vector3 v, Vector3 onNor) { return Vector3.Project(v, onNor); }
    public static Vector3 VecProjectOnPlane(Vector3 v, Vector3 plnNor) { return Vector3.ProjectOnPlane(v, plnNor); }
    public static Vector3 VecReflect(Vector3 inDir, Vector3 inNor) { return Vector3.Reflect(inDir, inNor); }
    public static Vector3 VecRot(Vector3 a, Vector3 b, float maxRadD, float maxMagD) { return Vector3.RotateTowards(a, b, maxRadD, maxMagD); }
    public static float VecSignAng(Vector3 a, Vector3 b, Vector3 axis) { return Vector3.SignedAngle(a, b, axis); }
    public static Vector3 VecSlerp(Vector3 a, Vector3 b, float t) { return Vector3.Slerp(a, b, t); }
    public static Vector3 VecUnSlerp(Vector3 a, Vector3 b, float t) { return Vector3.SlerpUnclamped(a, b, t); }

    ///<summary>Vector-г Vector-д үржинэ</summary>
    public static Vector3 VecMul(params Vector3[] arr)
    {
        if (arr.Length == 0) return Vector3.zero;
        if (arr.Length == 1) return arr[0];
        if (arr.Length == 2) return Vector3.Scale(arr[0], arr[1]);
        Vector3 res = Vector3.one;
        foreach (Vector3 v in arr)
            res = Vector3.Scale(res, v);
        return res;
    }

    ///<summary>Vector2-г Vector2-д хуваана</summary>
    public static Vector2 Vec2Div(Vector2 a, Vector2 b) { return new Vector2(a.x / b.x, a.y / b.y); }
    ///<summary>Vector2Int-г Vector2Int-д хуваана</summary>
    public static Vector2Int Vec2IDiv(Vector2Int a, Vector2Int b) { return new Vector2Int(a.x / b.x, a.y / b.y); }
    ///<summary>Vector3-г Vector3-д хуваана</summary>
    public static Vector3 Vec3Div(Vector3 a, Vector3 b) { return new Vector3(a.x / b.x, a.y / b.y, a.z / b.z); }
    ///<summary>Vector3Int-г Vector3Int-д хуваана</summary>
    public static Vector3Int Vec3IDiv(Vector3Int a, Vector3Int b) { return new Vector3Int(a.x / b.x, a.y / b.y, a.z / b.z); }
    ///<summary>Vector4-г Vector4-д хуваана</summary>
    public static Vector4 Vec4Div(Vector4 a, Vector4 b) { return new Vector4(a.x / b.x, a.y / b.y, a.z / b.z, a.w / b.w); }

    ///<summary>Vector2-г string болгоно</summary>
    public static string Vec2Str(Vector2 v) { return "(" + v.x + ", " + v.y + ")"; }
    ///<summary>Vector2Int-г string болгоно</summary>
    public static string Vec2IStr(Vector2Int v) { return "(" + v.x + ", " + v.y + ")"; }
    ///<summary>Vector3-г string болгоно</summary>
    public static string Vec3Str(Vector3 v) { return "(" + v.x + ", " + v.y + ", " + v.z + ")"; }
    ///<summary>Vector3Int-г string болгоно</summary>
    public static string Vec3IStr(Vector3Int v) { return "(" + v.x + ", " + v.y + ", " + v.z + ")"; }
    ///<summary>Vector4-г string болгоно</summary>
    public static string Vec4Str(Vector4 v) { return "(" + v.x + ", " + v.y + ", " + v.z + ", " + v.w + ")"; }

    ///<summary>string-г Vector2 болгоно</summary>
    public static Vector2 StrVec2(string s) { return Vec4Vec2(StrVec4(s)); }
    ///<summary>string-г Vector2Int болгоно</summary>
    public static Vector2Int StrVec2I(string s) { return Vec4Vec2I(StrVec4(s)); }
    ///<summary>string-г Vector3 болгоно</summary>
    public static Vector3 StrVec3(string s) { return Vec4Vec3(StrVec4(s)); }
    ///<summary>string-г Vector3Int болгоно</summary>
    public static Vector3Int StrVec3I(string s) { return Vec4Vec3I(StrVec4(s)); }
    ///<summary>string-г Vector4 болгоно</summary>
    public static Vector4 StrVec4(string s)
    {
        string[] arr = (s.StartsWith("(") && s.EndsWith(")") ? s.Substring(1, s.Length - 2) : s).Split(',');
        Vector2 a = Vector2.zero * Vector2.zero;
        return new Vector4(
            arr.Length > 0 ? float.Parse(arr[0]) : 0,
            arr.Length > 1 ? float.Parse(arr[1]) : 0,
            arr.Length > 2 ? float.Parse(arr[2]) : 0,
            arr.Length > 3 ? float.Parse(arr[3]) : 0
        );
    }

    ///<summary>Vector2-г Vector2Int болгоно</summary>
    public static Vector2Int Vec2Vec2I(Vector2 v) { return new Vector2Int((int)v.x, (int)v.y); }
    ///<summary>Vector2-г Vector3 болгоно</summary>
    public static Vector3 Vec2Vec3(Vector2 v) { return new Vector3(v.x, v.y, 0); }
    ///<summary>Vector2-г Vector3Int болгоно</summary>
    public static Vector3Int Vec2Vec3I(Vector2 v) { return new Vector3Int((int)v.x, (int)v.y, 0); }
    ///<summary>Vector2-г Vector4 болгоно</summary>
    public static Vector4 Vec2Vec4(Vector2 v) { return new Vector4(v.x, v.y, 0, 0); }

    ///<summary>Vector2Int-г Vector2 болгоно</summary>
    public static Vector2 Vec2IVec2(Vector2Int v) { return new Vector2(v.x, v.y); }
    ///<summary>Vector2Int-г Vector3 болгоно</summary>
    public static Vector3 Vec2IVec3(Vector2Int v) { return new Vector3(v.x, v.y, 0); }
    ///<summary>Vector2Int-г Vector3Int болгоно</summary>
    public static Vector3Int Vec2IVec3I(Vector2Int v) { return new Vector3Int(v.x, v.y, 0); }
    ///<summary>Vector2Int-г Vector4 болгоно</summary>
    public static Vector4 Vec2IVec4(Vector2Int v) { return new Vector4(v.x, v.y, 0, 0); }

    ///<summary>Vector3-г Vector2 болгоно</summary>
    public static Vector2 Vec3Vec2(Vector3 v) { return new Vector2(v.x, v.y); }
    ///<summary>Vector3-г Vector2Int болгоно</summary>
    public static Vector2Int Vec3Vec2I(Vector3 v) { return new Vector2Int((int)v.x, (int)v.y); }
    ///<summary>Vector3-г Vector3Int болгоно</summary>
    public static Vector3Int Vec3Vec3I(Vector3 v) { return new Vector3Int((int)v.x, (int)v.y, (int)v.z); }
    ///<summary>Vector3-г Vector4 болгоно</summary>
    public static Vector4 Vec3Vec4(Vector3 v) { return new Vector4(v.x, v.y, v.z, 0); }

    ///<summary>Vector3Int-г Vector2 болгоно</summary>
    public static Vector2 Vec3IVec2(Vector3Int v) { return new Vector2(v.x, v.y); }
    ///<summary>Vector3Int-г Vector2Int болгоно</summary>
    public static Vector2Int Vec3IVec2I(Vector3Int v) { return new Vector2Int(v.x, v.y); }
    ///<summary>Vector3Int-г Vector3 болгоно</summary>
    public static Vector3 Vec3IVec3(Vector3Int v) { return new Vector3(v.x, v.y, v.z); }
    ///<summary>Vector3Int-г Vector4 болгоно</summary>
    public static Vector4 Vec3IVec4(Vector3Int v) { return new Vector4(v.x, v.y, v.z, 0); }

    ///<summary>Vector4-г Vector2 болгоно</summary>
    public static Vector2 Vec4Vec2(Vector4 v) { return new Vector2(v.x, v.y); }
    ///<summary>Vector4-г Vector2Int болгоно</summary>
    public static Vector2Int Vec4Vec2I(Vector4 v) { return new Vector2Int((int)v.x, (int)v.y); }
    ///<summary>Vector4-г Vector3 болгоно</summary>
    public static Vector3 Vec4Vec3(Vector4 v) { return new Vector3(v.x, v.y, v.z); }
    ///<summary>Vector4-г Vector3Int болгоно</summary>
    public static Vector3Int Vec4Vec3I(Vector4 v) { return new Vector3Int((int)v.x, (int)v.y, (int)v.z); }

    ///<summary>Vector2-н v.x-н утгыг x-р солино</summary>
    public static Vector2 Vec2X(Vector2 v, float x) { return new Vector2(x, v.y); }
    ///<summary>Vector2-н v.y-н утгыг y-р солино</summary>
    public static Vector2 Vec2Y(Vector2 v, float y) { return new Vector2(v.x, y); }

    ///<summary>Vector2Int-н v.x-н утгыг x-р солино</summary>
    public static Vector2Int Vec2IX(Vector2Int v, int x) { return new Vector2Int(x, v.y); }
    ///<summary>Vector2Int-н v.y-н утгыг y-р солино</summary>
    public static Vector2Int Vec2IY(Vector2Int v, int y) { return new Vector2Int(v.x, y); }

    ///<summary>Vector3-н v.x-н утгыг x-р солино</summary>
    public static Vector3 Vec3X(Vector3 v, float x) { return new Vector3(x, v.y, v.z); }
    ///<summary>Vector3-н v.y-н утгыг y-р солино</summary>
    public static Vector3 Vec3Y(Vector3 v, float y) { return new Vector3(v.x, y, v.z); }
    ///<summary>Vector3-н v.z-н утгыг z-р солино</summary>
    public static Vector3 Vec3Z(Vector3 v, float z) { return new Vector3(v.x, v.y, z); }

    ///<summary>Vector3Int-н v.x-н утгыг x-р солино</summary>
    public static Vector3Int Vec3IX(Vector3Int v, int x) { return new Vector3Int(x, v.y, v.z); }
    ///<summary>Vector3Int-н v.y-н утгыг y-р солино</summary>
    public static Vector3Int Vec3IY(Vector3Int v, int y) { return new Vector3Int(v.x, y, v.z); }
    ///<summary>Vector3Int-н v.z-н утгыг z-р солино</summary>
    public static Vector3Int Vec3IZ(Vector3Int v, int z) { return new Vector3Int(v.x, v.y, z); }

    ///<summary>Vector4-н v.x-н утгыг x-р солино</summary>
    public static Vector3 Vec4X(Vector4 v, float x) { return new Vector4(x, v.y, v.z, v.w); }
    ///<summary>Vector4-н v.y-н утгыг y-р солино</summary>
    public static Vector3 Vec4Y(Vector4 v, float y) { return new Vector4(v.x, y, v.z, v.w); }
    ///<summary>Vector4-н v.z-н утгыг z-р солино</summary>
    public static Vector3 Vec4Z(Vector4 v, float z) { return new Vector4(v.x, v.y, z, v.w); }
    ///<summary>Vector4-н v.w-н утгыг w-р солино</summary>
    public static Vector3 Vec4W(Vector4 v, float w) { return new Vector4(v.x, v.y, v.z, w); }



    // █▀▀▀▄ ▄▀▀▀▄ ▀▀█▀▀ ▄▀▀▀▄ 
    // █   █ █▄▄▄█   █   █▄▄▄█ 
    // █   █ █   █   █   █   █ 
    // ▀▀▀▀  ▀   ▀   ▀   ▀   ▀

    ///<summary>PlayerPrefs-д хадгалсан data-г авна</summary>
    public static T GetData<T>(Data key)
    {
        object res = null;
        if (GameController.Datas.ContainsKey(key))
        {
            System.Type type = GameController.Datas[key].GetType();
            if (type == typeof(bool))
                res = PlayerPrefs.GetInt("" + key, (bool)GameController.Datas[key] ? 1 : 0) == 1;
            else if (type == typeof(int))
                res = PlayerPrefs.GetInt("" + key, (int)GameController.Datas[key]);
            else if (type == typeof(float))
                res = PlayerPrefs.GetFloat("" + key, (float)GameController.Datas[key]);
            else if (type == typeof(string))
                res = PlayerPrefs.GetString("" + key, (string)GameController.Datas[key]);
            else if (type == typeof(Vector2))
                res = StrVec2(PlayerPrefs.GetString("" + key, Vec2Str((Vector2)GameController.Datas[key])));
            else if (type == typeof(Vector2Int))
                res = StrVec2I(PlayerPrefs.GetString("" + key, Vec2IStr((Vector2Int)GameController.Datas[key])));
            else if (type == typeof(Vector3))
                res = StrVec3(PlayerPrefs.GetString("" + key, Vec3Str((Vector3)GameController.Datas[key])));
            else if (type == typeof(Vector3Int))
                res = StrVec3I(PlayerPrefs.GetString("" + key, Vec3IStr((Vector3Int)GameController.Datas[key])));
            else if (type == typeof(Vector4))
                res = StrVec4(PlayerPrefs.GetString("" + key, Vec4Str((Vector4)GameController.Datas[key])));
            else if (type == typeof(Color))
                res = StrCol(PlayerPrefs.GetString("" + key, ((Color)GameController.Datas[key]).ToString()));
        }
        return (T)res;
    }
    ///<summary>PlayerPrefs-д data-г хадгална</summary>
    public static void SetData(Data key, object value)
    {
        if (GameController.Datas.ContainsKey(key))
        {
            System.Type type = GameController.Datas[key].GetType();
            if (type == typeof(bool))
                PlayerPrefs.SetInt("" + key, (bool)value ? 1 : 0);
            else if (type == typeof(int))
                PlayerPrefs.SetInt("" + key, (int)value);
            else if (type == typeof(float))
                PlayerPrefs.SetFloat("" + key, (float)value);
            else if (type == typeof(string))
                PlayerPrefs.SetString("" + key, (string)value);
            else if (type == typeof(Vector2))
                PlayerPrefs.SetString("" + key, Vec2Str((Vector2)value));
            else if (type == typeof(Vector2Int))
                PlayerPrefs.SetString("" + key, Vec2IStr((Vector2Int)value));
            else if (type == typeof(Vector3))
                PlayerPrefs.SetString("" + key, Vec3Str((Vector3)value));
            else if (type == typeof(Vector3Int))
                PlayerPrefs.SetString("" + key, Vec3IStr((Vector3Int)value));
            else if (type == typeof(Vector4))
                PlayerPrefs.SetString("" + key, Vec4Str((Vector4)value));
            else if (type == typeof(Color))
                PlayerPrefs.SetString("" + key, ((Color)value).ToString());
        }
    }



    // ▀▀█▀▀ █▀▀▀▄ ▄▀▀▀▄ █   █ ▄▀▀▀▀ █▀▀▀▀ ▄▀▀▀▄ █▀▀▀▄ █▄ ▄█ 
    //   █   █▄▄▄▀ █▄▄▄█ █▀▄ █ ▀▄▄▄  █▄▄▄  █   █ █▄▄▄▀ █ █ █ 
    //   █   █ ▀▄  █   █ █  ▀█     █ █     █   █ █ ▀▄  █   █ 
    //   ▀   ▀   ▀ ▀   ▀ ▀   ▀ ▀▀▀▀  ▀      ▀▀▀  ▀   ▀ ▀   ▀

    ///<summary>pos байрлалыг local space-с world space болгон байрлалыг буцаана</summary>
    public static Vector3 TfTransformPoint(Tf tf, Vector3 pos) { return Matrix4x4.TRS(tf.t, tf.q, tf.s).MultiplyPoint3x4(pos); }
    ///<summary>pos байралыг world space-с local space болгон байрлалыг буцаана</summary>
    public static Vector3 TfInverseTransformPoint(Tf tf, Vector3 pos) { return Matrix4x4.TRS(tf.t, tf.q, tf.s).inverse.MultiplyPoint3x4(pos); }
    ///<summary>dir чиглэлийг local space-с world space болгон чиглэлийг буцаана</summary>
    public static Vector3 TfTransformDirection(Quaternion rot, Vector3 dir) { return rot * dir; }
    ///<summary>dir чиглэлийг local space-с world space болгон чиглэлийг буцаана</summary>
    public static Vector3 TfTransformDirection(Vector3 rot, Vector3 dir) { return TfTransformDirection(Quaternion.Euler(rot), dir); }
    ///<summary>pos-г rot-р эргүүлхэд гарах байрлалыг буцаана</summary>
    public static Vector3 TfRotate(Quaternion rot, Vector3 pos) { return Matrix4x4.Rotate(rot).MultiplyPoint3x4(pos); }
    ///<summary>pos-г rot-р эргүүлхэд гарах байрлалыг буцаана</summary>
    public static Vector3 TfRotate(Vector3 rot, Vector3 pos) { return TfRotate(Quaternion.Euler(rot), pos); }
    ///<summary>tf-г pnt цэг дээр төвтэй axis тэнхлэгийн дагуу ang өнцгөөр эргүүлхэд гарах байрлалыг буцаана</summary>
    public static Vector3 TfRotateAround(Tf tf, Vector3 pnt, Vector3 axis, float ang) { return pnt + Quaternion.AngleAxis(ang, axis) * (tf.t - pnt); }
    ///<summary>pos-с tar-рүү харах эргэлтийг буцаана</summary>
    public static Quaternion TfLookAt(Vector3 pos, Vector3 tar) { return Quaternion.LookRotation(tar - pos); }
    ///<summary>Transform-г Tf болгоно</summary>
    public static Tf TransformTf(Transform tf, Space spc = Space.Self)
    {
        if (spc == Space.Self)
            return new Tf(tf.localPosition, tf.localRotation, tf.localScale);
        return new Tf(tf.position, tf.rotation, tf.lossyScale);
    }



    // █▀▀▀▄ ▄▀▀▀▄ █   █ █▀▀▀▄ ▄▀▀▀▄ █▄ ▄█ 
    // █▄▄▄▀ █▄▄▄█ █▀▄ █ █   █ █   █ █ █ █ 
    // █ ▀▄  █   █ █  ▀█ █   █ █   █ █   █ 
    // ▀   ▀ ▀   ▀ ▀   ▀ ▀▀▀▀   ▀▀▀  ▀   ▀

    ///<summary>random өнгө [alpha = 1]</summary>
    public static Color RndCol { get { return new Color(Rnd01, Rnd01, Rnd01, 1f); } }
    ///<summary>random өнгө</summary>
    public static Color RndColA { get { return new Color(Rnd01, Rnd01, Rnd01, Rnd01); } }
    ///<summary>random тоо [-1, 1]</summary>
    public static float Rnd { get { return RndRng(-1f, 1f); } }
    ///<summary>random тоо [0, 1]</summary>
    public static float Rnd01 { get { return Random.value; } }
    ///<summary>random өнцөг [0, 360]</summary>
    public static float RndAng { get { return Rnd01 * 360f; } }
    ///<summary>random тэмдэг ±1</summary>
    public static int RndSign { get { return RndBool ? -1 : 1; } }
    ///<summary>random -1 || 0 || 1</summary>
    public static int Rnd101 { get { return RndRng(-1, 2); } }
    ///<summary>random bool</summary>
    public static bool RndBool { get { return Rnd01 > 0.5f; } }

    ///<summary>random тоо [0, n]</summary>
    public static float RndN(float n) { return Rnd01 * n; }
    ///<summary>random магадлал шалгана p[0, 1]</summary>
    public static bool RndP(float p) { return p > Rnd01; }
    ///<summary>random тоо [a, b]</summary>
    public static float RndRng(float a, float b) { return Random.Range(a, b); }
    ///<summary>random тоо [a, b[</summary>
    public static int RndRng(int a, int b) { return Random.Range(a, b); }
    ///<summary>random index [0, n[</summary>
    public static int RndIdx(int n) { return Random.Range(0, n); }
    ///<summary>random index i-с ялгаатай[0, n[</summary>
    public static int RndIdx(int n, int i)
    {
        int res = 0;
        if (i < 0)
            i = n - 1;
        if (n > 1)
            do { res = A.RndIdx(n); } while (res == i);
        return res;
    }
    ///<summary>random тоо a || b</summary>
    public static float Rnd2(float a, float b) { return Rnd2(0.5f, a, b); }
    ///<summary>random тоо p[0, 1] магадлал биелвэл a үгүй бол b</summary>
    public static float Rnd2(float p, float a, float b) { return RndP(p) ? a : b; }
    ///<summary>random байрлал [a, b]</summary>
    public static Vector3 RndPos(Vector3 a, Vector3 b)
    {
        Vector3 min = VecMin(a, b), max = VecMax(a, b);
        return new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));
    }
    ///<summary>random enum</summary>
    public static T RndEnum<T>()
    {
        var values = System.Enum.GetValues(typeof(T));
        return (T)values.GetValue(RndIdx(values.Length));
    }
    ///<summary>random list 1 элемент</summary>
    public static T RndList<T>(List<T> lis) { return lis[RndIdx(lis.Count)]; }
    ///<summary>random list n элемент</summary>
    public static List<T> RndList<T>(List<T> lis, int n)
    {
        List<T> res = new List<T>();
        if (1 <= n && n <= lis.Count)
            while (res.Count < n)
            {
                T t = lis[RndIdx(lis.Count)];
                if (!res.Contains(t))
                    res.Add(t);
            }
        else if (n > lis.Count)
            while (res.Count < n)
                res.Add(lis[RndIdx(lis.Count)]);
        return res;
    }



    // █▄ ▄█ ▄▀▀▀▄ ▀▀█▀▀ █▀▀▀▀ █▀▀▀▄  ▀█▀  ▄▀▀▀▄ █     
    // █ █ █ █▄▄▄█   █   █▄▄▄  █▄▄▄▀   █   █▄▄▄█ █     
    // █   █ █   █   █   █     █ ▀▄    █   █   █ █     
    // ▀   ▀ ▀   ▀   ▀   ▀▀▀▀▀ ▀   ▀  ▀▀▀  ▀   ▀ ▀▀▀▀▀

    ///<summary>go-с name-р эхэлсэн нэртэй материалыг олно</summary>
    public static Material MatFindName(GameObject go, string name) { return go.GC<Renderer>().materials.ToList().Find(i => i.name.StartsWith(name)); }
    ///<summary>mat-н rendering mode-г rm-р солино</summary>
    public static void MatRenMod(Material mat, RenderingMode rm)
    {
        switch (rm)
        {
            case RenderingMode.Opaque:
                mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                mat.SetInt("_ZWrite", 1);
                mat.DisableKeyword("_ALPHATEST_ON");
                mat.DisableKeyword("_ALPHABLEND_ON");
                mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                mat.renderQueue = -1;
                break;
            case RenderingMode.Cutout:
                mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                mat.SetInt("_ZWrite", 1);
                mat.EnableKeyword("_ALPHATEST_ON");
                mat.DisableKeyword("_ALPHABLEND_ON");
                mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                mat.renderQueue = 2450;
                break;
            case RenderingMode.Fade:
                mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                mat.SetInt("_ZWrite", 0);
                mat.DisableKeyword("_ALPHATEST_ON");
                mat.EnableKeyword("_ALPHABLEND_ON");
                mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                mat.renderQueue = 3000;
                break;
            case RenderingMode.Transparent:
                mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                mat.SetInt("_ZWrite", 0);
                mat.DisableKeyword("_ALPHATEST_ON");
                mat.DisableKeyword("_ALPHABLEND_ON");
                mat.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                mat.renderQueue = 3000;
                break;
        }
    }



    // █▄ ▄█ ▄▀▀▀▄ ▀▀█▀▀ █   █ 
    // █ █ █ █▄▄▄█   █   █▄▄▄█ 
    // █   █ █   █   █   █   █ 
    // ▀   ▀ ▀   ▀   ▀   ▀   ▀

    ///<summary>PI тоо 3.1415926536</summary>
    public static readonly float PI = Mathf.PI;
    ///<summary>e тоо 2.7182818284</summary>
    public static readonly float e = Exp(1);
    ///<summary>epsilon маш бага бутархай тоо</summary>
    public static readonly float Epsilon = Mathf.Epsilon;
    ///<summary>√2 = 1.41421356237</summary>
    public static readonly float Sqrt2 = Sqrt(2);
    ///<summary>√3 = 1.73205080757</summary>
    public static readonly float Sqrt3 = Sqrt(3);
    ///<summary>√5 = 2.2360679775</summary>
    public static readonly float Sqrt5 = Sqrt(5);
    ///<summary>√6 = 2.44948974278</summary>
    public static readonly float Sqrt6 = Sqrt(6);
    ///<summary>√7 = 2.64575131106</summary>
    public static readonly float Sqrt7 = Sqrt(3);
    ///<summary>√8 = 2.82842712475</summary>
    public static readonly float Sqrt8 = Sqrt(8);
    ///<summary>алтан харьцаа 1.61803398875</summary>
    public static readonly float GoldenRatio = (1f + Sqrt5) / 2f;
    ///<summary>ХБ утга -3.40282347E+38</summary>
    public static readonly float MinVal = float.MinValue;
    ///<summary>ХИ утга 3.40282347E+38</summary>
    public static readonly float MaxVal = float.MaxValue;
    ///<summary>хязгааргүй</summary>
    public static readonly float PosInf = float.PositiveInfinity;
    ///<summary>-хязгааргүй</summary>
    public static readonly float NegInf = float.NegativeInfinity;
    ///<summary>тоо биш</summary>
    public static readonly float NaN = float.NaN;
    ///<summary>өнцгийг радианруу хөрвүүлэх тогтмол 0.0174532924</summary>
    public static readonly float DegRad = Mathf.Deg2Rad;
    ///<summary>радианыг өнцөгрүү хөрвүүлэх тогтмол 57.29578</summary>
    public static readonly float RadDeg = Mathf.Rad2Deg;

    ///<summary>0-тэй тэнцүүг d-р шалгана</summary>
    public static bool IsZero(float f, float d) { return Apx(f, 0f, d); }
    ///<summary>n-р фибоначийн тоо</summary>
    public static int Fibonacci(int n)
    {
        int res = n, n1 = 0, n2 = 1;
        if (n > 1)
            for (int i = 2; i <= n; i++)
            {
                res = n1 + n2;
                n2 = n1;
                n1 = res;
            }
        return res;
    }
    ///<summary>n-р анхны тоо</summary>
    public static int Prime(int n)
    {
        int res = 2;
        for (int i = 0; i <= n; res++)
            if (IsPrime(res)) i++;
        return res;
    }
    ///<summary>анхны тоо мөнүү шалгана</summary>
    public static bool IsPrime(int n)
    {
        for (int i = 2, m = n / 2; i <= m; i++)
            if (n % i == 0)
                return false;
        return n >= 2;
    }

    ///<summary>a, b-н ХИ ерөнхий хуваагч</summary>
    public static int GCD(int a, int b)
    {
        while (a != 0 && b != 0)
            if (a > b) a %= b;
            else b %= a;
        return a == 0 ? b : a;
    }
    ///<summary>a, b-н ХБ ерөнхий хуваагдагч</summary>
    public static int LCM(int a, int b) { return a * b / GCD(a, b); }

    ///<summary>n! n-н факториал эсвэл сэлгэмэл Pn = n!</summary>
    public static int Factorial(int n)
    {
        int res = 1;
        if (n > 1)
            for (int i = 2; i <= n; i++)
                res *= i;
        return res;
    }
    ///<summary>гүйлгэмэл A(n,k) = n!/(n-k)!</summary>
    public static float Permutation(int n, int k) { return (float)Factorial(n) / (float)Factorial(n - k); }
    ///<summary>хэсэглэл C(n,k) = n!/(k!(n-k)!)</summary>
    public static float Combination(int n, int k) { return (float)Factorial(n) / (float)(Factorial(k) * Factorial(n - k)); }

    ///<summary>квадрат тэгшитгэл ax²+bx+c = 0</summary>
    public static Vector2 QuadraticEquation(float a, float b, float c)
    {
        if (IsZero(a, 0.001f))
            return new Vector2(IsZero(b, 0.001f) ? NaN : -c / b, NaN);
        float d = b * b - 4f * a * c;
        if (d >= 0f)
            return new Vector2((-b + Mathf.Sqrt(d)) / (2f * a), (-b - Mathf.Sqrt(d)) / (2f * a));
        return new Vector2(NaN, NaN);
    }
    ///<summary>a, b-г ойролцоог d-р шалгана</summary>
    public static bool Apx(float a, float b, float d) { return Dis(a, b) <= d; }
    ///<summary>a, b-г ойролцоог шалгана</summary>
    public static bool Apx(float a, float b) { return Mathf.Approximately(a, b); }

    ///<summary>logᵦ(x) логарифм b суурьтай x</summary>
    public static float Log(float x, float b) { return Mathf.Log(x, b); }
    ///<summary>lb(x) = log₂(x) логарифм 2 суурьтай x</summary>
    public static float Lb(float x) { return Log(x, 2); }
    ///<summary>ln(x) = logₑ(x) логарифм e суурьтай x</summary>
    public static float Ln(float x) { return Mathf.Log(x); }
    ///<summary>lg(x) = log₁₀(x) логарифм 10 суурьтай x</summary>
    public static float Lg(float x) { return Mathf.Log10(x); }

    ///<summary>bⁿ b-н n зэрэг</summary>
    public static float Pow(float b, float n) { return Mathf.Pow(b, n); }
    ///<summary>10ⁿ 10-н n зэрэг</summary>
    public static float Pow10(float n) { return Pow(10, n); }
    ///<summary>2ⁿ 2-н n зэрэг</summary>
    public static float Pow2(float n) { return Pow(2, n); }
    ///<summary>-1ⁿ -1-н n зэрэг</summary>
    public static float PowNeg1(float n) { return Pow(-1, n); }
    ///<summary>v-г 2-н n зэрэг мөнүү шалгана</summary>
    public static bool IsPow2(float v) { return Mathf.IsPowerOfTwo((int)v); }
    ///<summary>v-тэй ойрхон бага 2-н n зэрэг</summary>
    public static int CloPow2(float v) { return Mathf.ClosestPowerOfTwo((int)v); }
    ///<summary>v-тэй ойрхон их 2-н n зэрэг</summary>
    public static int NxtPow2(float v) { return Mathf.NextPowerOfTwo((int)v); }
    ///<summary>eⁿ e-н n зэрэг</summary>
    public static float Exp(float n) { return Mathf.Exp(n); }
    ///<summary>√x язгуур доор x</summary>

    public static float Sqrt(float x) { return Mathf.Sqrt(x); }
    ///<summary>³√x 3 язгуур доор x</summary>
    public static float Cbrt(float x) { return NthRoot(x, 3); }
    ///<summary>ⁿ√x n язгуур доор x</summary>
    public static float NthRoot(float x, int n) { return n % 2 == 1 && x < 0 ? -Mathf.Pow(-x, 1f / n) : Mathf.Pow(x, 1f / n); }

    ///<summary>x-г тоймлоно</summary>
    public static float Round(float x) { return Mathf.Round(x); }
    ///<summary>x-г доошоо тоймлоно</summary>
    public static float Floor(float x) { return Mathf.Floor(x); }
    ///<summary>x-г дээшээ тоймлоно</summary>
    public static float Ceil(float x) { return Mathf.Ceil(x); }
    ///<summary>x-г 0-рүү тоймлоно</summary>
    public static float Truncate(float x) { return x >= 0 ? Floor(x) : Ceil(x); }
    ///<summary>x-г 0-с хол тоймлоно</summary>
    public static float Sgn(float x) { return x >= 0 ? Ceil(x) : Floor(x); }
    ///<summary>x-г тоймлоно</summary>
    public static int RoundInt(float f) { return Mathf.RoundToInt(f); }
    ///<summary>x-г доошоо тоймлоно</summary>
    public static int FloorInt(float f) { return Mathf.FloorToInt(f); }
    ///<summary>x-г дээшээ тоймлоно</summary>
    public static int CeilInt(float f) { return Mathf.CeilToInt(f); }
    ///<summary>x-г 0-рүү тоймлоно</summary>
    public static int TruncateInt(float x) { return x >= 0 ? FloorInt(x) : CeilInt(x); }
    ///<summary>x-г 0-с хол тоймлоно</summary>
    public static int SgnInt(float x) { return x >= 0 ? CeilInt(x) : FloorInt(x); }
    ///<summary>x-г b-сууриар c-р эхлүүлж тоймлоно</summary>
    public static float BaseRound(float x, float b, float c = 0) { return Round((x - c) / b) * b + c; }
    ///<summary>x-г b-сууриар c-р эхлүүлж доошоо тоймлоно</summary>
    public static float BaseFloor(float x, float b, float c = 0) { return Floor((x - c) / b) * b + c; }
    ///<summary>x-г b-сууриар c-р эхлүүлж дээшээ тоймлоно</summary>
    public static float BaseCeil(float x, float b, float c = 0) { return Ceil((x - c) / b) * b + c; }

    //                   B
    //                 /|
    //               /  |
    // hypotenuse c/    |a opposite
    //           /      |
    //         /________|
    //       A     b     C
    //          adjacent
    ///<summary>sine sinA = opposite/hypotenuse = a/c</summary>
    public static float Sin(float f) { return RadSin(f * DegRad); }
    ///<summary>cosine cosA = adjacent/hypotenuse = b/c</summary>
    public static float Cos(float f) { return RadCos(f * DegRad); }
    ///<summary>tangent tanA = opposite/adjacent = a/b = (a/c)/(b/c) = sinA/cosA</summary>
    public static float Tan(float f) { return RadTan(f * DegRad); }
    ///<summary>cosecant cscA = 1/sinA = hypotenuse/opposite = c/a</summary>
    public static float Csc(float f) { return RadCsc(f * DegRad); }
    ///<summary>secant secA = 1/cosA = hypotenuse/adjacent = c/b</summary>
    public static float Sec(float f) { return RadSec(f * DegRad); }
    ///<summary>cotangent cotA = 1/tanA = adjacent/opposite = cosA/sinA = b/a</summary>
    public static float Cot(float f) { return RadCot(f * DegRad); }
    ///<summary>arcsine y = arcsin(x), x = sin(y)</summary>
    public static float Asin(float f) { return RadAsin(f) * RadDeg; }
    ///<summary>arccosine y = arccos(x), x = cos(y)</summary>
    public static float Acos(float f) { return RadAcos(f) * RadDeg; }
    ///<summary>arctangent y = arctan(x), x = tan(y)</summary>
    public static float Atan(float f) { return RadAtan(f) * RadDeg; }
    ///<summary>arccosecant y = arccsc(x), x = csc(y)</summary>
    public static float Acsc(float f) { return RadAcsc(f) * RadDeg; }
    ///<summary>arcsecant y = arcsec(x), x = sec(y)</summary>
    public static float Asec(float f) { return RadAsec(f) * RadDeg; }
    ///<summary>arccotangent y = arccot(x), x = cot(y)</summary>
    public static float Acot(float f) { return RadAcot(f) * RadDeg; }
    ///<summary>tan-н өнцөг</summary>
    public static float Atan2(float y, float x) { return RadAtan2(y, x) * RadDeg; }

    ///<summary>sine sinA = opposite/hypotenuse = a/c [радиан]</summary>
    public static float RadSin(float f) { return Mathf.Sin(f); }
    ///<summary>cosine cosA = adjacent/hypotenuse = b/c [радиан]</summary>
    public static float RadCos(float f) { return Mathf.Cos(f); }
    ///<summary>tangent tanA = opposite/adjacent = a/b = (a/c)/(b/c) = sinA/cosA [радиан]</summary>
    public static float RadTan(float f) { return Mathf.Tan(f); }
    ///<summary>cosecant cscA = 1/sinA = hypotenuse/opposite = c/a [радиан]</summary>
    public static float RadCsc(float f) { return 1f / RadSin(f); }
    ///<summary>secant secA = 1/cosA = hypotenuse/adjacent = c/b [радиан]</summary>
    public static float RadSec(float f) { return 1f / RadCos(f); }
    ///<summary>cotangent cotA = 1/tanA = adjacent/opposite = cosA/sinA = b/a [радиан]</summary>
    public static float RadCot(float f) { return 1f / RadTan(f); }
    ///<summary>arcsine y = arcsin(x), x = sin(y) [радиан]</summary>
    public static float RadAsin(float f) { return Mathf.Asin(f); }
    ///<summary>arccosine y = arccos(x), x = cos(y) [радиан]</summary>
    public static float RadAcos(float f) { return Mathf.Acos(f); }
    ///<summary>arctangent y = arctan(x), x = tan(y) [радиан]</summary>
    public static float RadAtan(float f) { return Mathf.Atan(f); }
    ///<summary>arccosecant y = arccsc(x), x = csc(y) [радиан]</summary>
    public static float RadAcsc(float f) { return RadAsin(1f / f); }
    ///<summary>arcsecant y = arcsec(x), x = sec(y) [радиан]</summary>
    public static float RadAsec(float f) { return RadAcos(1f / f); }
    ///<summary>arccotangent y = arccot(x), x = cot(y) [радиан]</summary>
    public static float RadAcot(float f) { return RadAtan(1f / f); }
    ///<summary>tan-н өнцөг [радиан]</summary>
    public static float RadAtan2(float y, float x) { return Mathf.Atan2(y, x); }

    ///<summary>a, b-н хооронд f байна уу шалгана</summary>
    public static bool IsBet(float f, float a, float b) { return Min(a, b) <= f && f <= Max(a, b); }
    ///<summary>f-н эерэг утга</summary>
    public static float Abs(float f) { return Mathf.Abs(f); }
    ///<summary>2D Перлин шуугиан үүсгэнэ</summary>
    public static float PerlinNoise(float x, float y) { return Mathf.PerlinNoise(x, y); }
    ///<summary>a, b-н зөрүү</summary>
    public static float Dis(float a, float b) { return Abs(a - b); }
    ///<summary>a, b-н бага</summary>
    public static float Min(float a, float b) { return Mathf.Min(a, b); }
    ///<summary>a, b-н их</summary>
    public static float Max(float a, float b) { return Mathf.Max(a, b); }
    ///<summary>a, b-н f-тэй ойрхон</summary>
    public static float Near(float f, float a, float b) { return Dis(f, a) < Dis(f, b) ? a : b; }
    ///<summary>minA, maxA-н хоорондох f-г minB, maxB хооронд болгож өөрчилнө</summary>
    public static float Resize(float f, float minA, float maxA, float minB, float maxB) { return (f - minA) / (maxA - minA) * (maxB - minB) + minB; }
    ///<summary>isPos үнэн бол 1 худал бол -1</summary>
    public static int Sign(bool isPos) { return isPos ? 1 : -1; }
    ///<summary>f-н тэмдэг</summary>
    public static int Sign(float f) { return f >= 0 ? 1 : -1; }

    ///<summary>f∈[a, b]</summary>
    public static float Clamp(float f, float a, float b) { return Mathf.Clamp(f, Min(a, b), Max(a, b)); }
    ///<summary>f∈[-1, 1]</summary>
    public static float Clamp(float f) { return Clamp(f, -1f, 1f); }
    ///<summary>f∈[0, n]</summary>
    public static float Clamp0(float f, float n) { return Clamp(f, 0f, n); }
    ///<summary>f∈[0, 1]</summary>
    public static float Clamp01(float f) { return Mathf.Clamp01(f); }
    ///<summary>idx∈[0, n[</summary>
    public static float ClampIdx(int idx, int n) { return Mathf.Clamp(idx, 0, n - 1); }

    ///<summary>a-г b-рүү d-р ойртуулна</summary>
    public static float Move(float a, float b, float d) { return Mathf.MoveTowards(a, b, d); }
    ///<summary>a-г b-рүү t хувиар ойртуулна [a, b]</summary>
    public static float Lerp(float a, float b, float t) { return Mathf.Lerp(a, b, t); }
    ///<summary>a-г b-рүү t хувиар ойртуулна</summary>
    public static float UnLerp(float a, float b, float t) { return Mathf.LerpUnclamped(a, b, t); }
    ///<summary>a, b-н хоорондох f-н хувь</summary>
    public static float InvLerp(float a, float b, float f) { return Mathf.InverseLerp(a, b, f); }
    ///<summary>De Casteljau-н алгоритм [зэргийн lerp]</summary>
    public static float DegLerp(float t, params float[] arr)
    {
        if (arr == null || arr.Length == 0)
            return 0;
        List<float> lis = new List<float>(arr);
        while (lis.Count > 1)
        {
            List<float> tmp = new List<float>();
            for (int i = 1; i < lis.Count; i++)
                tmp.Add(UnLerp(lis[i - 1], lis[i], t));
            lis = tmp;
        }
        return lis[0];
    }
    ///<summary>Herminate _/‾</summary>
    public static float Hermite(float a, float b, float t) { return UnLerp(a, b, t * t * (3f - 2f * t)); }
    ///<summary>Sinerp /‾</summary>
    public static float Sinerp(float a, float b, float t) { return UnLerp(a, b, RadSin(t * PI * 0.5f)); }
    ///<summary>Coserp _/</summary>
    public static float Coserp(float a, float b, float t) { return UnLerp(a, b, 1.0f - RadCos(t * PI * 0.5f)); }
    ///<summary>Berp /~</summary>
    public static float Berp(float a, float b, float t) { return UnLerp(a, b, (RadSin((t = Clamp01(t)) * PI * (0.2f + 2.5f * t * t * t)) * Pow(1f - t, 2.2f) + t) * (1f + (1.2f * (1f - t)))); }
    ///<summary>Bounce /\\^ˆ</summary>
    public static float Bounce(float a, float b, float t) { return UnLerp(a, b, Abs(RadSin(PI * 2f * ((t = Clamp01(t)) + 1f) * (t + 1f)) * (1f - t))); }

    ///<summary>f-г a, b-н хооронд давтана</summary>
    public static float Rep(float f, float a, float b) { return Rep(f - a, b - a) + a; }
    ///<summary>f-г 0, n-н хооронд давтана</summary>
    public static float Rep(float f, float n) { return n >= 0 ? Mathf.Repeat(f, n) : -Mathf.Repeat(-f, -n); }
    ///<summary>idx-г давтана</summary>
    public static int RepIdx(int idx, int n) { return (int)Rep(idx, n); }
    ///<summary>f-г a, b-н хооронд ойлгож давтана</summary>
    public static float PingPong(float f, float a, float b) { return PingPong(f - a, b - a) + a; }
    ///<summary>f-г 0, n-н хооронд ойлгож давтана</summary>
    public static float PingPong(float f, float n) { return n >= 0 ? Mathf.PingPong(f, n) : -Mathf.PingPong(-f, -n); }



    // ▄▀▀▀▄ █▀▀▀▄ █▀▀▀▀ ▄▀▀▀▄ ▀▀█▀▀ █▀▀▀▀ 
    // █     █▄▄▄▀ █▄▄▄  █▄▄▄█   █   █▄▄▄  
    // █   ▄ █ ▀▄  █     █   █   █   █     
    //  ▀▀▀  ▀   ▀ ▀▀▀▀▀ ▀   ▀   ▀   ▀▀▀▀▀

    ///<summary>Effect үүсгэнэ</summary>
    public static void CrtEffect(Vector3 pos, Color col)
    {
        GameObject effectGo = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Main/Effect"), pos, Quaternion.identity, A.GC.transform);
        effectGo.RenMatCol(col);
        MonoBehaviour.Destroy(effectGo, 1);
    }
    ///<summary>PrimitiveType-тай обект үүсгэнэ</summary>
    public static GameObject CrtPri(PrimitiveType type, Tf tf, Color col = default(Color), string name = "", Transform parTf = null)
    {
        GameObject go = GameObject.CreatePrimitive(type);
        go.transform.position = tf.t;
        go.transform.rotation = tf.q;
        go.transform.localScale = tf.s;
        go.GC<Renderer>().material.color = col == Color.clear ? Color.white : col;
        if (!name.IsNullOrEmpty())
            go.name = name;
        go.transform.parent = parTf;
        return go;
    }
    ///<summary>обект үүсгэнэ</summary>
    public static T CrtGo<T>(GameObject goPf, Tf tf, string name = "", Transform parTf = null)
    {
        GameObject go = MonoBehaviour.Instantiate(goPf, tf.t, tf.q, parTf);
        go.transform.localScale = tf.s;
        if (!name.IsNullOrEmpty())
            go.name = name;
        return go.GC<T>();
    }
    ///<summary>обект үүсгээд устгана</summary>
    public static T CrtDstGo<T>(GameObject goPf, Tf tf, float time, string name = "", Transform parTf = null)
    {
        GameObject go = CrtGo<GameObject>(goPf, tf, name, parTf);
        MonoBehaviour.Destroy(go, time);
        return go.GC<T>();
    }
    ///<summary>обект үүсгээд хүүхдийг авна</summary>
    public static T CrtGoChild<T>(GameObject goPf, Tf tf, string childs = "", string name = "", Transform parTf = null)
    {
        GameObject go = CrtGo<GameObject>(goPf, tf, name, parTf);
        return go.Child<T>(childs);
    }
    ///<summary>обект үүсгээд хүүхдийг аваад устгана</summary>
    public static T CrtDstGoChild<T>(GameObject goPf, Tf tf, float time, string childs = "", string name = "", Transform parTf = null)
    {
        GameObject go = CrtGo<GameObject>(goPf, tf, name, parTf);
        MonoBehaviour.Destroy(go, time);
        return go.Child<T>(childs);
    }

    ///<summary>random обектууд үүсгэнэ</summary>
    public static List<GameObject> CrtRndGos(Vector3 a, Vector3 b, List<GameObject> goPfs, int n,
        RotType rt = RotType.Def, Quaternion rot = default(Quaternion), Transform parTf = null)
    {
        List<GameObject> res = new List<GameObject>();
        for (int i = 0; i < n; i++)
            res.Add(CrtRndGo(a, b, goPfs, rt, rot, parTf));
        return res;
    }
    ///<summary>random обектууд давхцахгүйгаар үүсгэнэ</summary>
    public static List<GameObject> CrtRndGos(Vector3 a, Vector3 b, List<GameObject> goPfs, int n, float dis,
        RotType rt = RotType.Def, Quaternion rot = default(Quaternion), Transform parTf = null)
    {
        return CrtRndGos(a, b, null, goPfs, n, dis, rt, rot, parTf);
    }
    ///<summary>random обектууд давхцахгүйгаар үүсгэнэ</summary>
    public static List<GameObject> CrtRndGos(Vector3 a, Vector3 b, List<GameObject> gos, List<GameObject> goPfs, int n, float dis,
        RotType rt = RotType.Def, Quaternion rot = default(Quaternion), Transform parTf = null)
    {
        List<GameObject> res = new List<GameObject>();
        List<Vector3> pnts = new List<Vector3>();
        if (gos.NotNull() && gos.Count > 0)
            gos.ForEach(go => pnts.Add(go.transform.position));
        for (int i = 0; i < 100000 && pnts.Count < n; i++)
        {
            Vector3 rnd = RndPos(a, b);
            if (pnts.FindIndex(0, pnts.Count, pnt => Vector3.Distance(pnt, rnd) < dis) < 0)
            {
                pnts.Add(rnd);
                res.Add(CrtRndGo(RndPos(a, b), goPfs, rt, rot, parTf));
            }
        }
        return res;
    }

    ///<summary>random обект үүсгэнэ [a, b]</summary>
    public static GameObject CrtRndGo(Vector3 a, Vector3 b, List<GameObject> goPfs,
        RotType rt = RotType.Def, Quaternion rot = default(Quaternion), Transform parTf = null)
    {
        return CrtRndGo(RndPos(a, b), goPfs, rt, rot, parTf);
    }
    ///<summary>random обект үүсгэнэ</summary>
    public static GameObject CrtRndGo(Vector3 pos, List<GameObject> goPfs,
        RotType rt = RotType.Def, Quaternion rot = default(Quaternion), Transform parTf = null)
    {
        GameObject goPf = RndList<GameObject>(goPfs);
        if (rt == RotType.Def) rot = goPf.transform.rotation;
        else if (rt == RotType.Rnd) rot = Quaternion.Euler(0, RndAng, 0);
        return MonoBehaviour.Instantiate(goPf, pos, rot, parTf);
    }



    // █▀▀▀▄ █▀▀▀▀ █▀▀▀▄ █   █ ▄▀▀▀▄ 
    // █   █ █▄▄▄  █▄▄▄▀ █   █ █     
    // █   █ █     █   █ █   █ █  ▀█ 
    // ▀▀▀▀  ▀▀▀▀▀ ▀▀▀▀   ▀▀▀   ▀▀▀

    ///<summary>шулуун зурна</summary>
    public static void DebugLine(Vector3 a, Vector3 b, Color col = default(Color), float dur = 0)
    {
        ColDefBlack(ref col);
        Debug.DrawLine(a, b, col, dur);
    }
    ///<summary>цацраг зурна</summary>
    public static void DebugRay(Vector3 a, Vector3 dir, Color col = default(Color), float dur = 0)
    {
        ColDefBlack(ref col);
        Debug.DrawRay(a, dir, col, dur);
    }
    ///<summary>муруй зурна</summary>
    public static void DebugCurve(List<Vector3> pnts, Color col = default(Color), float dur = 0, bool isShowPnt = false, float r = 0.1f, Color col2 = default(Color))
    {
        if (isShowPnt && pnts.Count > 0)
            DebugSphere(pnts[0], r, col2, dur);
        for (int i = 1; i < pnts.Count; i++)
        {
            DebugLine(pnts[i - 1], pnts[i], col, dur);
            if (isShowPnt)
                DebugSphere(pnts[i], r, col2, dur);
        }
    }
    ///<summary>тойрог зурна</summary>
    public static void DebugSphere(Vector3 pos, float r, Color col = default(Color), float dur = 0)
    {
        for (int ang = 0, dAng = 36; ang < 360; ang += dAng)
        {
            float sin1 = Sin(ang) * r, cos1 = Cos(ang) * r, sin2 = Sin(ang + dAng) * r, cos2 = Cos(ang + dAng) * r;
            DebugLine(pos + new Vector3(0, sin1, cos1), pos + new Vector3(0, sin2, cos2), col, dur); // y z
            DebugLine(pos + new Vector3(cos1, sin1, 0), pos + new Vector3(cos2, sin2, 0), col, dur); // x y
            DebugLine(pos + new Vector3(sin1, 0, cos1), pos + new Vector3(sin2, 0, cos2), col, dur); // x z
        }
    }
    ///<summary>цлиндр зурна</summary>
    public static void DebugCylinder(Vector3 pos, float r, float h, Color col = default(Color), float dur = 0)
    {
        for (int ang = 0, dAng = 36; ang < 360; ang += dAng)
        {
            float sin1 = Sin(ang) * r, cos1 = Cos(ang) * r, sin2 = Sin(ang + dAng) * r, cos2 = Cos(ang + dAng) * r;
            Vector3 a = new Vector3(sin1, 0, cos1), b = new Vector3(sin2, 0, cos2), c = Vector3.up * h / 2;
            DebugLine(pos + a + c, pos + b + c, col, dur); // up
            DebugLine(pos + a - c, pos + b - c, col, dur); // down
            DebugLine(pos + a + c, pos + a - c, col, dur); // line
        }
    }
    ///<summary>куб зурна</summary>
    public static void DebugCube(Vector3 pos, Vector3 size, Color col = default(Color), float dur = 0)
    {
        Vector3 half = size / 2;
        // up
        DebugLine(pos + new Vector3(-half.x, half.y, -half.z), pos + new Vector3(-half.x, half.y, half.z), col, dur);
        DebugLine(pos + new Vector3(-half.x, half.y, half.z), pos + new Vector3(half.x, half.y, half.z), col, dur);
        DebugLine(pos + new Vector3(half.x, half.y, half.z), pos + new Vector3(half.x, half.y, -half.z), col, dur);
        DebugLine(pos + new Vector3(half.x, half.y, -half.z), pos + new Vector3(-half.x, half.y, -half.z), col, dur);
        // middle
        DebugLine(pos + new Vector3(-half.x, half.y, -half.z), pos + new Vector3(-half.x, -half.y, -half.z), col, dur);
        DebugLine(pos + new Vector3(-half.x, half.y, half.z), pos + new Vector3(-half.x, -half.y, half.z), col, dur);
        DebugLine(pos + new Vector3(half.x, half.y, half.z), pos + new Vector3(half.x, -half.y, half.z), col, dur);
        DebugLine(pos + new Vector3(half.x, half.y, -half.z), pos + new Vector3(half.x, -half.y, -half.z), col, dur);
        // down
        DebugLine(pos + new Vector3(-half.x, -half.y, -half.z), pos + new Vector3(-half.x, -half.y, half.z), col, dur);
        DebugLine(pos + new Vector3(-half.x, -half.y, half.z), pos + new Vector3(half.x, -half.y, half.z), col, dur);
        DebugLine(pos + new Vector3(half.x, -half.y, half.z), pos + new Vector3(half.x, -half.y, -half.z), col, dur);
        DebugLine(pos + new Vector3(half.x, -half.y, -half.z), pos + new Vector3(-half.x, -half.y, -half.z), col, dur);
    }



    // ▀▀█▀▀ ▄▀▀▀▄ ▄▀▀▀▄ █     ▄▀▀▀▀ 
    //   █   █   █ █   █ █     ▀▄▄▄  
    //   █   █   █ █   █ █         █ 
    //   ▀    ▀▀▀   ▀▀▀  ▀▀▀▀▀ ▀▀▀▀

    ///<summary>Screenshot хийнэ</summary>
    public static void Screenshot(string path, string name)
    {
        if (!System.IO.Directory.Exists(path))
            System.IO.Directory.CreateDirectory(path);
        string fileName = path + "/" + name + ".png";
#if UNITY_5
        Application.CaptureScreenshot (fileName, 1);
#else
        ScreenCapture.CaptureScreenshot(fileName, 1);
#endif
    }
    ///<summary>T төрөлтэй бүх обектуудыг авна</summary>
    public static List<T> FOsOTA<T>() { return Resources.FindObjectsOfTypeAll(typeof(T)).ToList().TypeCast<Object, T>(); }
    ///<summary>T төрөлтэй бүх идэвхитэй обектуудыг авна</summary>
    public static List<T> FOsOT<T>() { return Object.FindObjectsOfType(typeof(T)).ToList().TypeCast<Object, T>(); }

    ///<summary>sprite border болгоно</summary>
    public static Vector4 SprBorder(float l, float r, float t, float b) { return new Vector4(l, b, r, t); }
    ///<summary>sprite-н border-г өөрчилнэ</summary>
    public static void SetSprBorder(string path, Vector4 border) { FileReplaceStrings(GetPrvPath() + path, "spriteBorder: {[\\w:\\.,: \\d]+}", "spriteBorder: {x: " + border.x + ", y: " + border.y + ", z: " + border.z + ", w: " + border.w + "}"); }
    ///<summary>assets folder-н замыг авна</summary>
    public static string GetPrvPath() { return (System.IO.Directory.GetCurrentDirectory() + "\\Assets\\"); }
    ///<summary>file-г regex хийнэ</summary>
    public static void FileReplaceStrings(string path, string regex, string replace) { System.IO.File.WriteAllText(path, System.Text.RegularExpressions.Regex.Replace(System.IO.File.ReadAllText(path), regex, replace)); }
    ///<summary>format string-г key-г data-р орлуулж тавина</summary>
    public static string Format(string format, params string[] keyDatas)
    {
        if (keyDatas.Length == 2)
            return format.Contains(keyDatas[0]) ? format.Replace(keyDatas[0], keyDatas[1]) : format + keyDatas[1];
        else if (keyDatas.Length > 2)
            for (int i = 0, n = keyDatas.Length - (keyDatas.Length % 2 == 0 ? 0 : 1); i < n; i += 2)
                format = format.Contains(keyDatas[i]) ? format.Replace(keyDatas[i], keyDatas[i + 1]) : format;
        return format;
    }
    ///<summary>a обектийн name нэртэй хувьсагчийн мэдээллийг авна</summary>
    public static FieldInfo Field<T>(T a, string name) { return a.GetType().GetField(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic); }
    ///<summary>Sprite-г Texture2D болгоно</summary>
    public static Texture2D SprTex(Sprite spr) { return spr.texture; }
    ///<summary>Texture2D-г Sprite болгоно</summary>
    public static Sprite TexSpr(Texture2D tex) { return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.one * 0.5f); }
    static Dictionary<System.Type, System.Func<object, object, float>> dicDisFunc = new Dictionary<System.Type, System.Func<object, object, float>>() { //
        { typeof (int), (a, b) => Mathf.Abs ((int) a - (int) b) }, //
        { typeof (float), (a, b) => Mathf.Abs ((float) a - (float) b) }, //
        { typeof (Vector2), (a, b) => Vector2.Distance ((Vector2) a, (Vector2) b) }, //
        { typeof (Vector2Int), (a, b) => Vector2Int.Distance ((Vector2Int) a, (Vector2Int) b) }, //
        { typeof (Vector3), (a, b) => Vector3.Distance ((Vector3) a, (Vector3) b) }, //
        { typeof (Vector3Int), (a, b) => Vector3Int.Distance ((Vector3Int) a, (Vector3Int) b) }, //
        { typeof (Vector4), (a, b) => Vector4.Distance ((Vector4) a, (Vector4) b) }, //
        { typeof (Transform), (a, b) => Vector3.Distance (((Transform) a).position, ((Transform) b).position) }, //
        { typeof (GameObject), (a, b) => Vector3.Distance (((GameObject) a).transform.position, ((GameObject) b).transform.position) }, //
        { typeof (MonoBehaviour), (a, b) => Vector3.Distance (((MonoBehaviour) a).transform.position, ((MonoBehaviour) b).transform.position) }, //
    };
    static Dictionary<System.Type, System.Func<object, Vector3, float>> dicPosDisFunc = new Dictionary<System.Type, System.Func<object, Vector3, float>>() { //
        { typeof (Vector3), (a, b) => Vector3.Distance ((Vector3) a, b) }, //
        { typeof (Transform), (a, b) => Vector3.Distance (((Transform) a).position, b) }, //
        { typeof (GameObject), (a, b) => Vector3.Distance (((GameObject) a).transform.position, b) }, //
        { typeof (MonoBehaviour), (a, b) => Vector3.Distance (((MonoBehaviour) a).transform.position, b) }, //
    };
    ///<summary>t-тэй ойрхон lis-н элементийн idx-г авна</summary>
    public static int GetNearIdx<T>(List<T> lis, T t)
    {
        int idx = -1;
        if (lis.Count > 0 && (dicDisFunc.ContainsKey(lis[0].GetType()) || (lis[0] is MonoBehaviour)))
        {
            float minDis = float.MaxValue, dis;
            System.Func<object, object, float> f = dicDisFunc[(lis[0] is MonoBehaviour) ? typeof(MonoBehaviour) : lis[0].GetType()];
            for (int i = 0; i < lis.Count; i++)
            {
                dis = f(lis[i], t);
                if (dis < minDis)
                {
                    minDis = dis;
                    idx = i;
                }
            }
        }
        return idx;
    }
    ///<summary>pos байрлалтай ойрхон lis-н элементийн idx-г авна</summary>
    public static int GetPosNearIdx<T>(List<T> l, Vector3 pos)
    {
        int idx = -1;
        if (l.Count > 0 && (dicPosDisFunc.ContainsKey(l[0].GetType()) || (l[0] is MonoBehaviour)))
        {
            float minDis = float.MaxValue, dis;
            System.Func<object, Vector3, float> f = dicPosDisFunc[(l[0] is MonoBehaviour) ? typeof(MonoBehaviour) : l[0].GetType()];
            for (int i = 0; i < l.Count; i++)
            {
                dis = f(l[i], pos);
                if (dis < minDis)
                {
                    minDis = dis;
                    idx = i;
                }
            }
        }
        return idx;
    }
    ///<summary>t-тэй ойрхон lis-н элементийн idx-г авна</summary>
    public static List<T> DisSort<T>(List<T> l, T t)
    {
        List<T> lis = null;
        if (l.Count > 0 && (dicDisFunc.ContainsKey(l[0].GetType()) || (l[0] is MonoBehaviour)))
        {
            System.Func<object, object, float> f = dicDisFunc[(l[0] is MonoBehaviour) ? typeof(MonoBehaviour) : l[0].GetType()];
            List<Vector2> disLis = new List<Vector2>();
            for (int i = 0; i < l.Count; i++)
                disLis.Add(new Vector2(i, f(l[i], t)));
            disLis.Sort((a, b) => a.y.CompareTo(b.x));
            lis = new List<T>();
            for (int i = 0; i < disLis.Count; i++)
                lis.Add(l[(int)disLis[i].x]);
        }
        return lis;
    }
    ///<summary>time-г string болгоно</summary>
    public static string TimeStr(float time) { return System.TimeSpan.FromSeconds(Mathf.FloorToInt(time)).ToString().Substring(3); }
    ///<summary>a, b-г солино</summary>
    public static void Swap<T>(ref T a, ref T b) { T t = a; a = b; b = t; }
    ///<summary>lis-г холино</summary>
    public static void Shuffle<T>(ref List<T> lis)
    {
        for (int i = 0; i < lis.Count; i++)
        {
            T tmp = lis[i];
            int randIdx = UnityEngine.Random.Range(i, lis.Count);
            lis[i] = lis[randIdx];
            lis[randIdx] = tmp;
        }
    }
    ///<summary>a, b list-г нэмнэ</summary>
    public static List<T> Add2List<T>(List<T> a, List<T> b)
    {
        List<T> res = a;
        foreach (T e in b) res.Add(e);
        return res;
    }



    // █▀▀▀▄ ▄▀▀▀▄  ▀█▀  █   █ ▀▀█▀▀ ▄▀▀▀▀ 
    // █▄▄▄▀ █   █   █   █▀▄ █   █   ▀▄▄▄  
    // █     █   █   █   █  ▀█   █       █ 
    // ▀      ▀▀▀   ▀▀▀  ▀   ▀   ▀   ▀▀▀▀

    ///<summary>pnts-н цэгүүдийн хоорондох зайг ижил dis болгох</summary>
    public static List<Vector3> PntsSameDisBetPnts(List<Vector3> pnts, float dis)
    {
        List<Vector3> res = new List<Vector3>();
        if (pnts.Count > 0)
        {
            Vector3 p = pnts[0];
            res.Add(p);
            for (int i = 1; i < pnts.Count; i++)
            {
                while (Vector3.Distance(p, pnts[i]) > dis)
                {
                    p = Vector3.MoveTowards(p, pnts[i], dis);
                    res.Add(p);
                }
            }
            res.Add(pnts[pnts.Count - 1]);
        }
        return res;
    }
    ///<summary>pnts-н цэгүүдийн зай</summary>
    public static float PntsDis(List<Vector3> pnts)
    {
        float res = 0;
        for (int i = 1; i < pnts.Count; i++)
            res += VecDis(pnts[i - 1], pnts[i]);
        return res;
    }
    ///<summary>3-н цэгийн bezier-н муруй</summary>
    public static Vector3 QuadraticCurve(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        return VecLerp(VecLerp(a, b, t), VecLerp(b, c, t), t);
    }
    ///<summary>4-н цэгийн bezier-н муруй</summary>
    public static Vector3 CubicCurve(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
    {
        return VecLerp(QuadraticCurve(a, b, c, t), QuadraticCurve(b, c, d, t), t);
    }
    ///<summary>CatmullRom муруй p1-с p2-н хоорондох цэгүүдийг үүсгэнэ</summary>
    public static List<Vector3> CatmullRomSpline(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, int smooth, bool isStaIn = true, bool isEndIn = true)
    {
        List<Vector3> res = new List<Vector3>();
        if (isStaIn)
            res.Add(p1);
        for (int i = 1; i < smooth; i++)
            res.Add(GetCatmullRomPosition(p0, p1, p2, p3, (float)i / smooth));
        if (isEndIn)
            res.Add(p2);
        return res;
    }
    ///<summary>CatmullRom муруйн цэг үүсгэх томъёо</summary>
    public static Vector3 GetCatmullRomPosition(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        return 0.5f * ((2f * p1) + (p2 - p0) * t + (2f * p0 - 5f * p1 + 4f * p2 - p3) * t * t + (-p0 + 3f * p1 - 3f * p2 + p3) * t * t * t);
    }



    // █▄ ▄█ █▀▀▀▀ ▄▀▀▀▀ █   █ 
    // █ █ █ █▄▄▄  ▀▄▄▄  █▄▄▄█ 
    // █   █ █         █ █   █ 
    // ▀   ▀ ▀▀▀▀▀ ▀▀▀▀  ▀   ▀

    public static int[] UvUArr = new int[] { 0, 0, 1, 1, 0, 1 };
    public static int[] UvVArr = new int[] { 0, 1, 0, 0, 1, 1 };

    ///<summary>mesh null байх үед шинийг үүсгэж материалд нь стандарт материал өгнө</summary>
    public static void MeshInit(ref Mesh mesh, GameObject go)
    {
        if (mesh.Null())
        {
            mesh = new Mesh();
            go.GC<MeshFilter>().sharedMesh = mesh;
            go.GC<MeshRenderer>().material = new Material(Shader.Find("Standard"));
        }
    }
    ///<summary>mesh-р эргэлтийн бие үүсгэнэ</summary>
    public static void MeshRotModel(ref Mesh mesh, bool isFill, bool isDisUv, int n, List<Vector2> points, Tf tf)
    {
        List<Vector2> lis = new List<Vector2>(points);
        if (isFill)
        {
            lis.Insert(0, Vec2X(points[0], 0));
            lis.Add(Vec2X(points[points.Count - 1], 0));
        }
        // vertices
        Vector3[] vers = new Vector3[n * lis.Count];
        float dAng = 360f / n;
        for (int i = 0; i < lis.Count; i++)
            for (int j = 0; j < n; j++)
                vers[i * n + j] = TfRotate(tf.r, VecMul(new Vector3(lis[i].x * Cos(j * dAng), lis[i].y, lis[i].x * Sin(j * dAng)), tf.s)) + tf.t;
        // triangles
        int[] tris = new int[n * 6 * (lis.Count - 1)];
        for (int i = 0; i < lis.Count - 1; i++)
            for (int j = 0; j < n; j++)
                for (int k = 0; k < 6; k++)
                    tris[n * 6 * i + j * 6 + k] = n * i + (j + UvUArr[k]) % n + UvVArr[k] * n;
        // uv
        Vector3[] v = new Vector3[tris.Length];
        int[] t = new int[tris.Length];
        Vector2[] uv = new Vector2[tris.Length];
        float du = 1f / n, dv = 1f / (lis.Count - 1);
        if (isDisUv)
        {
            float dis = 0, tmpDis, curDis = 0;
            List<float> disLis = new List<float>();
            for (int i = 1; i < lis.Count; i++)
            {
                tmpDis = Vector3.Distance(lis[i - 1], lis[i]);
                disLis.Add(tmpDis);
                dis += tmpDis;
            }
            for (int i = 0; i < tris.Length; i++)
            {
                v[i] = vers[tris[i]];
                t[i] = i;
                uv[i] = new Vector2(
                    (i / 6 % n + UvUArr[i % 6]) * du,
                    (curDis + UvVArr[i % 6] * disLis[i / 6 / n]) / dis
                );
                if ((i + 1) % (6 * n) == 0)
                    curDis += disLis[i / 6 / n];
            }
        }
        else
        {
            for (int i = 0; i < tris.Length; i++)
            {
                v[i] = vers[tris[i]];
                t[i] = i;
                uv[i] = new Vector2(
                    (i / 6 % n + UvUArr[i % 6]) * du,
                    (i / 6 / n + UvVArr[i % 6]) * dv
                );
            }
        }
        // mesh
        mesh.Clear();
        mesh.vertices = v;
        mesh.triangles = t;
        mesh.uv = uv;
        mesh.RecalculateNormals();
    }
    ///<summary>mesh-р зам үүсгэнэ</summary>
    public static void MeshRoad(ref Mesh mesh, bool isLoop, List<Vector3> points, Vector3 dPos, Vector2 size)
    {
        if (points.Count >= 2 && mesh.NotNull())
        {
            int n = 4;
            List<Vector3> lis = new List<Vector3>(points);
            if (isLoop)
                lis.Add(lis[0]);
            // vertices
            Vector3[] vers = new Vector3[lis.Count * n];
            Vector3 d = Vector3.zero, dir;
            for (int i = 0; i < lis.Count; i++)
            {
                if (isLoop && (i == 0 || i == lis.Count - 1))
                {
                    dir = Vector3.Lerp(lis[1] - lis[0], lis[lis.Count - 1] - lis[lis.Count - 2], 0.5f);
                    d = new Vector3(dir.z, 0, -dir.x);
                }
                else if (i < lis.Count - 1)
                {
                    dir = lis[i + 1] - lis[i];
                    d += new Vector3(dir.z, 0, -dir.x);
                }
                else
                {
                    dir = lis[i - 1] - lis[i];
                    d += new Vector3(-dir.z, 0, dir.x);
                }
                d.Normalize();
                vers[i * n + 0] = lis[i] - d * size.x / 2 - Vector3.up * size.y / 2+dPos;
                vers[i * n + 1] = lis[i] - d * size.x / 2 + Vector3.up * size.y / 2+dPos;
                vers[i * n + 2] = lis[i] + d * size.x / 2 + Vector3.up * size.y / 2+dPos;
                vers[i * n + 3] = lis[i] + d * size.x / 2 - Vector3.up * size.y / 2+dPos;
            }
            // triangles
            int[] tris = new int[n * 6 * (lis.Count - 1) + 12];
            for (int i = 0; i < lis.Count - 1; i++)
                for (int j = 0; j < n; j++)
                    for (int k = 0; k < 6; k++)
                        tris[n * 6 * i + j * 6 + k] = n * i + (j + UvUArr[k]) % n + UvVArr[k] * n;
            int[] fbArr = new int[] { 0, 1, 3, 3, 1, 2 };
            for (int i = 0; i < 12; i++)
                tris[n * 6 * (lis.Count - 1) + i] = i < 6 ? fbArr[i] : (vers.Length - 1 - fbArr[i - 6]);
            // uv
            Vector3[] v = new Vector3[tris.Length];
            int[] t = new int[tris.Length];
            Vector2[] uv = new Vector2[tris.Length];
            float du = 1f / n, dv = 1f / (lis.Count - 1);
            for (int i = 0; i < tris.Length; i++)
            {
                v[i] = vers[tris[i]];
                t[i] = i;
                uv[i] = new Vector2(
                    (i % 24 / 6 + UvUArr[i % 24 % 6]) * du,
                    (i / 24 + UvVArr[i % 24 % 6]) * dv
                );
            }
            // mesh
            mesh.Clear();
            mesh.vertices = v;
            mesh.triangles = t;
            mesh.uv = uv;
            mesh.RecalculateNormals();
        }
    }



    // █   █ ▄▀▀▀▄ █▀▀▀▄  ▀█▀  ▀▀▀▀█ ▄▀▀▀▄ █   █ ▀▀█▀▀ ▄▀▀▀▄ █           █▀▀▀▄ █▀▀▀▄ ▄▀▀▀▄   ▀█▀ █▀▀▀▀ ▄▀▀▀▄ ▀▀█▀▀  ▀█▀  █     █▀▀▀▀ 
    // █▄▄▄█ █   █ █▄▄▄▀   █     ▄▀  █   █ █▀▄ █   █   █▄▄▄█ █           █▄▄▄▀ █▄▄▄▀ █   █    █  █▄▄▄  █       █     █   █     █▄▄▄  
    // █   █ █   █ █ ▀▄    █   ▄▀    █   █ █  ▀█   █   █   █ █           █     █ ▀▄  █   █ ▄  █  █     █   ▄   █     █   █     █     
    // ▀   ▀  ▀▀▀  ▀   ▀  ▀▀▀  ▀▀▀▀▀  ▀▀▀  ▀   ▀   ▀   ▀   ▀ ▀▀▀▀▀       ▀     ▀   ▀  ▀▀▀   ▀▀   ▀▀▀▀▀  ▀▀▀    ▀    ▀▀▀  ▀▀▀▀▀ ▀▀▀▀▀ 
    // █▄ ▄█ ▄▀▀▀▄ ▀▀█▀▀  ▀█▀  ▄▀▀▀▄ █   █ 
    // █ █ █ █   █   █     █   █   █ █▀▄ █ 
    // █   █ █   █   █     █   █   █ █  ▀█ 
    // ▀   ▀  ▀▀▀    ▀    ▀▀▀   ▀▀▀  ▀   ▀

    ///<summary>a-с v0 анхны хурдтай хөдөлхөд time хугацаанд туулах n-ш цэгүүд</summary>
    public static List<Vector3> HpmPnts(Vector3 a, Vector3 v0, float time, int n)
    {
        List<Vector3> res = new List<Vector3>() { a };
        for (int i = 1; i <= n; i++)
            res.Add(HpmPnt(a, v0, time * i / n));
        return res;
    }
    ///<summary>a-с v0 анхны хурдтай хөдөлхөд time хугацаанд туулах цэг</summary>
    public static Vector3 HpmPnt(Vector3 a, Vector3 v0, float time) { return a + v0 * time + Vector3.up * Physics.gravity.y * time * time / 2f; }
    ///<summary>a-с b-рүү h өндөртэй явах үеийн анхны хурд</summary>
    public static Vector3 HpmH(Vector3 a, Vector3 b, float h)
    {
        float time = 0, ang = 0;
        return HpmH(a, b, h, ref time, ref ang);
    }
    ///<summary>a-с b-рүү h өндөртэй явах үеийн анхны хурд, хугацаа, өнцөг</summary>
    public static Vector3 HpmH(Vector3 a, Vector3 b, float h, ref float time, ref float ang)
    {
        float g = Physics.gravity.y;
        float disY = b.y - a.y;
        Vector3 disXZ = new Vector3(b.x - a.x, 0, b.z - a.z);
        time = Mathf.Sqrt(-2 * h / g) + Mathf.Sqrt(2 * (disY - h) / g);
        Vector3 vY = Vector3.up * Mathf.Sqrt(-2 * g * h);
        Vector3 vXZ = disXZ / time;
        Vector3 v0 = vXZ + vY * -Mathf.Sign(g);
        ang = Vector3.Angle(v0, new Vector3(v0.x, 0, v0.z));
        return v0;
    }
    ///<summary>a-с b-рүү ang өнцөгтэй явах үеийн анхны хурд</summary>
    public static Vector3 HpmAng(Vector3 a, Vector3 b, float ang)
    {
        float time = 0, h = 0;
        return HpmAng(a, b, ang, ref time, ref h);
    }
    ///<summary>a-с b-рүү ang өнцөгтэй явах үеийн анхны хурд, хугацаа, өндөр</summary>
    public static Vector3 HpmAng(Vector3 a, Vector3 b, float ang, ref float time, ref float h)
    {
        float g = Physics.gravity.y;
        Vector3 aXZ = new Vector3(a.x, 0f, a.z);
        Vector3 bXZ = new Vector3(b.x, 0f, b.z);
        float disXZ = Vector3.Distance(aXZ, bXZ);
        float tanAlpha = Mathf.Tan(ang * Mathf.Deg2Rad);
        float disY = b.y - a.y;
        float vZ = Mathf.Sqrt(g * disXZ * disXZ / (2f * (disY - disXZ * tanAlpha)));
        float vY = tanAlpha * vZ;
        Vector3 v0 = Quaternion.LookRotation(bXZ - a) * new Vector3(0f, vY, vZ);
        h = -v0.y * v0.y / (2 * g);
        time = time = Mathf.Sqrt(-2 * h / g) + Mathf.Sqrt(2 * (disY - h) / g);
        return v0;
    }
    ///<summary>v0 анхны хурдтай явах үеийн дээд өндөр</summary>
    public static float HpmV0H(Vector3 v0) { return -v0.y * v0.y / (2 * Physics.gravity.y); }
    ///<summary>v0 анхны хурдтай явах үеийн өнцөг</summary>
    public static float HpmV0Ang(Vector3 v0) { return Mathf.Sign(v0.y) * Vector3.Angle(v0, new Vector3(v0.x, 0, v0.z)); }



    // █▀▀▀▄ ▄▀▀▀▄ ▄▀▀▀▄ █▀▀▀▄ 
    // █▄▄▄▀ █   █ █▄▄▄█ █   █ 
    // █ ▀▄  █   █ █   █ █   █ 
    // ▀   ▀  ▀▀▀  ▀   ▀ ▀▀▀▀

    public static List<RoadData> RoadDirs2Datas(List<RoadDir> dirs)
    {
        List<RoadData> datas = new List<RoadData>();
        RoadDir prvDir, curDir;
        Vector3Int defPrvPrvPos = new Vector3Int(0, 0, -2), defPrvPos = new Vector3Int(0, 0, -1),
            prvPrvPos, prvPos, curPos;
        Quaternion curRot;
        for (int i = 0; i < dirs.Count; i++)
        {
            prvPrvPos = i == 0 ? defPrvPrvPos : i == 1 ? defPrvPos : datas[i - 2].pos;
            prvPos = i == 0 ? defPrvPos : datas[i - 1].pos;
            prvDir = i == 0 ? RoadDir.F : datas[i - 1].dir;
            curDir = dirs[i];
            curPos = RoadCurPos(prvDir, prvPrvPos, prvPos);
            curRot = RoadCurRot(prvPos, curPos);
            datas.Add(new RoadData(curDir, curPos, curRot));
        }
        return datas;
    }
    public static List<RoadData> RoadCreateDatas(int n, int staF, int endF)
    {
        bool isDone = false;
        List<RoadData> datas = new List<RoadData>();
        for (int i = 0; i < staF; i++)
        {
            Vector3Int curPos = RoadCurPos(RoadDir.F, RoadDatasPos(datas, i - 2), RoadDatasPos(datas, i - 1));
            datas.Add(new RoadData(RoadDir.F, curPos, RoadCurRot(RoadDatasPos(datas, i - 1), curPos)));
        }
        RoadCreateData(RoadDir.F, RoadDir.F, RoadDatasPos(datas, datas.Count - 2), RoadDatasPos(datas, datas.Count - 1),
            0, n - staF - endF, datas, ref isDone);
        for (int i = 0; i < endF; i++)
        {
            Vector3Int curPos = RoadCurPos(datas[datas.Count - 1].dir, datas[datas.Count - 2].pos, datas[datas.Count - 1].pos);
            datas.Add(new RoadData(RoadDir.F, curPos, RoadCurRot(datas[datas.Count - 1].pos, curPos)));
        }
        return datas;
    }
    static Vector3Int RoadDatasPos(List<RoadData> datas, int i) { return i < 0 ? new Vector3Int(0, 0, i) : datas[i].pos; }
    static void RoadCreateData(RoadDir prvDir, RoadDir curDir, Vector3Int prvPrvPos, Vector3Int prvPos, int iLen, int len,
        List<RoadData> datas, ref bool isDone)
    {
        if (isDone) return;
        if (iLen >= len)
        {
            isDone = true;
            return;
        }
        Vector3Int curPos = RoadCurPos(prvDir, prvPrvPos, prvPos);
        if (RoadIsOverlap(prvDir, prvPrvPos, prvPos, new List<RoadDir>() { curDir, RoadDir.F, RoadDir.F }, datas)) return;
        Quaternion curRot = RoadCurRot(prvPos, curPos);
        datas.Add(new RoadData(curDir, curPos, curRot));
        List<RoadDir> rndDirs = new List<RoadDir> { RoadDir.LD, RoadDir.FD, RoadDir.RD, RoadDir.L, RoadDir.F, RoadDir.R, RoadDir.LU, RoadDir.FU, RoadDir.RU };
        Shuffle<RoadDir>(ref rndDirs);
        for (int i = 0; i < rndDirs.Count; i++)
            RoadCreateData(curDir, rndDirs[i], prvPos, curPos, iLen + 1, len, datas, ref isDone);
        if (isDone) return;
        datas.RemoveAt(datas.Count - 1);
    }
    static Vector3Int RoadCurPos(RoadDir prvDir, Vector3Int prvPrvPos, Vector3Int prvPos)
    {
        Vector3 t = Vec3IVec3(prvPos);
        Quaternion r = Quaternion.LookRotation(t - new Vector3(prvPrvPos.x, prvPos.y, prvPrvPos.z));
        Vector3 s = Vector3.one;
        Vector3 pos = Vec3IVec3(new List<Vector3Int>() {
            new Vector3Int (-1, -1, 0), new Vector3Int (0, -1, 1), new Vector3Int (1, -1, 0),
                new Vector3Int (-1, 0, 0), new Vector3Int (0, 0, 1), new Vector3Int (1, 0, 0),
                new Vector3Int (-1, 1, 0), new Vector3Int (0, 1, 1), new Vector3Int (1, 1, 0),
        }[(int)prvDir]);
        return Vector3Int.RoundToInt(Matrix4x4.TRS(t, r, s).MultiplyPoint3x4(pos));
    }
    static Quaternion RoadCurRot(Vector3Int prvPos, Vector3Int curPos) { return Quaternion.LookRotation(Vec3IVec3(curPos) - new Vector3(prvPos.x, curPos.y, prvPos.z)); }
    static bool RoadIsOverlap(RoadDir prvDir, Vector3Int prvPrvPos, Vector3Int prvPos, List<RoadDir> dirs, List<RoadData> datas)
    {
        List<Vector3Int> posLis = new List<Vector3Int>();
        for (int i = 0; i < dirs.Count; i++)
        {
            posLis.Add(RoadCurPos(
                i == 0 ? prvDir : dirs[i - 1],
                i == 0 ? prvPrvPos : i == 1 ? prvPos : posLis[i - 2],
                i == 0 ? prvPos : posLis[i - 1]
            ));
        }
        for (int i = 0; i < datas.Count; i++)
        {
            for (int j = 0; j < posLis.Count; j++)
            {
                // if (datas[i].pos == posLis[j] - Vector3Int.up * 2 ||
                //     datas[i].pos == posLis[j] - Vector3Int.up ||
                //     datas[i].pos == posLis[j] ||
                //     datas[i].pos == posLis[j] + Vector3Int.up ||
                //     datas[i].pos == posLis[j] + Vector3Int.up * 2) {
                //     return true;
                // }
                if (datas[i].pos == posLis[j] ||
                    (((int)dirs[j] / 3 == 2) && ( // ^
                        (datas[i].pos == posLis[j] + Vector3Int.up) || // ^*
                        (datas[i].pos == posLis[j] + Vector3Int.up * 2 && (int)datas[i].dir / 3 == 0) // ^^v
                    )) ||
                    (((int)dirs[j] / 3 == 1) && ( // -
                        (datas[i].pos == posLis[j] + Vector3Int.up && (int)datas[i].dir / 3 == 0) || // ^v
                        (datas[i].pos == posLis[j] + Vector3Int.down && (int)datas[i].dir / 3 == 2) // v^
                    )) ||
                    (((int)dirs[j] / 3 == 0) && ( // v
                        (datas[i].pos == posLis[j] + Vector3Int.down) || // v*
                        (datas[i].pos == posLis[j] + Vector3Int.down * 2 && (int)datas[i].dir / 3 == 2) // vv^
                    ))
                ) return true;
            }
        }
        return false;
    }
    static void RoadAddDatas(RoadDir prvDir, Vector3Int prvPrvPos, Vector3Int prvPos, List<RoadDir> dirs, List<RoadData> datas,
        ref RoadDir refCurDir, ref Vector3Int refPrvPos, ref Vector3Int refCurPos)
    {
        List<Vector3Int> idxs = new List<Vector3Int>();
        for (int i = 0; i < dirs.Count; i++)
        {
            refPrvPos = i == 0 ? prvPos : idxs[i - 1];
            refCurDir = dirs[i];
            refCurPos = RoadCurPos(
                i == 0 ? prvDir : dirs[i - 1],
                i == 0 ? prvPrvPos : i == 1 ? prvPos : idxs[i - 2],
                refPrvPos
            );
            idxs.Add(refCurPos);
            datas.Add(new RoadData(refCurDir, refCurPos, RoadCurRot(refPrvPos, refCurPos)));
        }
    }



    // █     █▀▀▀▀ █   █ █▀▀▀▀ █     
    // █     █▄▄▄  █   █ █▄▄▄  █     
    // █     █      █ █  █     █     
    // ▀▀▀▀▀ ▀▀▀▀▀   ▀   ▀▀▀▀▀ ▀▀▀▀▀

    ///<summary>үеийн өнгөнөөс tile-г авна</summary>
    public static List<LevelTile> TexGetTiles(List<Color> colors, Vector2Int size, Color emptyCol, bool isCorner)
    {
        List<LevelTile> res = new List<LevelTile>();
        List<Vector2Int> levelDirs = new List<Vector2Int>() {
            new Vector2Int (-1, 0), new Vector2Int (-1, 1), new Vector2Int (0, 1), new Vector2Int (1, 1),
            new Vector2Int (1, 0), new Vector2Int (1, -1), new Vector2Int (0, -1), new Vector2Int (-1, -1),
        };
        System.Func<int, int, int, int, List<Color>, Color, bool> isEmpty = (x, y, w, h, cols, empCol) =>
            (x < 0 || x >= w || y < 0 || y >= h || cols[TexGetIdx(x, y, w, h)] == empCol);
        for (int y = 0; y < size.y; y++)
        {
            for (int x = 0; x < size.x; x++)
            {
                List<bool> actives = new List<bool>() { !isEmpty(x, y, size.x, size.y, colors, emptyCol) };
                for (int i = 0; i < 4; i++)
                {
                    bool dir0 = !isEmpty(x + levelDirs[i * 2].x, y + levelDirs[i * 2].y, size.x, size.y, colors, emptyCol),
                        dir1 = !isEmpty(x + levelDirs[i * 2 + 1].x, y + levelDirs[i * 2 + 1].y, size.x, size.y, colors, emptyCol),
                        dir2 = !isEmpty(x + levelDirs[(i * 2 + 2) % 8].x, y + levelDirs[(i * 2 + 2) % 8].y, size.x, size.y, colors, emptyCol);
                    actives.Add(isCorner ? (actives[0] ? (dir0 || dir1 || dir2) : (dir0 && dir2)) :
                        (actives[0] ? ((dir0 && dir1) || (dir1 && dir2) || (dir2 && dir0) || (dir0 && !dir1) || (dir2 && !dir1)) : (dir0 && dir1 && dir2)));
                }
                if (actives.Contains(true))
                    res.Add(new LevelTile(new Vector2Int(x, y), actives.ToArray()));
            }
        }
        return res;
    }
    ///<summary>зурагнаас үеийн өнгийг авна</summary>
    public static List<Color> TexGetLevelColors(Texture2D tex, Color borderCol, int level, out Vector2Int size)
    {
        int w = tex.width, h = tex.height;
        List<Color> colors = tex.GetPixels(0, 0, w, h).ToList();
        for (int y = 1, lvl = 0; y < h; y++)
        {
            for (int x = 0; x < w; x++)
            {
                if (colors[TexGetIdx(x, y, w, h)] == borderCol)
                {
                    bool isFirst = true;
                    for (int ix = x, iy = y; ix < w; ix++)
                    {
                        if (colors[TexGetIdx(ix, iy, w, h)] == borderCol)
                        {
                            Vector2Int tmpSize = TexGetRectSize(ix, iy, w, h, colors);
                            if (++lvl == level)
                            {
                                size = tmpSize - Vector2Int.one * 2;
                                return tex.GetPixels(ix + 1, h - iy - size.y - 1, size.x, size.y).ToList();
                            }
                            if (isFirst)
                            {
                                isFirst = false;
                                y += tmpSize.y;
                            }
                            ix += tmpSize.x;
                        }
                    }
                    break;
                }
            }
        }
        size = Vector2Int.zero;
        return null;
    }
    ///<summary>зурагнаас үеийн хүрээний хэмжэээг авна</summary>
    public static Vector2Int TexGetRectSize(int x, int y, int w, int h, List<Color> colors)
    {
        Color col = colors[TexGetIdx(x, y, w, h)];
        Vector2Int res = Vector2Int.zero;
        for (int ix = x; ix < w; ix++)
            if (colors[TexGetIdx(ix, y, w, h)] == col) res.x++;
            else break;
        for (int iy = y; iy < h; iy++)
            if (colors[TexGetIdx(x, iy, w, h)] == col) res.y++;
            else break;
        return res;
    }
    ///<summary>зурагны координатаар idx-г авах</summary>
    public static int TexGetIdx(int x, int y, int w, int h) { return (h - y - 1) * w + x; }



    // █   █  ▀█▀  █▀▀▀▄ █▀▀▀▄ ▄▀▀▀▄ ▀▀█▀▀  ▀█▀  ▄▀▀▀▄ █   █ 
    // █   █   █   █▄▄▄▀ █▄▄▄▀ █▄▄▄█   █     █   █   █ █▀▄ █ 
    //  █ █    █   █   █ █ ▀▄  █   █   █     █   █   █ █  ▀█ 
    //   ▀    ▀▀▀  ▀▀▀▀  ▀   ▀ ▀   ▀   ▀    ▀▀▀   ▀▀▀  ▀   ▀

    ///<summary>vibrate дугаргана</summary>
    public static void Vibrate(long ms)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        Vibration.Vibrate (ms);
#endif
    }
    ///<summary>vibrate дугаргана</summary>
    public static void Vibrate(Vib v)
    {
        if (GameController.IsVibration)
        {
#if UNITY_EDITOR
            Debug.Log("Vibrate: " + v);
#elif UNITY_ANDROID && !UNITY_EDITOR
            switch (v) {
                case Vib.Success:
                    Vibration.Vibrate (new long[] { 20, 20 }, -1);
                    break;
                case Vib.Warning:
                    Vibration.Vibrate (new long[] { 10, 10 }, -1);
                    break;
                case Vib.Error:
                    Vibration.Vibrate (new long[] { 20, 20, 20 }, -1);
                    break;
                case Vib.Light:
                    Vibration.Vibrate (10);
                    break;
                case Vib.Medium:
                    Vibration.Vibrate (15);
                    break;
                case Vib.Heavy:
                    Vibration.Vibrate (20);
                    break;
            }
#elif UNITY_IPHONE && !UNITY_EDITOR
            switch (v) {
                case Vib.Success:
                    TapticPlugin.TapticManager.Notification (TapticPlugin.Notification.Success);
                    break;
                case Vib.Warning:
                    TapticPlugin.TapticManager.Notification (TapticPlugin.Notification.Warning);
                    break;
                case Vib.Error:
                    TapticPlugin.TapticManager.Notification (TapticPlugin.Notification.Error);
                    break;
                case Vib.Light:
                    TapticPlugin.TapticManager.Impact (TapticPlugin.Impact.Light);
                    break;
                case Vib.Medium:
                    TapticPlugin.TapticManager.Impact (TapticPlugin.Impact.Medium);
                    break;
                case Vib.Heavy:
                    TapticPlugin.TapticManager.Impact (TapticPlugin.Impact.Heavy);
                    break;
            }
#endif
        }
    }



    // █▀▀▀▀ █▀▀▀▄  ▀█▀  ▀▀█▀▀ ▄▀▀▀▄ █▀▀▀▄ 
    // █▄▄▄  █   █   █     █   █   █ █▄▄▄▀ 
    // █     █   █   █     █   █   █ █ ▀▄  
    // ▀▀▀▀▀ ▀▀▀▀   ▀▀▀    ▀    ▀▀▀  ▀   ▀

    ///<summary>asset-с обект унших</summary>
    public static T LoadAsset<T>(string path)
    {
#if UNITY_EDITOR
        return (T)(object)UnityEditor.AssetDatabase.LoadAssetAtPath(path, typeof(T));
#else
        return default(T);
#endif
    }
    ///<summary>тоглоом эхлэхэд GameView scale-г тааруулна</summary>
    public static void SetGameViewScale()
    {
#if UNITY_EDITOR
        System.Reflection.Assembly assembly = typeof(UnityEditor.EditorWindow).Assembly;
        System.Type type = assembly.GetType("UnityEditor.GameView");
        UnityEditor.EditorWindow v = UnityEditor.EditorWindow.GetWindow(type);
        var defScaleField = type.GetField("m_defaultScale", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        float defaultScale = 0.1f;
        var areaField = type.GetField("m_ZoomArea", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        var areaObj = areaField.GetValue(v);
        var scaleField = areaObj.GetType().GetField("m_Scale", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        scaleField.SetValue(areaObj, new Vector2(defaultScale, defaultScale));
#endif
    }
    ///<summary>нээлттэй scene-г хадгална</summary>
    public static void SaveOpenScenes()
    {
#if UNITY_EDITOR
        UnityEditor.SceneManagement.EditorSceneManager.SaveOpenScenes();
#endif
    }
    ///<summary>PlayerSettings-г бөглөнө</summary>
    public static void PlayerSettingsChange()
    {
#if UNITY_EDITOR
        UnityEditor.PlayerSettings.companyName = A.GC.companyName;
        UnityEditor.PlayerSettings.productName = A.GC.productName;
        int lastDotIdx = A.GC.companyWebSite.LastIndexOf('.');
        UnityEditor.PlayerSettings.applicationIdentifier =
            A.GC.companyWebSite.Substring(lastDotIdx + 1, A.GC.companyWebSite.Length - lastDotIdx - 1) + "." +
            A.GC.companyWebSite.Substring(0, lastDotIdx) + "." +
            System.Text.RegularExpressions.Regex.Replace(A.GC.productName, "\\s+", "").ToLower();
        UnityEditor.PlayerSettings.bundleVersion = A.GC.version;
        Texture2D[] icons = new Texture2D[] { A.GC.icon };
        UnityEditor.PlayerSettings.SetIconsForTargetGroup(UnityEditor.BuildTargetGroup.Unknown, icons);
        UnityEditor.PlayerSettings.SplashScreen.show = false;
        UnityEditor.PlayerSettings.SplashScreen.logos = null;
        Texture2D iphone = A.GC.iPhoneLaunchScreen, ipad = A.GC.iPadLaunchScreen;
        UnityEditor.PlayerSettings.iOS.SetiPhoneLaunchScreenType(UnityEditor.iOSLaunchScreenType.ImageAndBackgroundRelative);
        UnityEditor.PlayerSettings.iOS.SetLaunchScreenImage(iphone, UnityEditor.iOSLaunchScreenImageType.iPhonePortraitImage);
        UnityEditor.PlayerSettings.iOS.SetiPadLaunchScreenType(UnityEditor.iOSLaunchScreenType.ImageAndBackgroundRelative);
        UnityEditor.PlayerSettings.iOS.SetLaunchScreenImage(ipad, UnityEditor.iOSLaunchScreenImageType.iPadImage);
        UnityEditor.PlayerSettings.SplashScreen.show = true;
        if (iphone.NotNull())
            UnityEditor.PlayerSettings.SplashScreen.background = TexSpr(iphone);
#endif
    }



    // █▀▀▀▄  ▀█▀  █   █ █▀▀▀▀ █           ▄▀▀▀▄ ▄▀▀▀▄ █▄ ▄█ █▄ ▄█ █▀▀▀▀ █   █ ▀▀█▀▀ 
    // █▄▄▄▀   █    ▀▄▀  █▄▄▄  █           █     █   █ █ █ █ █ █ █ █▄▄▄  █▀▄ █   █   
    // █       █   ▄▀ ▀▄ █     █           █   ▄ █   █ █   █ █   █ █     █  ▀█   █   
    // ▀      ▀▀▀  ▀   ▀ ▀▀▀▀▀ ▀▀▀▀▀        ▀▀▀   ▀▀▀  ▀   ▀ ▀   ▀ ▀▀▀▀▀ ▀   ▀   ▀
    static string[] charDataArr = new string[] {
        "                                     ▀ ▀                     ▀▄▀                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              ",
        "▄▀▀▀▄ █▀▀▀▀ █▀▀▀▄ █▀▀▀▀  █▀█  █▀▀▀▀ █▀▀▀▀ █ █ █ ▄▀▀▀▄ █   █ █   █ █  ▄▀  █▀▀█ █▄ ▄█ █   █ ▄▀▀▀▄ ▄▀▀▀▄ █▀▀▀█ █▀▀▀▄ ▄▀▀▀▄ ▀▀█▀▀ █   █ █   █ ▄▀█▀▄ █   █ █  █  █   █ █ █ █ █ █ █ ▀█    █   █ █     ▀▀▀▀▄ █ ▄▀▄ ▄▀▀▀█        ▄▄▄▀                          ▀ ▀                     ▀▄▀                                                                                                                                                  ▄▀▀▀▄ █▀▀▀▄ ▄▀▀▀▄ █▀▀▀▄ █▀▀▀▀ █▀▀▀▀ ▄▀▀▀▄ █   █  ▀█▀    ▀█▀ █  ▄▀ █     █▄ ▄█ █   █ ▄▀▀▀▄ █▀▀▀▄ ▄▀▀▀▄ █▀▀▀▄ ▄▀▀▀▀ ▀▀█▀▀ █   █ █   █ █   █ █   █ █   █ ▀▀▀▀█       █               █         ▄▀▄       █       ▄      ▀   █     ▀█                                               █                                        ▀▄    ▄█   ▄▀▀▀▄ ▀▀▀█▀   ▄█  █▀▀▀▀  ▄▀▀  ▀▀▀▀█ ▄▀▀▀▄ ▄▀▀▀▄ ▄▀▀▀▄              ▄▄▄▀   █   ▄▀▀▀▄  █ █   ▄█▄▄ ██  ▄  ▄▀▄  ▄▀▀▄    ▄     ▄▀   ▀▄           ▄     █▀   ▀█   ▄      ▄▄    ▀█                   ▄  ▄▀▀   ▀▀▄    █    ▄▄    █ █    ▄▀   ▀▄   ▄▀▀▀▄       ",
        "█▄▄▄█ █▄▄▄  █▄▄▄▀ █      █ █  █▄▄▄  █▄▄▄  ▀▄█▄▀  ▄▄▄▀ █ ▄▀█ █ ▄▀█ █▄▀    █  █ █ █ █ █▄▄▄█ █   █ █▄▄▄█ █   █ █▄▄▄▀ █       █   ▀▄▄▄█  ▀▄▀  ▀▄█▄▀  ▀▄▀  █  █  ▀▄▄▄█ █ █ █ █ █ █  █▄▄  █▄  █ █▄▄▄   ▄▄▄█ █▄█ █ ▀▄▄▄█  ▀▀▀▄ █▄▄▄  █▀▀▀▄ █▀▀▀▀  █▀█  ▄▀▀▀▄ ▄▀▀▀▄ █ █ █ ▀▀▀▀▄ █  ▄█ █  ▄█ █  ▄▀  █▀▀█ █▄ ▄█ █   █ ▄▀▀▀▄ ▄▀▀▀▄ █▀▀▀█ █▀▀▀▄ ▄▀▀▀▀ ▀▀█▀▀ █   █ █   █ ▄▀█▀▄ ▀▄ ▄▀ █  █  █   █ █ █ █ █ █ █ ▀█    █   █ █     ▀▀▀▀▄ █ ▄▀▄  ▄▀▀█ █▄▄▄█ █▄▄▄▀ █     █   █ █▄▄▄  █▄▄▄  █     █▄▄▄█   █      █  █▄▀   █     █ █ █ █▀▄ █ █   █ █▄▄▄▀ █   █ █▄▄▄▀ ▀▄▄▄    █   █   █ █   █ █ ▄ █  ▀▄▀   ▀▄▀    ▄▀   ▀▀▀▄ █▄▄▄  ▄▀▀▀▀  ▄▄▄█ ▄▀▀▀▄  ▄█▄  ▄▀▀▀█ █▄▄▄    ▄      █   █ ▄▀   █   █▀▄▀█ █▄▀▀▄ ▄▀▀▀▄ █▀▀▀▄ ▄▀▀▀█ █▄▀▀▄ ▄▀▀▀▀ ▀▀█▀▀ █   █ █   █ █   █ ▀▄ ▄▀ ▀▄ ▄▀ ▀▀▀█▀    ▀    █      ▄▀   ▀▄  ▄▀ █  ▀▀▀▀▄ █▄▄▄    ▄▀  ▀▄▄▄▀ ▀▄▄▄█ █ ▄▀█ ▄▄▄▄▄ ▀▀▀▀▀ ▀       █    ▄▄ █ ▀█▀█▀ ▀▄█▄    ▄▀  ▀   ▀ ▀▄▀   ▀▄█▄▀  █       █        ▄▄█▄▄   █     █    ▀▄    ▀▀    ▀                  ▄▀  ▄▀       ▀▄   █    ▀▀    ▀ ▀  ▄▀       ▀▄    ▄▀       ",
        "█   █ █   █ █   █ █      █ █  █     █     █ █ █ ▄   █ █▀  █ █▀  █ █ ▀▄   █  █ █   █ █   █ █   █ █   █ █   █ █     █   ▄   █   ▄   █   █     █   ▄▀ ▀▄ █  █      █ █ █ █ █ █ █  █  █ █ █ █ █   █     █ █ █ █  ▄▀ █ ▄▀▀▀█ █   █ █▀▀▀▄ █      █ █  █▀▀▀  █▀▀▀  ▄▀█▀▄  ▀▀▀▄ █▄▀ █ █▄▀ █ █▀▀▄   █  █ █ ▀ █ █▀▀▀█ █   █ █▀▀▀█ █   █ █▀▀▀  █       █    ▀▀▀█  ▀▄▀  ▀▄█▄▀  ▄▀▄  █  █   ▀▀▀█ █ █ █ █ █ █  █▀▀▄ █▀▄ █ █▀▀▀▄  ▀▀▀█ █▀█ █  ▄▀▀█ █   █ █   █ █   ▄ █   █ █     █     █  ▀█ █   █   █   ▄  █  █ ▀▄  █     █   █ █  ▀█ █   █ █     █ ▀▄▀ █ ▀▄      █   █   █   █  █ █  █ █ █ ▄▀ ▀▄   █   ▄▀    ▄▀▀▀█ █   █ █     █   █ █▀▀▀▀   █    ▀▀▀█ █   █   █   ▄  █   █▀▄    █   █ █ █ █   █ █   █ █▀▀▀   ▀▀▀█ █      ▀▀▀▄   █ ▄ █   █ ▀▄ ▄▀ █ █ █  ▄▀▄    █    ▄▀           █    ▄▀   ▄   █ ▀▀▀█▀ ▄   █ █   █  █    █   █    ▄▀ █▀  █       ▀▀▀▀▀             █ █ █ ▀█▀█▀ ▄▄█▄▀ ▄▀ ▄▄       █ ▀▄▀ ▀ █ ▀  ▀▄     ▄▀          █     █     █      ▀▄  ▀█          ▀█    ▄▄   ▄▀     █       █    █    ██          ▀▄     ▄▀    ▀         ",
        "▀   ▀ ▀▀▀▀  ▀▀▀▀  ▀     █▀▀▀█ ▀▀▀▀▀ ▀▀▀▀▀ ▀ ▀ ▀  ▀▀▀  ▀   ▀ ▀   ▀ ▀   ▀ ▀   ▀ ▀   ▀ ▀   ▀  ▀▀▀   ▀▀▀  ▀   ▀ ▀      ▀▀▀    ▀    ▀▀▀    ▀     ▀   ▀   ▀ ▀▀▀▀█     ▀ ▀▀▀▀▀ ▀▀▀▀█  ▀▀▀  ▀▀  ▀ ▀▀▀▀  ▀▀▀▀  ▀  ▀  ▀   ▀  ▀▀▀▀  ▀▀▀  ▀▀▀▀  ▀     █▀▀▀█  ▀▀▀▀  ▀▀▀▀ ▀ ▀ ▀ ▀▀▀▀  ▀   ▀ ▀   ▀ ▀   ▀ ▀   ▀ ▀   ▀ ▀   ▀  ▀▀▀   ▀▀▀  ▀   ▀ ▀      ▀▀▀▀   ▀    ▀▀▀    ▀     ▀   ▀   ▀ ▀▀▀▀█     ▀ ▀▀▀▀▀ ▀▀▀▀█  ▀▀▀  ▀▀  ▀ ▀▀▀▀  ▀▀▀▀  ▀  ▀  ▀   ▀ ▀   ▀ ▀▀▀▀   ▀▀▀  ▀▀▀▀  ▀▀▀▀▀ ▀      ▀▀▀  ▀   ▀  ▀▀▀   ▀▀   ▀   ▀ ▀▀▀▀▀ ▀   ▀ ▀   ▀  ▀▀▀  ▀      ▀▀ ▀ ▀   ▀ ▀▀▀▀    ▀    ▀▀▀    ▀    ▀ ▀  ▀   ▀   ▀   ▀▀▀▀▀  ▀▀▀▀ ▀▀▀▀   ▀▀▀▀  ▀▀▀▀  ▀▀▀▀   ▀   ▀▀▀▀  ▀   ▀   ▀    ▀▀    ▀  ▀  ▀▀▀  ▀   ▀ ▀   ▀  ▀▀▀  ▀         ▀ ▀     ▀▀▀▀     ▀   ▀▀▀    ▀    ▀ ▀  ▀   ▀  ▀    ▀▀▀▀▀        ▀▀▀  ▀▀▀▀▀  ▀▀▀     ▀   ▀▀▀   ▀▀▀   ▀     ▀▀▀   ▀▀    ▀▀▀                      ▀    ▀▀▀   ▀ ▀    ▀      ▀▀        ▀▀ ▀          ▀   ▀    ▀▀▀▀▀         ▀▀   ▀▀          ▀           ▀     ▀▀           ▀▀   ▀▀     ▀                  ▀   ▀      ▀         ",
    };
    static string charDataStr = "АБВГДЕЁЖЗИЙКЛМНОӨПРСТУҮФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмноөпрстуүфхцчшщъыьэюяABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz`1234567890-=~!@#$%^&*()_+[]\\;',./{}|:\"<>? ";
    public static string GetComment(string str, int tabSize = 4)
    {
        string res = "";
        string[] arr = str.Split('\n');
        int startIdx = str.Contains("Ё") || str.Contains("Й") ? 0 : 1;
        foreach (string s in arr)
        {
            for (int i = startIdx; i < charDataArr.Length; i++)
            {
                res += "\n\t// ";
                for (int j = 0, k = 0; j < s.Length; j++)
                {
                    if (s[j] == '\t')
                    {
                        for (int it = 0, t = tabSize - k % tabSize; it < t; it++)
                        {
                            res += charDataArr[i].Substring(charDataStr.IndexOf(' ') * 6, 6);
                            k++;
                        }
                    }
                    else if (charDataStr.Contains("" + s[j]))
                    {
                        res += charDataArr[i].Substring(charDataStr.IndexOf(s[j]) * 6, 6);
                        k++;
                    }
                }
            }
        }
        return res;
    }
}
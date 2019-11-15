// #define VOODOO_LEVEL
// #define VOODOO_NO_LEVEL

using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
#if VOODOO_NO_LEVEL || VOODOO_LEVEL
using Voodoo.Sauce.Internal;
#endif

public enum GameState { Starting, Playing, Pause, LevelCompleted, GameOver, Settings, Shifting }
public enum Data { IsWin, Score, Best, Level, Diamond, Coin, PlayerName, IsVibration, PlayerSkinIdx, LevelIdx }
public enum FollowType { None, Hud, HudPointer, HudPointerOff, Follow, FollowPointer, FollowPointerOff }

public class GameController : Singleton<GameController>
{
    public static GameState State;
    public static float Score, GameTime;
    public static float Best { get { return A.GetData<float>(Data.Best); } }
    public static bool IsWin { get { return A.GetData<bool>(Data.IsWin); } }
    public static int Level, Diamond, Coin, Kills, Life, PlayerSkinIdx;
    public static string PlayerName;
    public static bool IsVibration;
    // Data-д хадгалж болох төрөл: bool, int, float, string, Vector2, Vector2Int, Vector3, Vector3Int, Vector4, Color
    public static Dictionary<Data, object> Datas = new Dictionary<Data, object>() { //
        { Data.IsWin, true }, //
        { Data.Score, 0f }, //
        { Data.Best, 0f }, //
        { Data.Level, 1 }, //
        { Data.Diamond, 0 }, //
        { Data.Coin, 0 }, //
        { Data.PlayerName, "Player" }, //
        { Data.IsVibration, true }, //
        { Data.PlayerSkinIdx, 0 }, //
        { Data.LevelIdx, -1 }, //
    };
    [Header("UI")]
    public bool isStartButton = false;
    public bool isNewBest = true;
    public bool isPauseButton = false;
    [Header("Game")]
    public float gameTime = 120;
    public float gameOverWaitTime = 0;
    public float levelCompletedWaitTime = 0;
    [Header("LeaderBoard")]
    public bool isLbhBgColConst = true;
    public Color LbhPlayerCol = new Color32(255, 255, 255, 100);
    public Color LbhBotCol = new Color32(0, 0, 0, 100);
    public bool isLbgNameUseCol = true;
    public Color LbgPlayerCol = new Color32(255, 255, 255, 100);
    public Color LbgBotCol = Color.clear;
    [Header("Character")]
    public int showFollowLive = 3;
    public FollowType followType;
    [DrawIf("followType", typeof(int), (int)FollowType.None, ComparisonType.NotEqual)]
    public bool isNameUppercase = false;
    public Color playerCol = new Color32(244, 67, 54, 255);
    public List<Color> botCols = new List<Color>() {
        new Color32 (233, 30, 99, 255),
        new Color32 (156, 39, 176, 255),
        new Color32 (103, 58, 183, 255),
        new Color32 (63, 81, 181, 255),
        new Color32 (33, 150, 243, 255),
        new Color32 (3, 169, 244, 255),
        new Color32 (0, 188, 212, 255),
        new Color32 (0, 150, 136, 255),
        new Color32 (76, 175, 80, 255),
        new Color32 (139, 195, 74, 255),
        new Color32 (205, 220, 57, 255),
        new Color32 (255, 235, 59, 255),
        new Color32 (255, 193, 7, 255),
        new Color32 (255, 152, 0, 255),
        new Color32 (255, 87, 34, 255),
        new Color32 (121, 85, 72, 255),
        new Color32 (158, 158, 158, 255),
        new Color32 (96, 125, 139, 255),
    };
    [Header("Player Settings Change")]
    public string companyName = "Black Candy";
    public string companyWebSite = "blackcandy.io";
    public string productName = "Game Name";
    public string version = "1.0";
    public Texture2D icon;
    public Texture2D iPhoneLaunchScreen;
    public Texture2D iPadLaunchScreen;
    [Header("Screenshot")]
    public string screenshotName;
    void Awake()
    {
        Application.targetFrameRate = 60;
        A.SetGameViewScale();
    }
    void Start()
    {
        State = GameState.Starting;
        // update data
        GameTime = gameTime;
        Score = A.GetData<float>(Data.Score);
        Coin = A.GetData<int>(Data.Coin);
        Diamond = A.GetData<int>(Data.Diamond);
        Level = A.GetData<int>(Data.Level);
        IsVibration = A.GetData<bool>(Data.IsVibration);
        PlayerSkinIdx = A.GetData<int>(Data.PlayerSkinIdx);
        Kills = 0;
        Life = 1;
        // show ui
        A.CC.HudScore(Score);
        A.CC.HudBest(Best);
        A.CC.HudCoin(Coin);
        A.CC.HudDiamond(Diamond);
        A.CC.HudKills(Kills);
        A.CC.HudLife(Life);
        A.CC.HudLevelBar(Level);
        A.LS.Init();
        VoodooStart();
    }
    void Update()
    {
        if (A.IsStarting && !isStartButton && Input.GetMouseButtonDown(0))
            Play();
        if (A.IsPause && !isPauseButton && Input.GetMouseButtonDown(0))
            A.CC.Resume();
    }
    public void Play()
    {
        State = GameState.Playing;
        A.Player.MouseButtonDown();
        A.CC.ShowHud();
    }
    public void GameOver(bool isComplete = true)
    {
        GameOver(isComplete, LeaderBoardData.GetDatas());
    }
    public void GameOver(bool isComplete, List<LeaderBoardData> datas)
    {
        if (A.IsPlaying)
        {
            VoodooFinish(false);
            LeaderBoardData.RemoveNames();
            State = GameState.GameOver;
            A.SetData(Data.IsWin, false);
            A.SetData(Data.Score, 0f);
            if (Best < Score)
            {
                A.SetData(Data.Best, Score);
                if (isNewBest) A.CC.NewBest(gameOverWaitTime, Score, Level, datas);
                else A.CC.GameOver(gameOverWaitTime, Score, Best, Level, datas, isComplete);
            }
            else A.CC.GameOver(gameOverWaitTime, Score, Best, Level, datas, isComplete);
        }
    }
    public void LevelCompleted()
    {
        LevelCompleted(LeaderBoardData.GetDatas());
    }
    public void LevelCompleted(List<LeaderBoardData> datas)
    {
        if (A.IsPlaying)
        {
            VoodooFinish();
            State = GameState.LevelCompleted;
            A.SetData(Data.IsWin, true);
            A.SetData(Data.Score, Score);
            A.SetData(Data.Level, Level + 1);
            A.CC.LevelCompleted(levelCompletedWaitTime, Level, datas);
        }
    }
    void OnApplicationQuit()
    {
        Time.timeScale = 1;
        A.SetData(Data.Score, 0f);
    }
    public void VarTimeSet<T, V>(T obj, string name, V value, float time) { StartCoroutine(VarTimeSetCor(obj, name, value, time)); }
    IEnumerator VarTimeSetCor<T, V>(T obj, string name, V value, float time)
    {
        FieldInfo f = obj.GetType().GetField(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        f.SetValue(obj, new VarTime<V>(((VarTime<V>)f.GetValue(obj)).value, Time.time));
        yield return new WaitForSeconds(time + 0.001f);
        if (((VarTime<V>)f.GetValue(obj)).time + time <= Time.time)
            f.SetValue(obj, new VarTime<V>(value, Time.time));
    }
    void VoodooStart()
    {
#if VOODOO_LEVEL
        TinySauce.OnGameStarted("" + Level);
#elif VOODOO_NO_LEVEL
        TinySauce.OnGameStarted("game");
#endif
    }
    void VoodooFinish(bool isLevelComplete = true)
    {
#if VOODOO_LEVEL
            TinySauce.OnGameFinished("" + Level, isLevelComplete, Score);
#elif VOODOO_NO_LEVEL
            TinySauce.OnGameFinished("game", isLevelComplete, Score);
#endif
    }
}
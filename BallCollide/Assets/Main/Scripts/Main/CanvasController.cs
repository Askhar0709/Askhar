using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum TutorialType { Line, Infinity, TapToPlay, InputName }

public class CanvasController : Singleton<CanvasController>
{
    [Header("Panels")]
    public UIData menu;
    public UIData hud;
    public UIData gameOverComplete;
    public UIData gameOverUnCompleted;
    public UIData newBest;
    public UIData levelCompleted;
    public UIData pause;
    [Header("Hud items")]
    public UIData hudTime;
    public UIData hudKills;
    public UIData hudScore;
    public UIData hudBest;
    public UIData hudDiamond;
    public UIData hudCoin;
    public UIData hudLife;
    public UIData hudLevelBar;
    public UIData hudLeaderBoard;
    GameState tmpGs;
    InputField playerNameInp;
    float time = 0;
    void Start()
    {
        Show(menu);
        Hide(hud);
        Hide(gameOverComplete);
        Hide(gameOverUnCompleted);
        Hide(newBest);
        Hide(levelCompleted);
        Hide(pause);
        ReadPlayerName();
    }
    private void Update()
    {
        if (A.IsPlaying)
        {
            if (hudLeaderBoard.NotNull())
                HudLeaderBoard(LeaderBoardData.GetDatas());
            if (hudTime.NotNull())
            {
                time += Time.deltaTime;
                if (GameController.GameTime - time > 0)
                {
                    HudTime(GameController.GameTime - time);
                }
                else
                {
                    HudTime(0);
                    A.GC.GameOver();
                }
            }
        }
    }
    void ReadPlayerName()
    {
        GameObject playerNameGo = GameObject.Find("PlayerName");
        if (playerNameGo.NotNull())
        {
            playerNameInp = playerNameGo.GC<InputField>();
            if (playerNameInp.NotNull())
                playerNameInp.text = GameController.PlayerName = A.GetData<string>(Data.PlayerName);
        }
    }
    public void HudScore(float score) { HudData(hudScore, score); }
    public void HudBest(float best) { HudData(hudBest, best); }
    public void HudCoin(int coin) { HudData(hudCoin, coin); }
    public void HudDiamond(int diamond) { HudData(hudDiamond, diamond); }
    public void HudTime(float time) { HudData(hudTime, time); }
    public void HudKills(int kills) { HudData(hudKills, kills); }
    public void HudLife(int life) { HudData(hudLife, life); }
    public void HudLevelBar(int level = 1, float progressScore = 0, float progressBest = 0, string progressText = "", int stage = 1)
    {
        if (hudLevelBar.NotNull())
            hudLevelBar.DataLevelBar(level, progressScore, progressBest, progressText, stage);
    }
    public void HudLeaderBoard(List<LeaderBoardData> datas = null)
    {
        if (hudLeaderBoard.NotNull())
            hudLeaderBoard.DataLeaderBoard(datas);
    }
    public void ShowHud()
    {
        Hide(menu);
        Show(hud);
    }
    public void GameOver(float time = 0, float score = 0, float best = 0, int level = 1, List<LeaderBoardData> datas = null, bool isComplete = true) { StartCoroutine(GameOverCor(time, score, best, level, datas, isComplete)); }
    IEnumerator GameOverCor(float time, float score, float best, int level, List<LeaderBoardData> datas, bool isComplete)
    {
        yield return new WaitForSeconds(time);
        if (isComplete)
        {
            if (gameOverComplete.NotNull())
            {
                Hide(hud);
                Show(gameOverComplete);
                gameOverComplete.DataGameOver(score, best, level, datas);
            }
        }
        else
        {
            if (gameOverUnCompleted.NotNull())
            {
                Hide(hud);
                Show(gameOverUnCompleted);
                gameOverUnCompleted.DataGameOver(score, best, level, datas);
            }
        }
    }
    public void NewBest(float time = 0, float score = 0, int level = 1, List<LeaderBoardData> datas = null) { StartCoroutine(NewBestCor(time, score, level, datas)); }
    public IEnumerator NewBestCor(float time, float score, int level, List<LeaderBoardData> datas)
    {
        yield return new WaitForSeconds(time);
        if (newBest.NotNull())
        {
            Hide(hud);
            Show(newBest);
            newBest.DataNewBest(score, level, datas);
        }
    }
    public void LevelCompleted(float time = 0, int level = 1, List<LeaderBoardData> datas = null) { StartCoroutine(LevelCompletedCor(time, level, datas)); }
    IEnumerator LevelCompletedCor(float time, int level, List<LeaderBoardData> datas)
    {
        yield return new WaitForSeconds(time);
        if (levelCompleted.NotNull())
        {
            Hide(hud);
            Show(levelCompleted);
            levelCompleted.DataLevelCompleted(level, datas);
        }
    }
    public void Pause(float time = 0) { StartCoroutine(PauseCor(time)); }
    IEnumerator PauseCor(float time)
    {
        yield return new WaitForSeconds(time);
        if (pause.NotNull())
        {
            Show(pause);
            Time.timeScale = 0;
            tmpGs = GameController.State;
            GameController.State = GameState.Pause;
        }
    }
    public void Resume(float time = 0) { StartCoroutine(ResumeCor(time)); }
    IEnumerator ResumeCor(float time)
    {
        yield return new WaitForSeconds(time);
        if (pause.NotNull())
        {
            Hide(pause);
            Time.timeScale = 1;
            GameController.State = tmpGs;
        }
    }
    public void Replay(float time = 0) { StartCoroutine(ReplayCor(time)); }
    IEnumerator ReplayCor(float time)
    {
        yield return new WaitForSeconds(time);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void Play()
    {
        if (playerNameInp.NotNull()) GameController.PlayerName = playerNameInp.text;
        A.SetData(Data.PlayerName, GameController.PlayerName);
        LeaderBoardData.SetPlayerData(GameController.PlayerName);
        A.GC.Play();
    }
    void Hide(UIData uiData) { if (uiData.NotNull()) uiData.Hide(); }
    void Show(UIData uiData) { if (uiData.NotNull()) uiData.Show(); }
    void HudData(UIData uiData, float data) { if (uiData.NotNull()) uiData.DataHudData(data); }
}
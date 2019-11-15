using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EditorMenuItem : Editor {
    const string ui = "UI/",
        menu = "GameObject/UI/",
        simple = "Simple",
        slash = "/",
        menuUI = menu + ui,
        uiUI = ui + ui;
    // █   █ █   █ █▀▀▀▄ 
    // █▄▄▄█ █   █ █   █ 
    // █   █ █   █ █   █ 
    // ▀   ▀  ▀▀▀  ▀▀▀▀
    const string hud = "Hud",
        menuHud = menu + hud + slash,
        uiHud = ui + hud + slash + hud;
    [MenuItem (menuHud + hud)]
    public static void Hud () { Create (uiHud, hud); }

    [MenuItem (menuHud + "Score")]
    public static void HudScore () { CreatePrefab (uiHud + "Score", "Score"); }

    [MenuItem (menuHud + "Best")]
    public static void HudBest () { CreatePrefab (uiHud + "Best", "Best"); }

    [MenuItem (menuHud + "Coin")]
    public static void HudCoin () { CreatePrefab (uiHud + "Coin", "Coin"); }

    [MenuItem (menuHud + "Diamond")]
    public static void HudDiamond () { CreatePrefab (uiHud + "Diamond", "Diamond"); }

    [MenuItem (menuHud + "Time")]
    public static void HudTime () { CreatePrefab (uiHud + "Time", "Time"); }

    [MenuItem (menuHud + "Kills")]
    public static void HudKills () { CreatePrefab (uiHud + "Kills", "Kills"); }

    [MenuItem (menuHud + leaderBoard)]
    public static void HudLeaderBoard () { CreatePrefab (uiLeaderBoard + "Hud", leaderBoard); }

    [MenuItem (menuHud + levelBar)]
    public static void HudLevelBar () { CreatePrefab (uiLevelBar + simple, levelBar); }
    // ▄▀▀▀▄ ▄▀▀▀▄ █▄ ▄█ █▀▀▀▀       ▄▀▀▀▄ █   █ █▀▀▀▀ █▀▀▀▄ 
    // █     █▄▄▄█ █ █ █ █▄▄▄        █   █ █   █ █▄▄▄  █▄▄▄▀ 
    // █  ▀█ █   █ █   █ █           █   █  █ █  █     █ ▀▄  
    //  ▀▀▀  ▀   ▀ ▀   ▀ ▀▀▀▀▀        ▀▀▀    ▀   ▀▀▀▀▀ ▀   ▀
    const string gameOver = "GameOver",
        menuGameOver = menu + gameOver + slash,
        uiGameOver = ui + gameOver + slash + gameOver;
    [MenuItem (menuGameOver + "LeaderBoardButton")]
    public static void GameOverLeaderBoardButton () { CreatePrefab (uiGameOver + "LeaderBoardButton", gameOver); }

    [MenuItem (menuGameOver + "ScoreBestButton")]
    public static void GameOverScoreBestButton () { CreatePrefab (uiGameOver + "ScoreBestButton", gameOver); }

    [MenuItem (menuGameOver + "Button")]
    public static void GameOverButton () { CreatePrefab (uiGameOver + "Button", gameOver); }
    // █   █ █▀▀▀▀ █   █       █▀▀▀▄ █▀▀▀▀ ▄▀▀▀▀ ▀▀█▀▀ 
    // █▀▄ █ █▄▄▄  █ ▄ █       █▄▄▄▀ █▄▄▄  ▀▄▄▄    █   
    // █  ▀█ █     █ █ █       █   █ █         █   █   
    // ▀   ▀ ▀▀▀▀▀  ▀ ▀        ▀▀▀▀  ▀▀▀▀▀ ▀▀▀▀    ▀
    const string newBest = "NewBest",
        menuNewBest = menu + newBest + slash,
        uiNewBest = ui + newBest + slash + newBest;
    [MenuItem (menuNewBest + "ScoreButton")]
    public static void NewBestScoreButton () { CreatePrefab (uiNewBest + "ScoreButton", newBest); }
    // █     █▀▀▀▀ █   █ █▀▀▀▀ █           ▄▀▀▀▄ ▄▀▀▀▄ █▄ ▄█ █▀▀▀▄ █     █▀▀▀▀ ▀▀█▀▀ █▀▀▀▀ █▀▀▀▄ 
    // █     █▄▄▄  █   █ █▄▄▄  █           █     █   █ █ █ █ █▄▄▄▀ █     █▄▄▄    █   █▄▄▄  █   █ 
    // █     █      █ █  █     █           █   ▄ █   █ █   █ █     █     █       █   █     █   █ 
    // ▀▀▀▀▀ ▀▀▀▀▀   ▀   ▀▀▀▀▀ ▀▀▀▀▀        ▀▀▀   ▀▀▀  ▀   ▀ ▀     ▀▀▀▀▀ ▀▀▀▀▀   ▀   ▀▀▀▀▀ ▀▀▀▀
    const string levelCompleted = "LevelCompleted",
        menuLevelCompleted = menu + levelCompleted + slash,
        uiLevelCompleted = ui + levelCompleted + slash + levelCompleted;
    [MenuItem (menuLevelCompleted + simple)]
    public static void LevelCompletedSimple () { CreatePrefab (uiLevelCompleted + simple, levelCompleted); }

    [MenuItem (menuLevelCompleted + "Button")]
    public static void LevelCompletedButton () { CreatePrefab (uiLevelCompleted + "Button", levelCompleted); }

    [MenuItem (menuLevelCompleted + "LeaderBoardButton")]
    public static void LevelCompletedLeaderBoardButton () { CreatePrefab (uiLevelCompleted + "LeaderBoardButton", levelCompleted); }
    // █▀▀▀▄ ▄▀▀▀▄ █   █ ▄▀▀▀▀ █▀▀▀▀ 
    // █▄▄▄▀ █▄▄▄█ █   █ ▀▄▄▄  █▄▄▄  
    // █     █   █ █   █     █ █     
    // ▀     ▀   ▀  ▀▀▀  ▀▀▀▀  ▀▀▀▀▀
    const string pause = "Pause",
        menuPause = menu + pause + slash,
        uiPause = ui + pause + slash + pause;
    [MenuItem (menuPause + "TapToPlay")]
    public static void PauseTapToPlay () { CreatePrefab (uiPause + "TapToPlay", pause); }

    [MenuItem (menuPause + "Button")]
    public static void PauseButton () { CreatePrefab (uiPause + "Button", pause); }
    // ▀▀█▀▀ █   █ ▀▀█▀▀ ▄▀▀▀▄ █▀▀▀▄  ▀█▀  ▄▀▀▀▄ █     
    //   █   █   █   █   █   █ █▄▄▄▀   █   █▄▄▄█ █     
    //   █   █   █   █   █   █ █ ▀▄    █   █   █ █     
    //   ▀    ▀▀▀    ▀    ▀▀▀  ▀   ▀  ▀▀▀  ▀   ▀ ▀▀▀▀▀
    const string tutorial = "Tutorial",
        menuTutorial = menu + tutorial + slash,
        uiTutorial = ui + tutorial + slash + tutorial;
    [MenuItem (menuTutorial + "Line")]
    public static void TutorialLine () { CreatePrefab (uiTutorial + "Line", tutorial); }

    [MenuItem (menuTutorial + "Infinity")]
    public static void TutorialInfinity () { CreatePrefab (uiTutorial + "Infinity", tutorial); }

    [MenuItem (menuTutorial + "TapToPlay")]
    public static void TutorialTapToPlay () { CreatePrefab (uiTutorial + "TapToPlay", tutorial); }

    [MenuItem (menuTutorial + "InputName")]
    public static void TutorialInputName () { CreatePrefab (uiTutorial + "InputName", tutorial); }
    // ▄▀▀▀▄ ▄▀▀▀▄ █▄ ▄█ █▀▀▀▀       ▄▀▀▀▀ █▀▀▀▀ ▀▀█▀▀ ▀▀█▀▀ █   █ ▄▀▀▀▄ ▄▀▀▀▀ 
    // █     █▄▄▄█ █ █ █ █▄▄▄        ▀▄▄▄  █▄▄▄    █     █   █▀▄ █ █     ▀▄▄▄  
    // █  ▀█ █   █ █   █ █               █ █       █     █   █  ▀█ █  ▀█     █ 
    //  ▀▀▀  ▀   ▀ ▀   ▀ ▀▀▀▀▀       ▀▀▀▀  ▀▀▀▀▀   ▀     ▀   ▀   ▀  ▀▀▀  ▀▀▀▀
    const string gameSettings = "GameSettings",
        menuGameSettings = menu + gameSettings + slash,
        uiGameSettings = ui + gameSettings + slash + gameSettings;
    [MenuItem (menuGameSettings + gameSettings)]
    public static void GameSettings () { Create (uiGameSettings, gameSettings); }

    [MenuItem (menuGameSettings + "ColorPicker")]
    public static void GameSettingsColorPicker () { CreatePrefab (uiGameSettings + "ColorPicker", "ColorPicker"); }

    [MenuItem (menuGameSettings + "DropDown")]
    public static void GameSettingsDropDown () { CreatePrefab (uiGameSettings + "DropDown", "DropDown"); }

    [MenuItem (menuGameSettings + "InputField")]
    public static void GameSettingsInputField () { CreatePrefab (uiGameSettings + "InputField", "InputField"); }

    [MenuItem (menuGameSettings + "Slider")]
    public static void GameSettingsSlider () { CreatePrefab (uiGameSettings + "Slider", "Slider"); }

    [MenuItem (menuGameSettings + "Toggle")]
    public static void GameSettingsToggle () { CreatePrefab (uiGameSettings + "Toggle", "Toggle"); }

    [MenuItem (menuGameSettings + "Vector")]
    public static void GameSettingsVector () { CreatePrefab (uiGameSettings + "Vector", "Vector"); }
    // █     █▀▀▀▀ ▄▀▀▀▄ █▀▀▀▄ █▀▀▀▀ █▀▀▀▄       █▀▀▀▄ ▄▀▀▀▄ ▄▀▀▀▄ █▀▀▀▄ █▀▀▀▄ 
    // █     █▄▄▄  █▄▄▄█ █   █ █▄▄▄  █▄▄▄▀       █▄▄▄▀ █   █ █▄▄▄█ █▄▄▄▀ █   █ 
    // █     █     █   █ █   █ █     █ ▀▄        █   █ █   █ █   █ █ ▀▄  █   █ 
    // ▀▀▀▀▀ ▀▀▀▀▀ ▀   ▀ ▀▀▀▀  ▀▀▀▀▀ ▀   ▀       ▀▀▀▀   ▀▀▀  ▀   ▀ ▀   ▀ ▀▀▀▀
    const string leaderBoard = "LeaderBoard",
        menuLeaderBoard = menu + leaderBoard + slash,
        uiLeaderBoard = ui + leaderBoard + slash + leaderBoard;
    [MenuItem (menuLeaderBoard + "GameOver")]
    public static void LeaderBoardGameOver () { CreatePrefab (uiLeaderBoard + "GameOver", leaderBoard); }

    [MenuItem (menuLeaderBoard + "Hud")]
    public static void LeaderBoardHud () { CreatePrefab (uiLeaderBoard + "Hud", leaderBoard); }
    // █     █▀▀▀▀ █   █ █▀▀▀▀ █           █▀▀▀▄ ▄▀▀▀▄ █▀▀▀▄ 
    // █     █▄▄▄  █   █ █▄▄▄  █           █▄▄▄▀ █▄▄▄█ █▄▄▄▀ 
    // █     █      █ █  █     █           █   █ █   █ █ ▀▄  
    // ▀▀▀▀▀ ▀▀▀▀▀   ▀   ▀▀▀▀▀ ▀▀▀▀▀       ▀▀▀▀  ▀   ▀ ▀   ▀
    const string levelBar = "LevelBar",
        menuLevelBar = menu + levelBar + slash,
        uiLevelBar = ui + levelBar + slash + levelBar;
    [MenuItem (menuLevelBar + simple)]
    public static void LevelBarSimple () { CreatePrefab (uiLevelBar + simple, levelBar); }

    [MenuItem (menuLevelBar + "Text")]
    public static void LevelBarText () { CreatePrefab (uiLevelBar + "Text", levelBar); }

    [MenuItem (menuLevelBar + "Image")]
    public static void LevelBarImage () { CreatePrefab (uiLevelBar + "Image", levelBar); }

    [MenuItem (menuLevelBar + "ImagePointer")]
    public static void LevelBarImagePointer () { CreatePrefab (uiLevelBar + "ImagePointer", levelBar); }

    [MenuItem (menuLevelBar + "Flag")]
    public static void LevelBarFlag () { CreatePrefab (uiLevelBar + "Flag", levelBar); }

    [MenuItem (menuLevelBar + "FlagPointer")]
    public static void LevelBarFlagPointer () { CreatePrefab (uiLevelBar + "FlagPointer", levelBar); }

    [MenuItem (menuLevelBar + "Stage")]
    public static void LevelBarStage () { CreatePrefab (uiLevelBar + "Stage", levelBar); }

    [MenuItem (menuLevelBar + "StageText")]
    public static void LevelBarStageText () { CreatePrefab (uiLevelBar + "StageText", levelBar); }
    // ▄▀▀▀▄ ▄▀▀▀▄ █   █ █   █ ▄▀▀▀▄ ▄▀▀▀▀       ▄▀▀▀▄ ▄▀▀▀▄ █   █ ▀▀█▀▀ █▀▀▀▄ ▄▀▀▀▄ █     █     █▀▀▀▀ █▀▀▀▄ 
    // █     █▄▄▄█ █▀▄ █ █   █ █▄▄▄█ ▀▄▄▄        █     █   █ █▀▄ █   █   █▄▄▄▀ █   █ █     █     █▄▄▄  █▄▄▄▀ 
    // █   ▄ █   █ █  ▀█  █ █  █   █     █       █   ▄ █   █ █  ▀█   █   █ ▀▄  █   █ █     █     █     █ ▀▄  
    //  ▀▀▀  ▀   ▀ ▀   ▀   ▀   ▀   ▀ ▀▀▀▀         ▀▀▀   ▀▀▀  ▀   ▀   ▀   ▀   ▀  ▀▀▀  ▀▀▀▀▀ ▀▀▀▀▀ ▀▀▀▀▀ ▀   ▀
    const string canvasController = "CanvasController",
        menuCanvasController = menu + canvasController + slash,
        uiCanvasController = ui + canvasController + slash + canvasController;
    [MenuItem (menuCanvasController + simple)]
    public static void CanvasControllerSimple () { Create (uiCanvasController + simple, canvasController); }

    [MenuItem (menuCanvasController + "IO")]
    public static void CanvasControllerIO () { Create (uiCanvasController + "IO", canvasController); }
    // ▄▀▀▀▄ ▄▀▀▀▄ █   █ █▀▀▀▀ █▀▀▀▀ ▀▀█▀▀ ▀▀█▀▀  ▀█▀  
    // █     █   █ █▀▄ █ █▄▄▄  █▄▄▄    █     █     █   
    // █   ▄ █   █ █  ▀█ █     █       █     █     █   
    //  ▀▀▀   ▀▀▀  ▀   ▀ ▀     ▀▀▀▀▀   ▀     ▀    ▀▀▀
    const string confetti = "Confetti",
        menuConfetti = menu + confetti + slash,
        uiConfetti = ui + confetti + slash + confetti;
    [MenuItem (menuConfetti + simple)]
    public static void ConfettiSimple () { CreatePrefab (uiConfetti + simple, confetti); }

    [MenuItem (menuConfetti + "LeftRight")]
    public static void ConfettiLeftRight () { CreatePrefab (uiConfetti + "LeftRight", confetti); }

    [MenuItem (menuConfetti + "UpDown")]
    public static void ConfettiUpDown () { CreatePrefab (uiConfetti + "UpDown", confetti); }
    // █   █  ▀█▀  
    // █   █   █   
    // █   █   █   
    //  ▀▀▀   ▀▀▀
    [MenuItem (menuUI + "Menu")]
    public static void Menu () { Create (uiUI + "Menu", "Menu"); }

    [MenuItem (menuUI + "Title")]
    public static void Title () { CreatePrefab (uiUI + "Title", "Title"); }

    [MenuItem (menuUI + "FpsCounter")]
    public static void FpsCounter () { CreatePrefab (uiUI + "FpsCounter", "FpsCounter"); }

    [MenuItem (menuUI + "ButtonPlay")]
    public static void ButtonPlay () { CreatePrefab (uiUI + "ButtonPlay", "Button"); }

    [MenuItem (menuUI + "ButtonReplay")]
    public static void ButtonReplay () { CreatePrefab (uiUI + "ButtonReplay", "Button"); }

    [MenuItem (menuUI + "ButtonNext")]
    public static void ButtonNext () { CreatePrefab (uiUI + "ButtonNext", "Button"); }

    [MenuItem (menuUI + "ExceptionHandler")]
    public static void ExceptionHandler () { CreatePrefab ("ExceptionHandler", "ExceptionHandler"); }
    public static GameObject CreatePrefab (string path, string name) {
        GameObject go = (GameObject) PrefabUtility.InstantiatePrefab (Resources.Load<GameObject> (path));
        go.name = name;
        go.transform.SetParent (UnityEditor.Selection.activeGameObject.transform, false);
        return go;
    }
    public static GameObject Create (string path, string name) {
        GameObject go = Instantiate (Resources.Load<GameObject> (path));
        go.name = name;
        go.transform.SetParent (UnityEditor.Selection.activeGameObject.transform, false);
        return go;
    }
}
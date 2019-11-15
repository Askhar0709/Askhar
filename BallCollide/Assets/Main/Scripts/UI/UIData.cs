using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIData : MonoBehaviour {
    public virtual void DataGameOver (float score, float best, int level, List<LeaderBoardData> datas) { }
    public virtual void DataLevelBar (int level, float progressScore, float progressBest, string progressText, int stage) { }
    public virtual void DataLevelCompleted (int level, List<LeaderBoardData> datas) { }
    public virtual void DataNewBest (float score, int level, List<LeaderBoardData> datas) { }
    public virtual void DataLeaderBoard (List<LeaderBoardData> datas) { }
    public virtual void DataPause () { }
    public virtual void DataHudData (float data) { }
    public virtual void LeaderBoardUIDataActive (int idx, bool active, int place, LeaderBoardData data) { }
    public virtual void SkinUnlocked (Image skinImg) { }
    public virtual void CharacterSkin (int playerSkinIdx, params bool[] types) { }
    public void Replay (float time = 0) { A.CC.Replay (time); }
    public void Play () { A.CC.Play (); }
    public void Resume (float time = 0) { A.CC.Resume (time); }
}
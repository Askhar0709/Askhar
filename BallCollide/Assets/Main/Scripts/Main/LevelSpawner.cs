using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : Singleton<LevelSpawner>
{
    public List<Level> levels;
    public void Init()
    {
        LoadLevel();
        LeaderBoardData.SetDatas();
    }
    public void LoadLevel()
    {
        //
    }
    int GetLevelIdx()
    {
        int res = GameController.Level - 1;
        if (GameController.Level > levels.Count)
        {
            res = A.GetData<int>(Data.LevelIdx);
            if (res < 0 || GameController.IsWin)
            {
                res = A.RndIdx(levels.Count, res);
                A.SetData(Data.LevelIdx, res);
            }
        }
        return res;
    }
}
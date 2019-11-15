using System.Collections.Generic;
using UnityEngine.UI;

public class LevelCompleted : UIData
{
    public UIData level;
    public UIData leaderBoard;
    public float time = 1;
    public override void DataLevelCompleted(int level, List<LeaderBoardData> datas)
    {
        if (this.level.NotNull())
            this.level.DataHudData(level);
        if (leaderBoard.NotNull())
            leaderBoard.DataLeaderBoard(datas);
        if (time > 0)
            Replay(time);
    }
}
using System.Collections.Generic;
using UnityEngine.UI;

public class GameOver : UIData
{
    public UIData score;
    public UIData best;
    public UIData leaderBoard;
    public override void DataGameOver(float score, float best, int level, List<LeaderBoardData> datas)
    {
        if (this.score.NotNull())
            this.score.DataHudData(score);
        if (this.best.NotNull())
            this.best.DataHudData(best);
        if (leaderBoard.NotNull())
            leaderBoard.DataLeaderBoard(datas);
    }
}
using System.Collections.Generic;
using UnityEngine.UI;

public class NewBest : UIData
{
    public UIData score;
    public override void DataNewBest(float score, int level, List<LeaderBoardData> datas)
    {
        if (this.score.NotNull())
            this.score.DataHudData(score);
    }
}
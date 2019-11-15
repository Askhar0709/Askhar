using UnityEngine;
using UnityEngine.UI;

public class HudTime : UIData {
    public Text timeTxt;
    public override void DataHudData (float data) {
        if (timeTxt.NotNull ())
            timeTxt.text = A.TimeStr (data);
    }
}
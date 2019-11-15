using UnityEngine.UI;

public class HudData : UIData {
    public Text dataTxt;
    public string format = "(data)";
    public override void DataHudData (float data) {
        if (dataTxt.NotNull ())
            dataTxt.text = A.Format (format, "(data)", data.ToString ());
    }
}
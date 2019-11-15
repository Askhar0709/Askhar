using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class UIChange : MonoBehaviour
{
    [Range(0f, 1f)]
    public float alpha = 1;
    public Color color, color2;
    public Font font;
    public TMP_FontAsset tmpFont;
    List<TextMeshPro> tmpLis = new List<TextMeshPro>();
    List<TextMesh> tmLis = new List<TextMesh>();
    List<Text> txtLis = new List<Text>();
    List<Image> imgLis = new List<Image>();
    List<float> tmpALis = new List<float>();
    List<float> tmALis = new List<float>();
    List<float> txtALis = new List<float>();
    List<float> imgALis = new List<float>();
    private void Update()
    {
        alpha = A.Clamp01(alpha);
        for (int i = 0; i < tmpLis.Count; i++)
            tmpLis[i].color = A.ColA(tmpLis[i].color, tmpALis[i] * alpha);
        for (int i = 0; i < tmLis.Count; i++)
            tmLis[i].color = A.ColA(tmLis[i].color, tmALis[i] * alpha);
        for (int i = 0; i < txtLis.Count; i++)
            txtLis[i].color = A.ColA(txtLis[i].color, txtALis[i] * alpha);
        for (int i = 0; i < imgLis.Count; i++)
            imgLis[i].color = A.ColA(imgLis[i].color, imgALis[i] * alpha);
    }
    public void Alpha()
    {
        tmpLis = gameObject.GCA<TextMeshPro>();
        tmpLis.ForEach(a => tmpALis.Add(a.color.a));
        tmLis = gameObject.GCA<TextMesh>();
        tmLis.ForEach(a => tmALis.Add(a.color.a));
        txtLis = gameObject.GCA<Text>();
        txtLis.ForEach(a => txtALis.Add(a.color.a));
        imgLis = gameObject.GCA<Image>();
        imgLis.ForEach(a => imgALis.Add(a.color.a));
    }
    public void Color()
    {
        A.FOsOT<TextMeshPro>().ForEach(a => a.color = a.color == color ? color2 : a.color);
        A.FOsOT<TextMesh>().ForEach(a => a.color = a.color == color ? color2 : a.color);
        A.FOsOT<Text>().ForEach(a => a.color = a.color == color ? color2 : a.color);
        A.FOsOT<Image>().ForEach(a => a.color = a.color == color ? color2 : a.color);
        A.SaveOpenScenes();
    }
    public void Font()
    {
        A.FOsOT<TextMeshPro>().ForEach(a => a.font = tmpFont);
        A.FOsOT<TextMesh>().ForEach(a => a.font = font);
        A.FOsOT<Text>().ForEach(a => a.font = font);
        A.SaveOpenScenes();
    }
}
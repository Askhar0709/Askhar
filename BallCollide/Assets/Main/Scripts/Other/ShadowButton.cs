using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShadowButton : Shadow, IPointerDownHandler, IPointerUpHandler
{
    public float sclTime;
    public Vector2 clickDPos;
    public Color shadowColor;
    public Vector2 shadowDistance;
    RectTransform rt;
    private void Awake()
    {
        rt = GetComponent<RectTransform>();
        StartCoroutine(Scale(sclTime));
        Reset();
    }
    private IEnumerator Scale(float time)
    {
        for (float dt = 0; dt < time; dt += Time.deltaTime)
        {
            transform.localScale = Vector3.one * dt / time;
            yield return null;
        }
        transform.localScale = Vector3.one;
    }
    [ContextMenu("Reset")]
    public void Reset()
    {
        effectColor = A.ColDv(gameObject.UICol(Color.white), -0.2f);
        effectDistance = new Vector2(0, -20);
        sclTime = 0.25f;
        clickDPos = new Vector2(0, -20);
        shadowColor = new Color(0f, 0f, 0f, 0.2f);
        shadowDistance = new Vector2(20, -20);
    }
    public override void ModifyMesh(VertexHelper vh)
    {
        if (!IsActive())
            return;
        var verts = new List<UIVertex>();
        vh.GetUIVertexStream(verts);
        var neededCpacity = verts.Count * 5;
        if (verts.Capacity < neededCpacity)
            verts.Capacity = neededCpacity;
        var tmp = verts.Count;
        ApplyShadowZeroAlloc(verts, shadowColor, 0, verts.Count, effectDistance.x + shadowDistance.x, effectDistance.y + shadowDistance.y);
        ApplyShadowZeroAlloc(verts, effectColor, tmp, verts.Count, effectDistance.x, effectDistance.y);
        vh.Clear();
        vh.AddUIVertexTriangleStream(verts);
    }
    private void OnDisable()
    {
        if (Application.isPlaying)
        {
            rt.anchoredPosition -= clickDPos;
            effectDistance += clickDPos;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        rt.anchoredPosition += clickDPos;
        effectDistance -= clickDPos;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        rt.anchoredPosition -= clickDPos;
        effectDistance += clickDPos;
    }
}
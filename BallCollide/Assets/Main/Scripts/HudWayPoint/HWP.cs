﻿using UnityEngine;

public class HWP : MonoBehaviour {

    /// <summary>
    /// 
    /// </summary>
    public HWPInfo info;

    /// <summary>
    /// Instantiate a new Hud
    /// add hud to hud manager in start
    /// </summary>
    void Start()
    {
        if (HWPManager.instance != null)
        {
            if (info.m_Target == null) { info.m_Target = this.GetComponent<Transform>(); }
            if (info.ShowDynamically) { info.Hide = true; }
            HWPManager.instance.CreateHud(this.info);
        }
        else
        {
            Debug.LogError("Need have a Hud Manager in scene");
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public void Show()
    {
        if (HWPManager.instance != null)
        {
            HWPManager.instance.HideStateHud(info, false);
        }
        else
        {
            Debug.LogWarning("the instance of bl_HudManager in scene wasn't found.");
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public void Hide()
    {
        if (HWPManager.instance != null)
        {
            HWPManager.instance.HideStateHud(info, true);
        }
        else
        {
            Debug.LogWarning("the instance of bl_HudManager in scene wasn't found.");
        }
    }

}
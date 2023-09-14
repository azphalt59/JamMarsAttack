using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldKeyTask : MonoBehaviour
{
    public BoxCollider Collider;
    public Canvas UICanvas;
    public Image ProgressBar;
    public KeyCode KeyToHold;

    public float TaskTime;

    private float Progress;
    private bool IsInTaskZone = false;

    void Start()
    {
        SetTaskUIVisible(false);
        Progress = 0;
    }

    void Update()
    {
        if (!IsInTaskZone)
        {
            return;
        }

        if (!Input.GetKey(KeyToHold) && Progress >= 0)
        {
            Progress -= Time.deltaTime;
        } else if (Progress < TaskTime)
        {
            Progress += Time.deltaTime;
        }

        Progress = Math.Clamp(Progress, 0, TaskTime);

        UICanvas.enabled = Progress > 0;

        RectTransform rectTransform = ProgressBar.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(20, Mathf.Lerp(0, 50, Progress / TaskTime));
    }

    void OnTriggerEnter(Collider other)
    {
        SetTaskUIVisible(true);
    }

    void OnTriggerExit(Collider other)
    {
        SetTaskUIVisible(false);
        Progress = 0;
    }

    private void SetTaskUIVisible(bool visible)
    {
        IsInTaskZone = visible;
        UICanvas.enabled = visible;
    }
}

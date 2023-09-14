using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HoldKeyTask : MonoBehaviour
{
    public BoxCollider Collider;
    public Canvas UICanvas;
    public GameObject ProgressBarGameObject;
    public Image ProgressBarImage;
    public KeyCode KeyToHold;
    public TMP_Text KeyText;

    public float TaskTime;

    private float Progress;
    private bool IsInTaskZone = false;

    void Start()
    {
        KeyText.SetText(KeyToHold.ToString());
        SetTaskUIVisible(false);
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

        if (Progress >= TaskTime)
        {
            TaskCore.Instance.IncrementCompletedTasks();
            HideTask();
            return;
        }

        Progress = Math.Clamp(Progress, 0, TaskTime);

        ProgressBarGameObject.SetActive(Progress > 0);

        RectTransform rectTransform = ProgressBarImage.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(20, Mathf.Lerp(0, 50, Progress / TaskTime));
    }

    public void HideTask()
    {
        SetTaskVisible(false);
        SetTaskUIVisible(false);
    }

    public void ShowTask()
    {
        SetTaskVisible(true);
    }

    private void SetTaskVisible(bool visible)
    {
        ProgressBarGameObject.SetActive(visible);
        Progress = 0;
        gameObject.SetActive(visible);
    }

    void OnTriggerEnter(Collider other)
    {
        SetTaskUIVisible(true);
    }

    void OnTriggerExit(Collider other)
    {
        SetTaskUIVisible(false);
        Progress = 0;
        ProgressBarGameObject.SetActive(false);
    }

    private void SetTaskUIVisible(bool visible)
    {
        IsInTaskZone = visible;
        UICanvas.enabled = visible;
    }
}

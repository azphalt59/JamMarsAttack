using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

public class TaskCore : MonoBehaviour
{
    public static TaskCore Instance;

    public int CompletedTasks;
    public int TasksToWin;

    public List<HoldKeyTask> TaskTriggers;
    public HoldKeyTask CurrentTask;

    private TaskCore()
    {
        Instance = this;
    }

    private void Start()
    {
        foreach (HoldKeyTask holdKeyTask in TaskTriggers)
        {
            holdKeyTask.HideTask();
        }

        EnableRandomTask();
    }

    private void EnableRandomTask()
    {
        HoldKeyTask nextTask;
        do
        {
            nextTask = TaskTriggers[Random.Range(0, TaskTriggers.Count)];
        } while (nextTask == CurrentTask);

        CurrentTask = nextTask;
        CurrentTask.ShowTask();
    }

    public void IncrementCompletedTasks()
    {
        CompletedTasks++;

        EnableRandomTask();
    }

    public void AddAll(HoldKeyTask[] holdKeyTasks)
    {
        foreach (HoldKeyTask holdKeyTask in holdKeyTasks)
        {
            TaskTriggers.Add(holdKeyTask);
        }
    }
}

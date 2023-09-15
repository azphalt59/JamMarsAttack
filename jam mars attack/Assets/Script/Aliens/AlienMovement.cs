using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI;
using System;

public class AlienMovement : MonoBehaviour
{
    [Header("Path")]
    [SerializeField] private Path path;
    private Path nextPath;
    private Path nextPath2;
    private Vector3 target;
    private int pathPosIndex = 1;
    private float offsetTarget = 0.01f;
    private Vector3 dir;
    [SerializeField] private bool pathTransitions = false;

    [Header("Movements stats")]
    [SerializeField] private float movementSpeed = 5f;

    [Header("Paths")]

    [SerializeField] private Path callPath;
    [SerializeField] private Path workPath;
    [SerializeField] private Path launchPath;
    [SerializeField] private Path freeTimePath;
    [SerializeField] private Path nightPath;

    [Header("Path transition time state")]
    [SerializeField] private Path sleepToMorningCall;
    [SerializeField] private Path callToMorningWork;
    [SerializeField] private Path morningCallToLaunch;
    [SerializeField] private Path LaunchToFreeTime;
    [SerializeField] private Path FreeTimeToWork;
    [SerializeField] private Path workToEveningLaunch;
    [SerializeField] private Path eveningLaunchToCall;
    [SerializeField] private Path CallToSleep;


    Vector3 transiPathTarget = Vector3.zero;
    // Update is called once per frame
    void Update()
    {
        // check if at end path 
        if (Vector3.Distance(transform.position, target) < offsetTarget)
        {

            if (pathPosIndex == path.pathPositions.Count)
            {
                Debug.Log("End path");
                pathPosIndex = 0;
                if(nextPath != null)
                {
                    path = nextPath;
                }

                if (nextPath2 != null)
                {
                    nextPath = nextPath2;
                    nextPath2 = null;
                }
                else if (nextPath2 == null)
                {
                    nextPath = null;
                }
             
            }
        }

        target = path.pathPositions[pathPosIndex];
        transform.position = Vector3.MoveTowards(transform.position, target, movementSpeed * Time.deltaTime);
        dir = transform.position - target;
        if (Vector3.Distance(transform.position, target) < offsetTarget)
        {
            pathPosIndex++;
        }
        if (TimeSystem.Instance.phaseTimeCopy != TimeSystem.Instance.phaseTime)
        {
            SetPath();
        }

        UpdateRotation();
       
    }
    private void UpdateRotation()
    {
        Vector2 normDir;
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.z))
        {
            if(dir.x < 0)
            {
                normDir = new Vector2(-1, 0);
                transform.localRotation = Quaternion.Euler(0, 90, 0);
            }
            if(dir.x > 0)
            {
                normDir = new Vector2(1, 0);
                transform.localRotation = Quaternion.Euler(0, 270, 0);
            }
        }
        else if (Mathf.Abs(dir.x) < Mathf.Abs(dir.z))
        {
            if (dir.z < 0)
            {
                normDir = new Vector2(0, -1);
                transform.localRotation = Quaternion.Euler(0,0, 0);
            }
            if (dir.z > 0)
            {
                normDir = new Vector2(0, 1);
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }


    }

    public void SetPath()
    {
        TimeSystem.Instance.phaseTimeCopy = TimeSystem.Instance.phaseTime;
        //pathPosIndex = 0;
        if (TimeSystem.Instance.phaseTime == TimeSystem.PhaseTime.MorningCall)
        {
            nextPath = sleepToMorningCall;
            nextPath2 = callPath;
        }
        if (TimeSystem.Instance.phaseTime == TimeSystem.PhaseTime.MorningWork)
        {
            nextPath = callToMorningWork;
            nextPath2 = workPath;
        }
        if (TimeSystem.Instance.phaseTime == TimeSystem.PhaseTime.MiddayLaunch)
        {
            nextPath = morningCallToLaunch;
            nextPath2 = launchPath;  
        }
        if (TimeSystem.Instance.phaseTime == TimeSystem.PhaseTime.Free)
        {
            nextPath = LaunchToFreeTime;
            nextPath2 = freeTimePath;
        }
        if (TimeSystem.Instance.phaseTime == TimeSystem.PhaseTime.AfternoonWork)
        {
            nextPath = FreeTimeToWork;
            nextPath2 = workPath;
        }
        if (TimeSystem.Instance.phaseTime == TimeSystem.PhaseTime.EveningLaunch)
        {
            nextPath = workToEveningLaunch;
            nextPath2 = launchPath;
        }
        if(TimeSystem.Instance.phaseTime == TimeSystem.PhaseTime.EveningCall)
        {
            nextPath = eveningLaunchToCall;
            nextPath2 = callPath;
        }
        if (TimeSystem.Instance.phaseTime == TimeSystem.PhaseTime.Sleep)
        {
            nextPath = CallToSleep;
            nextPath2 = nightPath;
        }

        if (pathTransitions)
            return;

        if(nextPath != null) 
        {
            if(nextPath == path)
            {
                nextPath = null;
            } 
            else
            {
                pathTransitions = true;
            }
        }
    }
}

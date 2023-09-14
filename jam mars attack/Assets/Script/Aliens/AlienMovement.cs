using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI;


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

    [Header("Paths & transitions")]
    [SerializeField] private Path launchPath;
    [SerializeField] private Path workPath;
    [SerializeField] private Path workToLauchPath;
    [SerializeField] private Path launchToWorkPath;
 
    Vector3 transiPathTarget = Vector3.zero;



    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

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

       
    }

    public void SetPath()
    {
        TimeSystem.Instance.phaseTimeCopy = TimeSystem.Instance.phaseTime;
        //pathPosIndex = 0;
        
        if(TimeSystem.Instance.phaseTime == TimeSystem.PhaseTime.Launch ||
            TimeSystem.Instance.phaseTime == TimeSystem.PhaseTime.Sleep ||
            TimeSystem.Instance.phaseTime == TimeSystem.PhaseTime.Free ||
            TimeSystem.Instance.phaseTime == TimeSystem.PhaseTime.Call)
        {
            Debug.Log("launch");
            nextPath = workToLauchPath;
            nextPath2 = launchPath;
           
        }
        else
        {
            Debug.Log("work");
            if(path = launchPath)
            {
                nextPath = launchToWorkPath;
            }
            nextPath2 = workPath;
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

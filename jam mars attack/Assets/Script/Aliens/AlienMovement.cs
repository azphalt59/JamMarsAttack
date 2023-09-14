using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienMovement : MonoBehaviour
{
    [Header("Path")]
    [SerializeField] private Path path;
    private Vector3 target;
    private int pathPosIndex = 1;
    private float offsetTarget = 0.01f;
    private Vector3 dir;

    [Header("Movements stats")]
    [SerializeField] private float movementSpeed = 5f;
    [Header("PathList")]
    [SerializeField] private Path LaunchPath;
    [SerializeField] private Path WorkPath;

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
        if (TimeSystem.Instance.phaseTimeCopy != TimeSystem.Instance.phaseTime)
        {
            SetPath();
        }
        
        // check if at end path 
        if (Vector3.Distance(transform.position, target) < offsetTarget)
        {
            if (pathPosIndex == path.pathPositions.Count)
            {
                pathPosIndex = 0;
            }
        }
            target = path.pathPositions[pathPosIndex];
        transform.position = Vector3.MoveTowards(transform.position, target, movementSpeed * Time.deltaTime);
        dir = transform.position - target;
        if (Vector3.Distance(transform.position, target) < offsetTarget)
        {
            pathPosIndex++;
        }
    }

    public void SetPath()
    {
        TimeSystem.Instance.phaseTimeCopy = TimeSystem.Instance.phaseTime;
        //pathPosIndex = 0;

        if(TimeSystem.Instance.phaseTime == TimeSystem.PhaseTime.Launch)
        {
            path = LaunchPath;
        }
        else
        {
            path = WorkPath;
        }
    }
}

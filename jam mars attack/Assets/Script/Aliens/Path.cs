using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public List<Vector3> pathPositions;

    private void Awake()
    {

        for (int i = 0; i < transform.childCount; i++)
        {
            pathPositions.Add(transform.GetChild(i).gameObject.transform.position);
        }
    }
    private void Start()
    {
        //GameManager.Instance.pathList.Add(this);
    }
    private void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position);
        }
    }
}

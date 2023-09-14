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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

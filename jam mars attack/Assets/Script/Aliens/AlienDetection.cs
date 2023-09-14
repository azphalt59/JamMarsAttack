using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienDetection : MonoBehaviour
{
    [SerializeField] public float radius;
    [SerializeField][Range(0,360)] public float angle;
    [SerializeField] private float checkDelay;
    

    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask obstructionLayer;

    public bool playerDetected;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FovCheck());
    }

    private IEnumerator FovCheck()
    {
        WaitForSeconds wait = new WaitForSeconds(checkDelay);
        while (true) 
        { 
            yield return wait;
            FovCalcul();
        }
    }
    public void FovCalcul()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, radius, playerLayer);

        if (cols.Length != 0)
        {
            Transform col = cols[0].transform;
            Vector3 dirToCol = (col.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, dirToCol) < angle / 2)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, col.position);

                if (!Physics.Raycast(transform.position, dirToCol, distanceToPlayer, obstructionLayer))
                {
                    // player detection
                    playerDetected = true;
                }
                else
                {
                    playerDetected = false;
                }
            }
            else
            {
                playerDetected = false;
            }
        }
        else
        {
            playerDetected = false;
        }
    }

    private void Update()
    {
        if (playerDetected) 
        {

        }
    }
}

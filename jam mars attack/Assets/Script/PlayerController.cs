using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera Camera;
    public Rigidbody Rigidbody;

    public float Speed = 5f;
    public float CameraDistanceFromPlayer = 15f;

    void Start()
    {
    }

    void Update()
    {
        Vector3 direction = new Vector3();

        // direction.z = Input.GetAxis("Horizontal") * Speed;
        // direction.x = Input.GetAxis("Vertical") * -Speed;

        if (Input.GetKey(KeyCode.A))
        {
            direction.z = -Speed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            direction.z = Speed;
        }

        if (Input.GetKey(KeyCode.W))
        {
            direction.x = -Speed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            direction.x = Speed;
        }

        Rigidbody.velocity = direction;
        Camera.transform.position = gameObject.transform.position + new Vector3(CameraDistanceFromPlayer, CameraDistanceFromPlayer, 0);
    }
}

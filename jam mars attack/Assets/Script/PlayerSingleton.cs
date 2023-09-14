using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSingleton : MonoBehaviour
{
    public static PlayerSingleton Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private int health;


    [Header("Win & Lose con")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void Detected()
    {
        health--;
        if(health < 0)
        {
            Death();
        }
    }

    public void Death()
    {
        losePanel.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GameObject player;
    [SerializeField] private int maxHealth;
    [SerializeField] private Transform respawnPosition;
    private int health;


    [Header("Win & Lose con")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    // Start is called before the first frame update

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
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
        } else
        {
            player.transform.position = respawnPosition.position;
        }
    }

    public void Death()
    {
        losePanel.SetActive(true);
    }
    public void Win()
    {
        winPanel.SetActive(true);
    }
    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    private PlayerController playerControllerScript;
    void Start()
    {
        gameOverPanel.SetActive(false);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    void Update()
    {
        if (playerControllerScript.gameOver)
        {
            gameOverPanel.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private Text scoreTextUi;
    private Text livesTextUi;

    public GameObject playerPrefab;
    public GameObject player;

    public GameObject gameComplete;

    private int Score = 0;
    private bool isFlagTaken = false;

    private int lives = 3;

    public int sceneIndex = 1;

    private Transform playerSpawnPoint;

    public bool UIScreenShown = false;

    private void Awake()
    {
        if (instance == null)
        {
            isFlagTaken = false;
            UIScreenShown = false;
            sceneIndex = 1;
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            instance.Start();
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        isFlagTaken = false;
        UIScreenShown = false;
        sceneIndex = 1;

        GameObject scoretextObject = GameObject.Find("ScoreText");
        GameObject livestextObject = GameObject.Find("LivesText");

        if(scoretextObject != null)
        {
            scoreTextUi = scoretextObject.GetComponent<Text>();
        }

        if (livestextObject != null)
        {
            livesTextUi = livestextObject.GetComponent<Text>();
        }


        GameObject playerSpawnPointObject = GameObject.Find("PlayerSpawnPoint");

        if(playerSpawnPointObject != null)
        {
            playerSpawnPoint = playerSpawnPointObject.transform;
            SpawnPlayer();
        }

        updateUI();
    }


    private void SpawnPlayer()
    {
        player = Instantiate(playerPrefab, playerSpawnPoint.position, new Quaternion());
    }

    public void AddScore(int score)
    {
        Score += score;
        updateUI();
    }


    public bool IsFlagTaken()
    {
        return isFlagTaken;
    }

    public void TakeFlag(bool taken)
    {
        isFlagTaken = taken;
    }

    public void PlayerDie()
    {
        lives -= 1;
        TakeFlag(false);

        if(lives == 0)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SpawnPlayer();
        }

        updateUI();
    }


    private void updateUI()
    {
        if(livesTextUi != null) livesTextUi.text = "Lives: " + lives.ToString();

        if(scoreTextUi != null) scoreTextUi.text = "Score: " + Score.ToString();
    }


    public void LevelComplete()
    {
        isFlagTaken = false;
        UIScreenShown = true;
        GameObject canvas = GameObject.Find("Canvas");
        Instantiate(gameComplete, canvas.transform);
    }


    public void levelUp()
    {
        isFlagTaken = false;
        UIScreenShown = false;
        sceneIndex++;

        if (SceneManager.sceneCountInBuildSettings > sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            sceneIndex = 1;
            SceneManager.LoadScene(0);
        }
    }


    public void GotoMainMenu()
    {
        isFlagTaken = false;
        UIScreenShown = false;
        TakeFlag(false);
        SceneManager.LoadScene(0);
    }


    public GameObject getPlayer()
    {
        return player;
    }


    public void startGame()
    {
        isFlagTaken = false;
        UIScreenShown = false;
        SceneManager.LoadScene(sceneIndex);
    }

    public void quidGame()
    {
        Application.Quit();
    }
}

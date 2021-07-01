using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public Text scoreTextUi;

    private int Score = 0;

    private void Awake()
    {
        if(instance == null) instance = this;
    }


    public void AddScore(int score)
    {
        Score += score;
    }


    private void Update()
    {
        scoreTextUi.text = "Score: " + Score.ToString();
    }

}

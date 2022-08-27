using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject ballPrefab;
    public GameObject playerPrefab;
    public TMP_Text scoreText;
    public TMP_Text ballsText;
    public TMP_Text levelText;
    public TMP_Text highScoreText;

    public GameObject panelMenu;
    public GameObject panelPlay;
    public GameObject panelLevelCompleted;
    public GameObject panelGameOver;

    private GameObject currentBall;
    public GameObject[] levels;


    private const string highScoreKey = "highscore";

    public static GameManager Instance { get; private set; }

    private int score;

    public int Score
    {
        get { return score; }
        set {
            score = value;
            scoreText.text = $"SCORE: {score}";
        }
    }

    private int level;

    public int Level
    {
        get { return level; }
        set {
            level = value;
            levelText.text = $"LEVEL: {level}";
        }
    }

    private int balls;

    public int Balls
    {
        get { return balls; }
        set {
            balls = value;
            ballsText.text = $"BALLS: { balls}";
        }
    }

    public enum State
    {
        Menu,
        Init,
        Play,
        LevelCompleted,
        Loadlevel,
        GameOver
    }

    private State state;
    private GameObject currentLevel;
    private bool isSwitchingState;

    public void Play_Click()
    {
        SwitchState(State.Init);
    }

    private void Start()
    {
        Instance = this;
        this.SwitchState(State.Menu);
    }

    private void Update()
    {
        switch (state)
        {
            case State.Menu:
                break;

            case State.Init:
                break;

            case State.Play:
                if (currentBall == null)
                {
                    if (Balls > 0)
                    {
                        currentBall = Instantiate(ballPrefab);
                    }
                    else
                    {
                        SwitchState(State.GameOver);
                    }
                }

                if(currentLevel != null && currentLevel.transform.childCount == 0 && !isSwitchingState)
                {
                    isSwitchingState = true;
                    this.SwitchState(State.LevelCompleted);
                }
                
                break;

            case State.LevelCompleted:
                break;

            case State.Loadlevel:
                break;

            case State.GameOver:
                Cursor.visible = true;
                if (Input.anyKeyDown)
                {
                    SwitchState(State.Menu);
                }
                break;
        }
    }

    public void SwitchState(State newState, float delay = 0)
    {
        StartCoroutine(SwitchDelay(newState, delay));
    }

    IEnumerator SwitchDelay(State newState, float delay)
    {
        yield return new WaitForSeconds(delay);
        EndState();
        BeginState(newState);
    }

    private void BeginState(State newState)
    {
        this.state = newState;
        switch (newState)
        {
            case State.Menu:
                panelMenu.SetActive(true);
                highScoreText.text = $"HIGH SCORE\n{PlayerPrefs.GetInt(highScoreKey)}";
                break;

            case State.Init:
                panelPlay.SetActive(true);
                Score = 0;
                Level = 0;
                Balls = 3;
                Instantiate(playerPrefab);
                SwitchState(State.Loadlevel);
                break;

            case State.Play:
                Cursor.visible = false;
                isSwitchingState = false;
                break;

            case State.LevelCompleted:
                Destroy(currentLevel);
                Destroy(currentBall);
                Level++;
                panelLevelCompleted.SetActive(true);
                SwitchState(State.Loadlevel, 2f);
                break;

            case State.Loadlevel:
                if(Level < levels.Length)
                {
                    currentLevel = Instantiate(levels[Level]);
                    SwitchState(State.Play);
                }
                else
                {
                    SwitchState(State.GameOver);
                }
                
                break;

            case State.GameOver:
                Destroy(currentLevel);
                if (Score > PlayerPrefs.GetInt(highScoreKey))
                {
                    PlayerPrefs.SetInt(highScoreKey, Score);
                }

                panelGameOver.SetActive(true);
                break;
        }
    }
    private void EndState()
    {
        switch (this.state)
        {
            case State.Menu:
                panelMenu.SetActive(false);
                break;

            case State.Init:
                break;

            case State.Play:
                break;

            case State.LevelCompleted:
                panelLevelCompleted.SetActive(false);
                break;

            case State.Loadlevel:
                break;

            case State.GameOver:
                panelGameOver.SetActive(false);
                panelPlay.SetActive(false);
                break;
        }
    }
}
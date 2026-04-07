using JetBrains.Annotations;
using System;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   // private float waitingToStartTimer = 1f;
    private float countingToEndTimer = 3f;
   private float gameplayToStart;
    private float gameplayToStartMax = 30f;
    private State state;
    private bool isPaused = false;

    public static GameHandler Instance { get; private set; }
    public event EventHandler onStateChange;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;


    private enum State
    {
        WaitingToStart,
        CountingToStart,
        GamePlaying,
        GameOver,


    }

    private void Start()
    {
        GameInput.Instance.OnPause += GameInput_OnPause;
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (state == State.WaitingToStart) {
        state = State.CountingToStart;
        onStateChange?.Invoke(this, EventArgs.Empty);
        }
    }

    private void GameInput_OnPause(object sender, EventArgs e)
    {
        PauseGame();
    }

    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
        
    }

    // Update is called once per frame
    private void Update()
    {
        switch (state)
        
        { 
            case State.WaitingToStart:
                
                break;
            case State.CountingToStart:
                countingToEndTimer -= Time.deltaTime;
                if (countingToEndTimer < 0f)
                {
                    state = State.GamePlaying;
                    gameplayToStart = gameplayToStartMax;
                    onStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.GamePlaying:
                gameplayToStart -= Time.deltaTime;
                if (gameplayToStart < 0f)
                {
                    state = State.GameOver;
                    onStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;

        }
       
        
        
    }
    public bool isGamePlaying()
    {
        return state == State.GamePlaying;
    }
    public bool isCountdownActive()
    {
        return state == State.CountingToStart;
    }
    public float GetCountDownTimer()
    {
        return countingToEndTimer;
    }

    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    public float GetPlayTime()
    {
       
        return 1 - (gameplayToStart / gameplayToStartMax);

    }

    public void PauseGame()
    {
        
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else { 
        Time.timeScale = 1f;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty); 
        
        }
       
    }
}

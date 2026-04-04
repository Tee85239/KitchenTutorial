using JetBrains.Annotations;
using System;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float waitingToStartTimer = 1f;
    private float countingToEndTimer = 3f;
   private float gameplayToStart;
    private float gameplayToStartMax = 10f;
    private State state;

    public static GameHandler Instance { get; private set; }
    public event EventHandler onStateChange;

    private enum State
    {
        WaitingToStart,
        CountingToStart,
        GamePlaying,
        GameOver,


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
                waitingToStartTimer -= Time.deltaTime;
                if(waitingToStartTimer < 0f)
                {
                    state = State.CountingToStart;
                    onStateChange?.Invoke(this, EventArgs.Empty);
                }
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
        Debug.Log(state);
        
        
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
}

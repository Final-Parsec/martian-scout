using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LevelOneManager : Singleton<LevelOneManager>
{
    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;

    public GameState State { get; private set; }

    public float time;
    private LevelTimer Timer;
    private int tier1Time;
    private int tier2Time;
    private int tier3Time;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        Timer = new LevelTimer();
        tier1Time = 3;
        tier2Time = 23;
        tier3Time = 43;
        ChangeState(GameState.Start);
        UnityEngine.Debug.Log(State);
    }

public void ChangeState(GameState newState)
    {
        //check if GameState actually changed
        if(State == newState)
            return;
        OnBeforeStateChanged?.Invoke(newState);

        State = newState;
        switch (newState) {
            case GameState.Start:
                HandleStart();
                break;
            case GameState.Tier1:
                HandleTier1();
                break;
            case GameState.Tier2:
                HandleTier2();
                break;
            case GameState.Tier3:
                HandleTier3();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnAfterStateChanged?.Invoke(newState);
        UnityEngine.Debug.Log($"New State: {newState}");
    }

private void HandleStart() => EnvironmentManager.Instance.SetRocks(0);
private void HandleTier1() => EnvironmentManager.Instance.SetRocks(10);
private void HandleTier2() => EnvironmentManager.Instance.SetRocks(25);
private void HandleTier3() => EnvironmentManager.Instance.SetRocks(40);

    // Update is called once per frame
    void Update()
    {
        Timer.Update();
        time = Timer.time;
        int currentState = (int)State;
        if(time >= tier1Time && currentState < (int)GameState.Tier1)
            ChangeState(GameState.Tier1);
        if(time >= tier2Time && currentState < (int)GameState.Tier2)
            ChangeState(GameState.Tier2);
        if(time >= tier3Time && currentState < (int)GameState.Tier3)
            ChangeState(GameState.Tier3);
    }

//Level Game States
[Serializable]
    public enum GameState {
        Start = 0, //no enemies for a moment you get your bearings
        Tier1 = 1, //these increasing tiers will represent more rox
        Tier2 = 2,
        Tier3 = 3
    }
}
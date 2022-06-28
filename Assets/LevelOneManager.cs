using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LevelOneManager : Singleton<LevelOneManager>
{
    public static event Action<TagsAndEnums.GameState> OnBeforeStateChanged;
    public static event Action<TagsAndEnums.GameState> OnAfterStateChanged;

    public TagsAndEnums.GameState State { get; private set; }

    public float time;
    private LevelTimer Timer;
    private int tier1Time;
    private int tier2Time;
    private int tier3Time;
    private int winTime;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        Timer = new LevelTimer();
        tier1Time = 3;
        tier2Time = 23;
        tier3Time = 53;
        winTime = 85;
        ChangeState(TagsAndEnums.GameState.Start);
        UnityEngine.Debug.Log(State);
    }

public void ChangeState(TagsAndEnums.GameState newState)
    {
        //check if GameState actually changed
        if(State == newState)
            return;

        //invoke new events for before StateChange
        OnBeforeStateChanged?.Invoke(newState);

        State = newState;
        switch (newState) {
            case TagsAndEnums.GameState.Start:
                HandleStart();
                break;
            case TagsAndEnums.GameState.Tier1:
                HandleTier1();
                break;
            case TagsAndEnums.GameState.Tier2:
                HandleTier2();
                break;
            case TagsAndEnums.GameState.Tier3:
                HandleTier3();
                break;
            case TagsAndEnums.GameState.Win:
                HandleWin();
                break;
            case TagsAndEnums.GameState.Lose:
                HandleLose();
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
private void HandleTier3() => EnvironmentManager.Instance.SetRocks(30);
private void HandleWin()
    {
        EnvironmentManager.Instance.EnableWinText();
    }
private void HandleLose()
    {
        EnvironmentManager.Instance.EnableLoseText();
    }

    // Update is called once per frame
    void Update()
    {
        Timer.Update();
        time = Timer.time;
        int currentState = (int)State;
        if(time >= tier1Time && currentState < (int)TagsAndEnums.GameState.Tier1)
            ChangeState(TagsAndEnums.GameState.Tier1);
        if(time >= tier2Time && currentState < (int)TagsAndEnums.GameState.Tier2)
            ChangeState(TagsAndEnums.GameState.Tier2);
        if(time >= tier3Time && currentState < (int)TagsAndEnums.GameState.Tier3)
            ChangeState(TagsAndEnums.GameState.Tier3);
        if(time >= winTime && currentState < (int)TagsAndEnums.GameState.Lose)
            ChangeState(TagsAndEnums.GameState.Win);
    }
}
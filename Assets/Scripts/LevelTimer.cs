using UnityEngine;
using System.Collections;

public class LevelTimer {
    public bool timerIsRunning;
    public float time;

    public LevelTimer(bool startNow = true){
        time = 0;
        timerIsRunning = false;
    }

    public void Start() {
        if(!timerIsRunning){
            timerIsRunning = true;
            time = 0;
        }
    }

    public void Update(){
        time += Time.deltaTime;
    }
}
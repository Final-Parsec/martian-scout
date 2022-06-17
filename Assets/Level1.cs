using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{

    private LevelTimer timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = new LevelTimer(); 
    }

    // Update is called once per frame
    void Update()
    {
        LevelTimer.Update();
    }
}

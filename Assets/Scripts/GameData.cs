using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

// This script allows other scripts to access values from GameDataSO.
// このスクリプトは、他のスクリプトがGameDataSOの情報を読めるようにする

public class GameData
{
    private float startTime;
    private float timeLimit;
    private int totalLaps;
    private float respawnTime;

// This block accesses the variables in GameDataSO and stores them here.
//　これはGameDataSOの変数
    public GameData(GameDataSO scriptableInit)
    {
        this.startTime = scriptableInit.startTime;
        this.timeLimit = scriptableInit.timeLimit;
        this.totalLaps = scriptableInit.totalLaps;
        this.respawnTime = scriptableInit.respawnTime;
    }

//Getters and setters allow other scripts to access and change these variables
//これでGameDataSOの変数を取ったり変わったりができる
    public float StartTime
    { get { return startTime; }
      set { startTime = value; }
    }

    public float TimeLimit
    {
        get { return timeLimit; }
        set { timeLimit = value; }
    }

    public int TotalLaps
    {
        get { return totalLaps; }
        set { totalLaps = value; }
    }
    public float RespawnTime
    {
        get { return respawnTime; }
        set { respawnTime = value; }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script lets you quickly change game rules in Unity.
// このスクリプトでゲームのルールを変えるメニューを作る。

[CreateAssetMenu(fileName = "GameDataSO", menuName = "GameData")]　//This creates a menu in Unity. これはUnityでメニューを作る
public class GameDataSO : ScriptableObject
{
    public float startTime; //レースが始まる時間
    public float timeLimit; //レースが終わる時間
    public int totalLaps; //何回でクリアする
    public float respawnTime;
}
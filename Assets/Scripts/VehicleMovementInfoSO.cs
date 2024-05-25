using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script allows users to create multiple menus in Unity that let you quickly change the values of each variable for multiple objects.
//このスクリプトはUnityで1つ以上の車の変数を変えるメニューを作れる。


[CreateAssetMenu(fileName = "NewVehicleMovementInfo", menuName = "VehicleData")] 
public class VehicleMovementInfoSO : ScriptableObject
{
    public float forwardThrust;
    public float lateralThrust;
    public float rotationSpeed;
    public float rollSpeed;
    public float accelerationDebuff;
    public bool inertialDampenersOn;
    public float boostSpeed;
    public float damageResistance;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script allows other scripts to access the values stored in VehicleMovementInfoSO
// このスクリプトは、他のスクリプトがVehicleMovementInfoSOの情報を読めるようにする

public class VehicleData 
{
    private float forwardThrust;
    private float lateralThrust;
    private float rotationSpeed;
    private float rollSpeed;
    private float accelerationDebuff = 1f;
    private bool inertialDampenersOn;
    private float boostSpeed;
    private float damageResistance;

    public VehicleData(VehicleMovementInfoSO scriptableInit)
    {
        this.forwardThrust = scriptableInit.forwardThrust;
        this.lateralThrust = scriptableInit.lateralThrust;
        this.rotationSpeed = scriptableInit.rotationSpeed;
        this.rollSpeed = scriptableInit.rollSpeed;
        this.accelerationDebuff = scriptableInit.accelerationDebuff;
        this.inertialDampenersOn = scriptableInit.inertialDampenersOn;
        this.boostSpeed = scriptableInit.boostSpeed;
        this.damageResistance = scriptableInit.damageResistance;
    }

    public float ForwardThrust
    {
        get { return forwardThrust; }
        set { forwardThrust = value; }
    }
    public float LateralThrust
    {  
        get { return lateralThrust; } 
        set { lateralThrust = value; } 
    }
    public float RotationSpeed
    {
        get { return rotationSpeed; }
        set { rotationSpeed = value; }
    }
    public float RollSpeed
    {
        get { return rollSpeed; }
        set { rollSpeed = value; }
    }
    public float AccelerationDebuff
    {
        get { return accelerationDebuff; }
        set { accelerationDebuff = value; }
    }
    public bool InertialDampenersOn
    {
        get { return  inertialDampenersOn; }
        set {  inertialDampenersOn = value;}
    }
    public float BoostSpeed
    {
        get { return boostSpeed; }
        set { boostSpeed = value; }
    }
    public float DamageResistance
    {
        get { return damageResistance; }
        set { damageResistance = value; }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public GameObject MainCamera;
    public GameObject RearCamera;
    public GameObject FrontCameraAxis;
    public GameObject BackCameraAxis;
    public GameDataSO gameDataInitializer;
    public PlayerControl player;

    private Vector3 mainCameraInitialPosition;
    private Vector3 rearCameraInitialPosition;
    // Update is called once per frame
    
    private void FlipCamera()
    {
        if (Input.GetKey("r"))
        {
            MainCamera.SetActive(false);
            RearCamera.SetActive(true);
        }
        else
        {
            MainCamera.SetActive(true);
            RearCamera.SetActive(false);
        }
    }

    private Vector3 deathSpot;
    private void GetDeathSpot()
    {
        deathSpot = MainCamera.transform.position;
    }
    private void LookAtDeathSpot()
    {
        MainCamera.transform.position = deathSpot;
    }    
    private void ReturnToFocus()
    {
        MainCamera.transform.localPosition = mainCameraInitialPosition;
    }
    void Update()
    {
        if(!player.IsAlive)
        {
            if(player.TimeFromDeath < 0.1)
            {
                GetDeathSpot();
            }
            LookAtDeathSpot();
        }
        if(player.IsAlive)
        {
            ReturnToFocus();
        }
        if(player.IsAlive)
        {
            FlipCamera();
        }
        
    }
    void Start()
    {
        MainCamera.SetActive(true);
        RearCamera.SetActive(false);
        mainCameraInitialPosition = MainCamera.transform.localPosition;
        rearCameraInitialPosition = RearCamera.transform.localPosition;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public GameObject MainCamera;
    public GameObject RearCamera;

    // Update is called once per frame
    void Update()
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
    void Start()
    {
        MainCamera.SetActive(true);
        RearCamera.SetActive(false);
    }
}

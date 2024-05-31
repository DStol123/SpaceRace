using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private GameObject MainCamera;
    private GameObject SubCamra;
    // Start is called before the first frame update
    void Start()
    {
        SubCamra.SetActive(false);
        MainCamera = GameObject.Find("MainCamera");
        SubCamra = GameObject.Find("SubCamera");

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("push");
            MainCamera.SetActive(false);
            SubCamra.SetActive(true);
           
        }
        else
        {
            MainCamera.SetActive(true);
            RearCamera.SetActive(false);
        }
       

        }
        
}

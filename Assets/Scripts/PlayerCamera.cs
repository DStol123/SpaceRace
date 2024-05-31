using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public GameObject MainCamera;
    public GameObject RearCamera;
    // Start is called before the first frame update
    void Start()
    {
        MainCamera = GetComponent<GameObject>();
        RearCamera = GetComponent<GameObject>();
        MainCamera.SetActive(true);
        RearCamera.SetActive(false);
    }

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
        
}

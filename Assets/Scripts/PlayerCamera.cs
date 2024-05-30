using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private GameObject MainCamera;
    private GameObject SubCamera;
    // Start is called before the first frame update
    void Start()
    {
        MainCamera = GetComponent<GameObject>();
        SubCamera = GetComponent<GameObject>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            MainCamera.SetActive(false);
            SubCamera.SetActive(true);
        }
        else
        {
            MainCamera.SetActive(true);
            SubCamera.SetActive(false);
        }
       

    }
}

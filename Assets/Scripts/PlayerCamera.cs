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
        MainCamera = GetComponent<GameObject>();
        SubCamra = GetComponent<GameObject>();
         
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            MainCamera setActive = false;
            SubCamra.SetActive(true);
        }
        else
        {
            MainCamera.SetActive(true);
            SubCamra.SetActive(false);
        }
       

        }
        
    }
}

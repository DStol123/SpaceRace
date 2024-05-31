using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public GameObject MainCamera;
    public GameObject SubCamera;
    // Start is called before the first frame update
    void Start()
    {
        SubCamera.SetActive(false);
        MainCamera = GameObject.Find("Main Camera");
        SubCamera = GameObject.Find("Sub Camera");

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            Debug.Log("push");
            SubCamera.SetActive(true);
            MainCamera.SetActive(false);
            
          
        }


    }

}

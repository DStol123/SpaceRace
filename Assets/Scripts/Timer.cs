using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text txt;
    
    // Update is called once per frame
    void Update()
    {
        txt.text = Convert.ToString(Time.time);
    }
}

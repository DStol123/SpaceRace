using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Vector3 checkpoint;

    public GameObject checkPoint;
    public GameObject Player;

    void hitcheckpoint()
    {
        checkpoint = Player.transform.position;
    }
    // Start is called before the first frame update

    void Start()
    {
        checkpoint = Player.transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == Player.name)
        {
            checkpoint = Player.transform.position;
        }
    }
}

    // Update is called once per frame

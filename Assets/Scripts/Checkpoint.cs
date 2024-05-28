using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Vector3 checkpoint;
    public GameObject checkPoint;
    public GameObject Player;
    private int nowlaps = 0;
    private int totallaps;
    public Checkpoint(GameDataSO scriptableInit)//GameDataSOÇÃtotalLapsÇéÊìæ
    {
        this.totallaps = scriptableInit.totalLaps;
    }

    public int TotalLaps//èëÇ´ä∑Ç¶ÇÈÇΩÇﬂÇÃÉvÉçÉOÉâÉÄ
    {
        get { return totallaps; }
        set { totallaps = value; }
    }

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
        if (other.name == checkPoint.name)
        {
            checkpoint = Player.transform.position;
        }
        if (nowlaps == totallaps)
        {
            Debug.Log("GOAL!!");
        }
    }
}

    // Update is called once per frame

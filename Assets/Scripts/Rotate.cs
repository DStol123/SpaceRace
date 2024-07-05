using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]
    private float SpinSpeedX;
    [SerializeField]
    private float SpinSpeedY;
    [SerializeField]
    private float SpinSpeedZ;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.angularVelocity = new Vector3(SpinSpeedX, SpinSpeedY, SpinSpeedZ);
    }
}

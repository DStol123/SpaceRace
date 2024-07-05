using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour
{
    public GameObject WarpEnd;
    float warpOffset;
    [SerializeField]
    private float MaxSpeed;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        other.transform.position = WarpEnd.transform.position + warpOffset * WarpEnd.transform.forward;
        other.transform.rotation = WarpEnd.transform.rotation;
        other.attachedRigidbody.velocity = Vector3.ClampMagnitude(other.attachedRigidbody.velocity.magnitude * WarpEnd.transform.forward, MaxSpeed);
    }
}

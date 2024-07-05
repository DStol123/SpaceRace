using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField]
    private float force;
    private Vector3 launchDirection = new Vector3 (1f, 0, 0);
    void OnTriggerStay(Collider other)
    {
        other.attachedRigidbody.AddRelativeForce(launchDirection * force);
    }
}

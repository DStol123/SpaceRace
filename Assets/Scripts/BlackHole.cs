using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    [SerializeField]
    private float gravityStrength;
    [SerializeField]
    private float deathzone;

    public PlayerControl player;

    private Vector3 relativePosition = new Vector3();
    private float distanceToCenter;
    private float distanceToEdge;
    private Vector3 direction = new Vector3();

    private void OnTriggerStay(Collider other)
    {
        relativePosition = this.transform.position - other.gameObject.transform.position;
        distanceToCenter = relativePosition.magnitude;
        direction = relativePosition / distanceToCenter;
        if(other.attachedRigidbody && distanceToCenter != 0)
        {
            other.attachedRigidbody.AddForce(direction * gravityStrength * Time.deltaTime / distanceToCenter);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.IsAlive = false;
        }
        else if (collision.gameObject.tag != "Player")
        {
            Destroy(collision.gameObject);
        }
    }
}

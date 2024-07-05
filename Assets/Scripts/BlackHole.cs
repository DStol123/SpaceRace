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
    private Vector3 direction = new Vector3();

    public GameObject BlackHoleCamera;

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

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.IsAlive = false;
            BlackHoleCamera.SetActive(true);
        }
        else if (collision.gameObject.tag != "Player")
        {
            Destroy(collision.gameObject);
        }
    }
    void Start()
    {
        BlackHoleCamera.SetActive(false);
    }
    void Update()
    {
        if(player.IsAlive == true)
        {
            BlackHoleCamera.SetActive(false);
        }
    }
}

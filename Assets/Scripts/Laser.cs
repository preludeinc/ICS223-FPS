using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float speed = 6f;
    [SerializeField] private Rigidbody rb;
    private void FixedUpdate()
    {
        float toNewtons = 100;

        // uses the laser's local forward vector
        Vector3 movement = transform.forward * Time.deltaTime * speed * toNewtons;
        rb.velocity = movement;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();

        if (player != null)
        {
            player.Hit();
        }
        Destroy(this.gameObject);
    }
}

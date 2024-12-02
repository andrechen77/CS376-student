using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homing : MonoBehaviour
{
    public GameObject target;
    public float tightness;

    private Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        // always face forward
        transform.up = rb.velocity.normalized;

        if (target == null) {
            return;
        }

        Vector3 directionToTarget = target.transform.position - transform.position;
        directionToTarget.z = 0; // Ensure we're working in 
        directionToTarget.Normalize();

        float angleToTarget = Vector2.SignedAngle(rb.velocity, directionToTarget);

        rb.AddForce(-angleToTarget * tightness * transform.right * rb.velocity.sqrMagnitude);
    }
}

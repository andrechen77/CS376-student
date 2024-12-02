using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDamage : MonoBehaviour {
    public float damage;

    void OnCollisionEnter2D(Collision2D collision) {
        var health = collision.gameObject.GetComponent<Health>();
        if (health != null) {
            health.Damage(damage);
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public AudioClip damageSound;
    public AudioClip deathSound;

    public float value {
        get {
            return health;
        }
    }

    private float health;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void Damage(float amount) {
        health -= amount;

        if (damageSound != null) {
            AudioSource.PlayClipAtPoint(damageSound, transform.position);
        }
        if (health <= 0) {
            health = 0;
            Die();
        }
    }

    public virtual void Die() {
        if (deathSound != null) {
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
        }
        Destroy(gameObject);
    }
}

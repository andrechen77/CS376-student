using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Turret : MonoBehaviour {
    public float rotationSpeed; // Speed of turret rotation (degrees per second)
    public GameObject bulletPrefab;
    public AudioClip fireSound;

    public void TurnInDirection(float dTheta) {
        float rotationAmount = -dTheta * rotationSpeed * Time.deltaTime;

        transform.Rotate(0, 0, rotationAmount);
    }

    public GameObject Fire(float bulletSpeed) {
        if (bulletPrefab == null) {
            return null;
        }

        GameObject bullet = Instantiate(bulletPrefab, transform.position + transform.up, transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * bulletSpeed;
        
        if (fireSound != null) {
            AudioSource.PlayClipAtPoint(fireSound, transform.position);
        }

        return bullet;
    }
}
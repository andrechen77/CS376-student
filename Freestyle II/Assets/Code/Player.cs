using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float rotationSpeed; // degrees per second
    public float moveSpeed; // units per second
    public float bulletSpeed;
    public float maxEnergy;
    public float energyRechargeRate;
    public float fireEnergyCost;

    public float Energy {
        get; private set;
    }

    private Rigidbody2D rb;
    private Turret turret;
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer turretSpriteRenderer;
    private PointEffector2D effector;
    private Health health;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        turret = GetComponentInChildren<Turret>();
        effector = GetComponent<PointEffector2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        turretSpriteRenderer = turret.GetComponent<SpriteRenderer>();
        health = GetComponent<Health>();
        Energy = maxEnergy;
        effector.enabled = false;
    }

    void FixedUpdate() {
        float moveInput = Input.GetAxis("Vertical");
        float rotationInput = Input.GetAxis("Horizontal");

        float forwardForce = rb.drag * moveSpeed * rb.mass * moveInput;
        rb.AddForce(transform.up * forwardForce);

        float torque = -rb.angularDrag * Mathf.Deg2Rad * rotationSpeed * rb.inertia * rotationInput;
        rb.AddTorque(torque);

        float turretInput = Input.GetAxis("Horizontal II");
        turret.TurnInDirection(turretInput);
    }

    void Update() {
        bool fireInput = Input.GetButtonDown("Fire1");
        if (fireInput) {
            AttemptFire();
        }

        ManageRepel();

        Recharge();

        float healthPercentage = health.value / health.maxHealth;
        spriteRenderer.color = new Color(0, healthPercentage, 0);
    }

    void AttemptFire() {
        if (Energy >= fireEnergyCost) {
            Energy -= fireEnergyCost;
            turret.Fire(bulletSpeed);
        }
    }

    void ManageRepel() {
        if (Input.GetButtonDown("Fire2")) {
            effector.enabled = true;
        }
        if (Input.GetButtonUp("Fire2")) {
            effector.enabled = false;
        }
        if (effector.enabled) {
            if (Energy > Time.deltaTime) {
                Energy -= Time.deltaTime;
            } else {
                effector.enabled = false;
                GetComponent<Health>().Damage(1);
            }            
        }
    }

    void Recharge() {
        Energy += energyRechargeRate * Time.deltaTime;
        if (Energy > maxEnergy) {
            Energy = maxEnergy;
        }

        float percentage = Energy / maxEnergy;
        turretSpriteRenderer.color = new Color(0, 0, percentage);
    }
}

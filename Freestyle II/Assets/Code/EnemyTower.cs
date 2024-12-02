using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTower : MonoBehaviour
{
    public float bulletSpeed;
    public float fireCooldown;
    public float turnSpeed;
    public float aggroRange;

    private float lastFireTime = -Mathf.Infinity;
    private Turret turret;
    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        turret = GetComponentInChildren<Turret>();
        var homing = turret.bulletPrefab.GetComponent<Homing>();
        if (homing != null) {
            homing.target = target;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Aggro();
    }

    void Aggro() {
        if (target == null) {
            return;
        }

        Vector3 directionToTarget = target.transform.position - transform.position;

        if (directionToTarget.magnitude > aggroRange) {
            return;
        }

        directionToTarget.Normalize();
        float angleToTarget = Vector2.SignedAngle(turret.transform.up, directionToTarget);

        turret.TurnInDirection(-angleToTarget * turnSpeed);

        if (Time.time >= lastFireTime + fireCooldown) {
            lastFireTime = Time.time;
            turret.Fire(bulletSpeed);
        }
    }

}

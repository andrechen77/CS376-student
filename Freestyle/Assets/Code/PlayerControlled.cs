using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControlled : MonoBehaviour
{
    public float rotationSpeed = 200f;
    public float acceleration = 10f;
    public float maxDashSpeed = 40f;
    public float dashRecovery = 20f;
    public float brakingDrag = 5f;
    public float normalDrag;
    public AudioClip crashSound;
    public AudioClip dashSound;

    public static string rotateAxis = "Horizontal";
    public static string accelerateAxis = "Vertical";
    public static string followMouseButton = "Fire1";
    public static string dashButton = "Fire2";
    public static string brakeButton = "Fire3";

    private float lastDashTime = -Mathf.Infinity;
    private float dashSpeed {
        get {
            return Math.Min(maxDashSpeed, (Time.time - lastDashTime) * dashRecovery);
        }
    }

    private Rigidbody2D myRigidBody;
    private SpriteRenderer mySpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        normalDrag = myRigidBody.drag;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        UpdateColor();
    }

    void HandleMovement() {
        float followMouse = Input.GetAxis(followMouseButton);
        float accelerationInput = Math.Max(followMouse, Input.GetAxis(accelerateAxis));

        if (followMouse > 0f) {
            // rotate toward the mouse
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 directionToMouse = (mousePosition - transform.position).normalized;
            float angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));
        } else {
            // rotate according to the keyboard
            float rotationInput = Input.GetAxis(rotateAxis);
            float rotationAmount = rotationInput * rotationSpeed * Time.deltaTime;
            transform.Rotate(0, 0, -rotationAmount);
        }


        if (accelerationInput != 0) {
            myRigidBody.AddForce(accelerationInput * acceleration * transform.up, ForceMode2D.Force);
        }

        if (Input.GetButtonDown("Fire2")) {
            float attenuation = (maxDashSpeed - dashSpeed) / maxDashSpeed;
            myRigidBody.velocity = myRigidBody.velocity * attenuation;
            Vector2 additional = transform.up * dashSpeed;
            myRigidBody.velocity += additional;

            lastDashTime = Time.time;
            gameObject.GetComponent<AudioSource>().PlayOneShot(dashSound, 0.3f);
        }

        if (Input.GetButton("Fire3")) {
            myRigidBody.drag = brakingDrag;
        } else {
            myRigidBody.drag = normalDrag;
        }
    }

    void UpdateColor() {
        float percentage = dashSpeed / maxDashSpeed;
        mySpriteRenderer.color = new Color(percentage, 0, 0);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        GameMaster gameMaster = FindObjectOfType<GameMaster>();
        gameMaster.Score(-1f);
        gameObject.GetComponent<AudioSource>().PlayOneShot(crashSound, 0.3f);
    }
}

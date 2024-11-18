using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour {
    private Rigidbody2D myRigidBody;
    //private SpriteRenderer ballSpriteRenderer;

    // Start is called before the first frame update
    void Start() {
        myRigidBody = GetComponent<Rigidbody2D>();
        //ballSpriteRenderer = GetComponent<SpriteRenderer>(); // If you need to change ball's appearance
    }

    void OnCollisionEnter2D(Collision2D collision) {
    }
}

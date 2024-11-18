using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {
    public AudioClip scoreSound;

    void OnTriggerEnter2D(Collider2D collision) {
        Ball ball = collision.GetComponent<Ball>();
        if (ball != null) {
            Scored();
        }
    }

    void Scored() {
        GameMaster gameMaster = FindObjectOfType<GameMaster>();
        gameMaster.Score(10f);
        AudioSource.PlayClipAtPoint(scoreSound, transform.position, 10.0f);
        gameMaster.SpawnGoal();
        Destroy(gameObject);
    }
}

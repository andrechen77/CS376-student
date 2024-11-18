using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour {
    // Reference to the player's Transform
    public Transform player;
    // Smoothness of the camera movement
    public float smoothSpeed = 0.125f;
    // Offset to position the camera at a specific distance from the player
    public Vector3 offset = Vector3.zero;

    // Start is called before the first frame update
    void Start() {
        // You can either set the player manually in the Unity inspector
        // or find it by tag if you prefer
        if (player == null) {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void LateUpdate() {
        if (player == null) {
            return;
        }

        Vector3 desiredPosition = player.position + offset;
        desiredPosition.z = -10f;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.SetPositionAndRotation(desiredPosition, player.rotation);
    }
}

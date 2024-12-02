using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expires : MonoBehaviour
{
    public float remainingTime;

    void Update() {
        remainingTime -= Time.deltaTime;
        if (remainingTime <= 0) {
            Destroy(gameObject);
        }
    }
}

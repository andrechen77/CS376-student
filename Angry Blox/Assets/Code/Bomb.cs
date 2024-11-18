using UnityEngine;

public class Bomb : MonoBehaviour {
    public float ThresholdImpulse = 5;
    
    public GameObject ExplosionPrefab;


    private void Destruct() {
        Destroy(this.gameObject);
    }

    private void Boom() {
        this.gameObject.GetComponent<PointEffector2D>().enabled = true;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity, transform.parent);
        Invoke("Destruct", 0.1f);
    }

    internal void OnCollisionEnter2D(Collision2D collision) {
        foreach (ContactPoint2D contact in collision.contacts) {
            if (contact.normalImpulse > this.ThresholdImpulse) {
                this.Boom();
                break;
            }
        }
    }
}

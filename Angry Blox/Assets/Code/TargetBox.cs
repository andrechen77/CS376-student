using UnityEngine;

public class TargetBox : MonoBehaviour
{
    /// <summary>
    /// Targets that move past this point score automatically.
    /// </summary>
    public static float OffScreen;

    bool scored;

    internal void Start() {
        OffScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width-100, 0, 0)).x;
    }

    internal void Update()
    {
        if (transform.position.x > OffScreen)
            Scored();
    }

    private void Scored()
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        if (!this.scored) {
            this.scored = true;
            ScoreKeeper.AddToScore(this.gameObject.GetComponent<Rigidbody2D>().mass);
        }
    }

    internal void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            this.Scored();
        }
    }
}

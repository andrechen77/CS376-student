using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {
    public float timeToDeath = 60f;
    public TextMeshProUGUI ttdText;
    public float nextGoalSize = 20f;
    public float difficultyScale = 0.9f;
    public int numGoals = 3;
    public GameObject goalPrefab;

    private bool won {
        get {
            return nextGoalSize <= 1f;
        }
    }


    void Start() {
        UpdateScoreText();
        for (int i = 0; i < numGoals; i++) {
            SpawnGoal();
        }
    }

    void Update() {
        // reload on Esc
        if (Input.GetKeyDown(KeyCode.Escape)) {
            ReloadLevel();
        }

        // reduce the time to death
        if (timeToDeath > 0 && !won) {
            timeToDeath -= Time.deltaTime;
        }

        // check for death
        if (timeToDeath < 0 && !won) {
            timeToDeath = 0;
            FindAnyObjectByType<PlayerControlled>().enabled = false;
        }

        UpdateScoreText();
    }

    void ReloadLevel() {
        // Reload the active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Score(float delta) {
        if (timeToDeath > 0 && !won) {
            timeToDeath += delta;
            UpdateScoreText();
        }
    }

    private void UpdateScoreText() {
        if (ttdText == null) {
            return;
        }
        if (timeToDeath == 0) {
            ttdText.text = "You ran out of time!";
        } else if (won) {
            ttdText.text = string.Format("You won! final score: {0:00.0}", timeToDeath);
        } else {
            ttdText.text = string.Format("time till death: {0:00.0}", timeToDeath);
        }
    }

    public void SpawnGoal() {
        float minX = GameObject.Find("left wall").transform.position.x + nextGoalSize;
        float maxX = GameObject.Find("right wall").transform.position.x - nextGoalSize;
        float minY = GameObject.Find("bottom wall").transform.position.y + nextGoalSize;
        float maxY = GameObject.Find("top wall").transform.position.y - nextGoalSize;

        Vector2 spawnPosition = new Vector2(
            Random.Range(minX, maxX),
            Random.Range(minY, maxY)
        );
        Quaternion spawnRotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));

        GameObject goal = Instantiate(goalPrefab, spawnPosition, spawnRotation);
        var scale = goal.transform.localScale;
        scale.y = nextGoalSize;
        goal.transform.localScale = scale;

        nextGoalSize *= difficultyScale;
        
        if (nextGoalSize < 1f) {
            nextGoalSize = 1f;
        }
    }
}

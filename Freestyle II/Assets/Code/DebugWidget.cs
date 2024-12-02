using UnityEngine;
using UnityEngine.UI; // For standard UI Text
 using TMPro; // Uncomment if using TextMeshPro

public class DebugWidget : MonoBehaviour {
    public string axisName = "Horizontal"; // Input axis to display
    //public Text debugText; // Reference to the UI Text component
     public TextMeshProUGUI debugText; // Uncomment if using TextMeshPro

    void Start() {
        // Auto-find the Text component if not set
        if (debugText == null) {
            debugText = gameObject.GetComponent<TextMeshProUGUI>();
            // debugText = GetComponent<TextMeshProUGUI>(); // Uncomment for TextMeshPro
        }
    }

    void Update() {
        // Get the axis value
        float axisValue = Input.GetAxis(axisName);

        // Update the text with the axis value
        debugText.text = $"{axisName} Value: {axisValue:F2}";
    }
}

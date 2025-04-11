using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HabitItemController : MonoBehaviour
{
    public TMP_Text habitNameText; // Text component to display the habit name
    public Toggle completeToggle; // Toggle component to mark the habit as complete
    public Button deleteButton; // Button component to delete the habit
    public Image backgroundImage; // Image component for the background of the habit item

    private Color originalColor; // Original color of the background image
    public Color completedColor; // Color to use when the habit is completed

    void Start()
    {
        // Initialize the habit item with default values
        if (completeToggle != null)
            completeToggle.onValueChanged.AddListener(OnToggleValueChanged);

        // Add listener for the delete button       
        if (deleteButton != null)
            deleteButton.onClick.AddListener(OnDeleteClicked);
        
        // Store the original color of the background image
        if (backgroundImage != null)
            originalColor = backgroundImage.color;
    }

    void OnToggleValueChanged(bool isOn)
    {
        if (backgroundImage != null)
            backgroundImage.color = isOn ? completedColor : originalColor;
    }

    void OnDeleteClicked()
    {
        Destroy(gameObject); // Destroy the habit item game object
    }
}

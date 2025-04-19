using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HabitItemController : MonoBehaviour
{
    public TMP_Text habitNameText; // Text component to display the habit name
    public TMP_Text habitFrequencyText; // Text component to display the habit frequency
    public Toggle completeToggle; // Toggle component to mark the habit as complete
    public Button deleteButton; // Button component to delete the habit
    public Image backgroundImage; // Image component for the background of the habit item

    private Color originalColor; // Original color of the background image
    public Color completedColor; // Color to use when the habit is completed

    // Method to initialize the habit item
    // This method is called when the script instance is being loaded
    // It sets up the toggle and delete button listeners
    public void Start()
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

    // Method to handle the toggle value change
    // This method is called when the toggle button is clicked
    // It updates the background color based on the completion status
    // It changes the background color to completedColor if the habit is completed
    public void OnToggleValueChanged(bool isOn)
    {
        if (backgroundImage != null)
            backgroundImage.color = isOn ? completedColor : originalColor;
    }

    // Method to handle the delete button click
    // This method is called when the delete button is clicked
    // It destroys the habit item game objectx
    public void OnDeleteClicked()
    {
        Destroy(gameObject); // Destroy the habit item game object
    }

    // Method to set the habit frequency text
    // This method is called to update the frequency text in the UI

    public void SetFrequency(string frequency)
    {
        if (habitFrequencyText != null)
            habitFrequencyText.text = frequency; // Set the habit frequency text
    }
}

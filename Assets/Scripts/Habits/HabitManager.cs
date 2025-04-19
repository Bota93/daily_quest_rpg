using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class HabitManager : MonoBehaviour
{
    [Header("Prefabs and UI References")]
    public GameObject habitItemPrefab; // Prefab for habit item
    public Transform habitListContent; // Parent object for habit items
    public GameObject addHabiPanel; // Panel for adding new habit
    public TMP_InputField habitInputField; // Input field for new habit
    public TMP_Dropdown habitFrequencyDropdown; // Dropdown for habit frequency

    private List<Habit> habitList = new List<Habit>(); // List to store habits
    private string habitFilePath => Path.Combine(Application.persistentDataPath, "habits.json"); // File path for saving habits

    // Method to load habits from file
    // This method is called at the start of the game
    private void Start()
    {
        LoadHabits(); // Load habits from file at the start
    }

     // Method to open the add habit panel
     // This method is called when the user clicks the "Add habit" button
    public void OpenAddHabitPanel()
    {
        addHabiPanel.SetActive(true); // Show the add habit panel
    }

    // Method to close the add habit panel
    // This method is called when the user clicks the "Close" button on the add habit panel
    public void CloseAddHabitPanel()
    {
        addHabiPanel.SetActive(false); // Hide the add habit panel
    }

    // Method to create a habit UI element
    // This method is called when a new habit is added
    // This method instantiates a new habit item prefab and sets its parent to the habit list content    
    public void ConfirmAddHabit()
    {
        string name = habitInputField.text.Trim(); // Get the text from the input field
        string frequency = habitFrequencyDropdown.options[habitFrequencyDropdown.value].text; // Get the selected frequency from the dropdown
        Debug.Log("Habit Frequency: " + frequency); // Log the selected frequency

        // Check if the input is not empty
        if (!string.IsNullOrEmpty(name))
        {
            Habit newHabit = new Habit(name, frequency); // Create a new habit object
            habitList.Add(newHabit); // Add the new habit to the habit list

            CreateHabitUI(newHabit); // Create the UI for the new habit
            SaveHabits(); // Save the updated habit list to file

            habitInputField.text = ""; // Clear the input field
            CloseAddHabitPanel(); // Close the add habit panel
        }
    }

    // Method to add a new habit
    // This method is called when the user clicks the "Add" button on the add habit panel
    // This method creates a new habit item in the UI and saves it to the habit list
    public void AddHabit()
    {
        string name = habitInputField.text.Trim(); // Get the text from the input field

        //Check if the input is not empty
        if(!string.IsNullOrEmpty(name))
        {
            // Instantiate the habit item prefab and set its parent to the habit list content
            GameObject newHabit = Instantiate(habitItemPrefab, habitListContent);
            TMP_Text textComponent = newHabit.GetComponentInChildren<TMP_Text>(); // Get the text component in the habit item prefab

            // Set the habit name in the new habit item
            newHabit.GetComponentInChildren<TMP_Text>().text = name;

            string frequency = habitFrequencyDropdown.options[habitFrequencyDropdown.value].text; // Get the selected frequency from the dropdown
            Debug.Log("Habit Frequency: " + frequency); // Log the selected frequency


            habitInputField.text = ""; // Clear the inputfield after adding the habit
            CloseAddHabitPanel(); // Close the add habit panel

        }
    }

    // Method to load habits from file
    // This method reads the habit list from a JSON file and populates the habit list
    public void LoadHabits()
    {
        // Check if the file exists
        if (File.Exists(habitFilePath))
        {
            string json = File.ReadAllText(habitFilePath); // Read the JSON file
            HabitListWrapper wrapper = JsonUtility.FromJson<HabitListWrapper>(json); // Deserialize the JSON into a wrapper object
            if (wrapper != null && wrapper.habits != null)
            {
                habitList = wrapper.habits; // Assign the loaded habits to the habit list
                foreach (Habit habit in habitList)
                {
                    CreateHabitUI(habit); // Create the UI for each loaded habit
                }
            }
        }
    }

    // Method to save habits to file
    // This method serializes the habit list to a JSON file
    // This method is called when the habit list is updated
    public void SaveHabits()
    {
        string json = JsonUtility.ToJson(new HabitListWrapper(habitList)); // Serialize the habit list to JSON
        File.WriteAllText(habitFilePath, json); // Write the JSON to file
        Debug.Log("Habits saved to " + habitFilePath); // Log the save status
    }

    // Method to create a habit UI element
    // This method instantiates a new habit item prefab and sets its parent to the habit list content
    // This method is called when a new habit is added
    // This method sets the habit name, frequency, and completion status in the UI
    void CreateHabitUI (Habit habit)
    {
        GameObject newHabitGO = Instantiate(habitItemPrefab, habitListContent); // Instantiate the habit item prefab
        HabitItemController controller = newHabitGO.GetComponent<HabitItemController>(); // Get the controller for the habit item

        if (controller != null)
        {
            controller.habitNameText.text = habit.name; // Set the habit name in the UI
            controller.SetFrequency(habit.frequency); // Set the habit frequency in the UI
            controller.completeToggle.isOn = habit.isCompleted; // Set the completion status in the UI

            // Add listener for the toggle button
            // This will be called when the toggle button is clicked
            controller.completeToggle.onValueChanged.AddListener((isOn) =>
            {
                habit.isCompleted = isOn;
                SaveHabits(); // Save the updated habit list to file
                Debug.Log("Habit " + habit.name + " completed: " + isOn); // Log the completion status
            });

            controller.deleteButton.onClick.AddListener(() =>
            {
                habitList.Remove(habit); //Remove the habit from the list
                Destroy(newHabitGO); // Destroy the habit item UI
                SaveHabits(); // Save the updated habit list to file
                Debug.Log("Habit " + habit.name + " deleted"); // Log the deletion status
            });
        }
    }
}

[System.Serializable]

// This class is used to wrap the list of habits for JSON serialization
// It contains a list of Habit objects
public class HabitListWrapper
{
    public List<Habit> habits; // List of habits
    
    public HabitListWrapper(List<Habit> habits)
    {
        this.habits = habits; // Initialize the habit list
    }
}

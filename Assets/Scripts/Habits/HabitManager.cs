using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HabtiManager : MonoBehaviour
{
    [Header("Prefabs and UI References")]
    public GameObject habitItemPrefab; // Prefab for habit item
    public Transform habitListContent; // Parent object for habit items
    public GameObject addHabiPanel; // Panel for adding new habit
    public TMP_InputField habitInputField; // Input field for new habit
    public TMP_Dropdown habitFrequencyDropdown; // Dropdown for habit frequency

    public void OpenAddHabitPanel()
    {
        addHabiPanel.SetActive(true); // Show the add habit panel
    }

    public void CloseAddHabitPanel()
    {
        addHabiPanel.SetActive(false); // Hide the add habit panel
    }

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
}

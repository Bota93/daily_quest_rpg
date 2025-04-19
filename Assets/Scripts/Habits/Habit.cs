using UnityEngine;
using System;

[Serializable]
public class Habit
{
    public string name; //Habit name
    public string frequency; //Habit frequency
    public bool isCompleted; //Is the habit completed?

    // Constructor to initialize the habit
    public Habit (string name, string frequency)
    {
        this.name = name;
        this.frequency = frequency;
        this.isCompleted = false;
    }
}

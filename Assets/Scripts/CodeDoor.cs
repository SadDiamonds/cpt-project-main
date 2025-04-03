using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class CodeDoor : MonoBehaviour
{
    public List<string> correctCode = new List<string> { "A", "B", "C" }; // Correct sequence
    private List<string> enteredCode = new List<string>(); // Stores player's input

    public Animator doorController; // Reference to the door
    public GameObject crosshair; // UI Crosshair
    public TextMeshPro displayText; // 3D TextMeshPro object for code display

    private void Start()
    {
        if (crosshair != null)
        {
            crosshair.SetActive(false); // Hide crosshair at start
        }

        UpdateDisplay(); // Initialize display
    }

    public void PressButton(string buttonID)
    {
        enteredCode.Add(buttonID);
        UpdateDisplay(); // Update the 3D Text Display

        Debug.Log("Entered Code: " + string.Join("", enteredCode));

        if (enteredCode.Count == correctCode.Count)
        {
            if (CheckCodeMatch())
            {
                Debug.Log(" Correct Code! Door Opens.");
                doorController.SetTrigger("Open");
                displayText.text = "ACCESS GRANTED"; // Change text to success
            }
            else
            {
                Debug.Log(" Incorrect Code! Resetting...");
                enteredCode.Clear(); // Reset input
                UpdateDisplay(); // Reset display text
            }
        }
    }

    private bool CheckCodeMatch()
    {
        for (int i = 0; i < correctCode.Count; i++)
        {
            if (enteredCode[i] != correctCode[i])
            {
                return false;
            }
        }
        return true;
    }

    private void UpdateDisplay()
    {
        if (displayText != null)
        {
            if (enteredCode.Count > 0)
                displayText.text = string.Join("", enteredCode); // Show entered code
            else
                displayText.text = "ENTER CODE"; // Default message
        }
    }

    public void ShowCrosshair(bool show)
    {
        if (crosshair != null)
        {
            crosshair.SetActive(show);
        }
    }
}

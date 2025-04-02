using UnityEngine;
using TMPro; // Import TextMeshPro for 3D text

public class DoorController : MonoBehaviour
{
    private int buttonsPressed = 0;
    public int totalButtons = 3;
    public TextMeshPro buttonCounterText; // Reference to the 3D sign text
    public Lever lever; // Reference to the lever
    public Animator doorAnimator; // Reference to the door's Animator
    public string doorOpenTrigger = "Open"; // Name of the trigger parameter in Animator
    private bool isDoorOpen = false;

    private void Start()
    {
        UpdateSign(); // Set initial text
    }

    public void ButtonPressed()
    {
        buttonsPressed++;
        UpdateSign(); // Update the 3D sign
        if (buttonsPressed >= totalButtons)
        {
            lever.UnlockLever(); // Allow lever to be used
        }
    }

    private void UpdateSign()
    {
        if (buttonCounterText != null)
        {
            buttonCounterText.text = buttonsPressed + "/" + totalButtons + " Buttons Pressed";
        }
    }

    public void OpenDoor()
    {
        if (!isDoorOpen)
        {
            isDoorOpen = true;
            if (doorAnimator != null)
            {
                doorAnimator.SetTrigger(doorOpenTrigger); // Trigger the door opening animation
            }
            else
            {
                Debug.LogError("Door Animator is not assigned!");
            }
        }
    }
}

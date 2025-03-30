using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Animator doorAnimator;  // Assign in Inspector
    private bool isOpen = false;   // Track door state

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isOpen) // Open only if it's not already open
        {
            doorAnimator.SetBool("IsOpen", true);  // Start opening the door
            isOpen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Close when player leaves
        {
            doorAnimator.SetBool("IsOpen", false);
            isOpen = false;
        }
    }
}

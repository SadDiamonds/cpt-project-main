using UnityEngine;

public class InteractableButton : MonoBehaviour
{
    private Renderer buttonRenderer;
    private Animator buttonAnimator; // Animator reference
    public bool isActivated = false;
    private bool playerNearby = false;
    public DoorController doorController; // Reference to the door

    private void Start()
    {
        buttonRenderer = GetComponent<Renderer>(); // Find Renderer
        buttonAnimator = GetComponent<Animator>(); // Find Animator

        if (buttonRenderer == null)
        {
            Debug.LogError("No Renderer found on " + gameObject.name + ". Make sure the object has a Mesh Renderer.");
        }
        if (buttonAnimator == null)
        {
            Debug.LogError("No Animator found on " + gameObject.name + ". Make sure it has an Animator.");
        }

        // Make sure button starts unpressed and green
        buttonRenderer.material.color = Color.white; // Set button color back to normal
    }


    private void Update()
    {
        if (playerNearby && !isActivated && Input.GetKeyDown(KeyCode.E))
        {
            ActivateButton();
        }
    }

    private void ActivateButton()
    {
        isActivated = true;
        buttonAnimator.Play("ButtonPress"); // Play animation
        doorController.ButtonPressed(); // Notify door
        buttonRenderer.material.color = Color.green; // Change to green
        Debug.Log("Button Pressed!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }
}

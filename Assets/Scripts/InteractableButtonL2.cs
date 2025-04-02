using UnityEngine;
using TMPro;

public class InteractableButton : MonoBehaviour
{
    private Renderer buttonRenderer;
    private Animator buttonAnimator;
    public bool isActivated = false;
    private bool playerNearby = false;
    public TextMeshProUGUI interactText;
    public DoorController doorController;

    public Transform buttonPart; // Assign in Inspector (button object with Animator)

    private void Start()
    {
        interactText.gameObject.SetActive(false);

        // Get Renderer and Animator from buttonPart
        if (buttonPart != null)
        {
            buttonRenderer = buttonPart.GetComponent<Renderer>();
            buttonAnimator = buttonPart.GetComponent<Animator>();
        }

        if (buttonRenderer == null)
        {
            Debug.LogError("No Renderer found on " + buttonPart.name + ". Make sure it has a Mesh Renderer.");
        }
        if (buttonAnimator == null)
        {
            Debug.LogError("No Animator found on " + buttonPart.name + ". Make sure it has an Animator.");
        }

        // Make sure button starts unpressed and white
        if (buttonRenderer != null)
        {
            buttonRenderer.material.color = Color.white;
        }
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

        // Debug Log to check if animation is triggered
        Debug.Log("Trying to Play Animation on " + buttonPart.name);

        if (buttonAnimator != null)
        {
            // Play animation directly (ensure animation name matches the one in Animator)
            buttonAnimator.Play("ButtonDownAnim", 0, 0f); // Force play from start

            // Alternative: If using trigger-based transitions in Animator
            // buttonAnimator.SetTrigger("Press");
        }
        else
        {
            Debug.LogError("Animator not found on buttonPart!");
        }

        if (doorController != null)
        {
            doorController.ButtonPressed(); // Notify door
        }

        if (buttonRenderer != null)
        {
            buttonRenderer.material.color = Color.green; // Change button color
        }

        Debug.Log("Button Pressed!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            interactText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            interactText.gameObject.SetActive(false);
        }
    }
}

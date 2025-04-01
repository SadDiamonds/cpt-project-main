using UnityEngine;
using TMPro;

public class Lever : MonoBehaviour
{
    public TextMeshProUGUI interactText;  // UI prompt for interaction
    private bool playerInRange = false;
    private bool canUseLever = false;
    private bool isActivated = false;

    private void Start()
    {
        interactText.gameObject.SetActive(false); // Hide UI at start
    }

    public void UnlockLever()
    {
        canUseLever = true;
    }

    private void Update()
    {
        if (playerInRange && canUseLever && !isActivated && Input.GetKeyDown(KeyCode.E))
        {
            PullLever();
        }
    }

    private void PullLever()
    {
        isActivated = true;
        transform.Rotate(0, 0, -45); // Rotate lever down
        interactText.gameObject.SetActive(false); // Hide UI after activation
        Debug.Log("Lever Pulled!");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered the trigger: " + other.name); // Debugging
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected in range of the lever!");
            playerInRange = true;
            if (canUseLever && !isActivated)
            {
                interactText.gameObject.SetActive(true); // Show UI prompt
                Debug.Log("Interaction UI should be visible now.");
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            interactText.gameObject.SetActive(false); // Hide UI prompt
        }
    }
}

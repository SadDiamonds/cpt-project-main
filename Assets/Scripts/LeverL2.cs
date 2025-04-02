using UnityEngine;
using TMPro;

public class Lever : MonoBehaviour
{
    public TextMeshProUGUI interactText;
    private bool playerInRange = false;
    private bool canUseLever = false;
    private bool isActivated = false;
    public Animator leverAnimator;  // Assign lever's Animator (not empty parent)
    public DoorController doorController; // Reference to the door

    private void Start()
    {
        interactText.gameObject.SetActive(false);
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
        interactText.gameObject.SetActive(false);

        if (leverAnimator != null)
        {
            
            leverAnimator.SetTrigger("Pull");  // Play lever animation
            Invoke("OpenDoorAfterAnimation", 1.5f); // Delay opening door (adjust time)
        }
        
    }

    private void OpenDoorAfterAnimation()
    {
        doorController.OpenDoor(); // Calls door-opening function
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (canUseLever && !isActivated)
            {
                interactText.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            interactText.gameObject.SetActive(false);
        }
    }
}

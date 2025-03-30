using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class ElevatorButtonUI : MonoBehaviour
{
    public Transform elevator;   // Elevator reference
    public Transform button;     // Button reference
    public float moveDistance = 10f; // Distance to move up
    public float speed = 5f;     // Movement speed
    public TextMeshProUGUI interactText;  // TMP UI text

    private bool playerInRange = false;  // Check if the player is near
    private bool isMoving = false;       // Prevents multiple triggers

    private void Start()
    {
        interactText.gameObject.SetActive(false);  // Hide text initially
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !isMoving)  // Press E to activate
        {
            isMoving = true;
            StartCoroutine(MoveObjects());  // Move both button and elevator
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            interactText.gameObject.SetActive(true);  // Show the UI prompt
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            interactText.gameObject.SetActive(false);  // Hide the prompt
        }
    }

    private IEnumerator MoveObjects()
    {
        float targetHeight = elevator.position.y + moveDistance;  // Elevator target position
        float buttonTargetHeight = button.position.y + moveDistance; // Button target position

        while (elevator.position.y < targetHeight)
        {
            // Move the elevator and button up
            elevator.position = Vector3.MoveTowards(elevator.position, new Vector3(elevator.position.x, targetHeight, elevator.position.z), speed * Time.deltaTime);
            button.position = Vector3.MoveTowards(button.position, new Vector3(button.position.x, buttonTargetHeight, button.position.z), speed * Time.deltaTime);

            yield return null;  // Wait for the next frame
        }
    }
}

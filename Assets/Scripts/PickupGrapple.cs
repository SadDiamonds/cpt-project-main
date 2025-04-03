using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplePickup : MonoBehaviour
{
    [Header("References")]
    public GameObject grappleUI;  // UI prompt for pickup
    public GameObject playerGrappleObject;  // The grapple object in PlayerCam
    public Grappling grapplingScript;  // Grappling script attached to Player
    public Swinging swingingScript;  // Swinging script attached to Player
    public Transform predictionPoint;  // The prediction point object (circle)
    public Camera cam;

    private bool isPlayerInRange = false;

    private void Start()
    {
        // Disable the grapple UI and player grapple object at the start
        if (grappleUI != null) grappleUI.SetActive(false);
        if (playerGrappleObject != null) playerGrappleObject.SetActive(false);
        if (predictionPoint != null) predictionPoint.gameObject.SetActive(false);  // Prediction circle off
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            grappleUI.SetActive(true);  // Show the pickup prompt
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            grappleUI.SetActive(false);  // Hide the pickup prompt
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            PickupGrapple();
        }
    }

    private void PickupGrapple()
    {
        Debug.Log("Player picked up the grapple!");

        // Enable the grapple object in the player's hand
        if (playerGrappleObject != null)
        {
            playerGrappleObject.SetActive(true);
        }
        else
        {
            Debug.LogError("ERROR: No player grapple object assigned in the Inspector!");
        }

        // Set the hasGrapple bool to true in both the Grappling and Swinging scripts
        if (grapplingScript != null)
        {
            grapplingScript.hasGrapple = true;  // Enable grapple ability
        }
        else
        {
            Debug.LogError("ERROR: Grappling script is not assigned in the Inspector!");
        }

        if (swingingScript != null)
        {
            swingingScript.hasGrapple = true;  // Enable swinging ability
        }
        else
        {
            Debug.LogError("ERROR: Swinging script is not assigned in the Inspector!");
        }

        // Enable prediction circle for grapple
        if (predictionPoint != null)
        {
            predictionPoint.gameObject.SetActive(true);  // Enable prediction circle
        }

        // Hide the pickup UI and destroy the pickup object
        grappleUI.SetActive(false);
        Destroy(gameObject);
    }
}

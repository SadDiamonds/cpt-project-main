using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliding : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform playerObj;
    private Rigidbody rb;
    private PlayerMovementAdvanced pm;
    private CapsuleCollider playerCollider;

    [Header("Sliding")]
    public float maxSlideTime = 1.5f;
    public float slideForce = 10f;
    private float slideTimer;

    public float slideYScale = 0.5f; // Adjust to reasonable crouch height
    private float startYScale;
    private float startColliderHeight;

    [Header("Input")]
    public KeyCode slideKey = KeyCode.LeftControl;
    private float horizontalInput;
    private float verticalInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovementAdvanced>();
        playerCollider = GetComponent<CapsuleCollider>();

        startYScale = playerObj.localScale.y;
        startColliderHeight = playerCollider.height;
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(slideKey) && (horizontalInput != 0 || verticalInput != 0))
            StartSlide();

        if (Input.GetKeyUp(slideKey) && pm.sliding)
            StopSlide();
    }

    private void FixedUpdate()
    {
        if (pm.sliding)
            SlidingMovement();
    }

    private void StartSlide()
    {
        pm.sliding = true;
        slideTimer = maxSlideTime;

        // Move player up slightly before shrinking to prevent clipping
        transform.position += Vector3.up * 0.1f;

        // Shrink the collider instead of scaling the object
        playerCollider.height = startColliderHeight * 0.5f;

        // Apply downward force to maintain ground contact
        rb.AddForce(Vector3.down * 10f, ForceMode.Impulse);
    }

    private void SlidingMovement()
    {
        Vector3 inputDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (!pm.OnSlope() || rb.velocity.y > -0.1f)
        {
            rb.AddForce(inputDirection.normalized * slideForce, ForceMode.Force);
            slideTimer -= Time.deltaTime;
        }
        else
        {
            rb.AddForce(pm.GetSlopeMoveDirection(inputDirection) * slideForce, ForceMode.Force);
        }

        if (slideTimer <= 0)
            StopSlide();
    }

    private void StopSlide()
    {
        pm.sliding = false;

        // Reset collider height
        playerCollider.height = startColliderHeight;
    }
}

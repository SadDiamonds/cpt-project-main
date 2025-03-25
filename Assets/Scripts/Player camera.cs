using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerCam : MonoBehaviour
{
    public static PlayerCam Instance;  // Singleton reference

    [Header("Sensitivity")]
    public float sensX;
    public float sensY;

    [Header("References")]
    public Transform orientation;
    public Camera cam;

    private float xRotation;
    private float yRotation;
    private float zTilt = 0f; // Store current Z tilt separately

    private PlayerMovementAdvanced playerMovement;
    private float baseFOV;
    public float sprintFOVMultiplier = 1.5f;

    private void Awake()
    {
        Instance = this; // Assign singleton
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Get player movement script
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovementAdvanced>();

        // Store base FOV at start
        baseFOV = cam.fieldOfView;
    }

    private void Update()
    {
        HandleMouseLook();
        AdjustFOV();
    }

    private void HandleMouseLook()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;

        // Clamp vertical rotation
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Combine tilt and mouse rotations here
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, zTilt);

        // Rotate orientation around Y for movement
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    private void AdjustFOV()
    {
        if (playerMovement == null) return;

        float speed = playerMovement.rb.velocity.magnitude;

        // Calculate target FOV based on speed
        float targetFOV = baseFOV + (speed * sprintFOVMultiplier);
        targetFOV = Mathf.Clamp(targetFOV, baseFOV, baseFOV * 1.5f);

        // Smooth transition of FOV
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * 5f);
    }

    public void DoFov(float endValue)
    {
        cam.DOFieldOfView(endValue, 0.25f);
    }

    public void DoTilt(float targetZTilt)
    {
        // Animate zTilt value using DOTween and apply it in HandleMouseLook
        DOTween.To(() => zTilt, x => zTilt = x, targetZTilt, 0.25f);
    }
}

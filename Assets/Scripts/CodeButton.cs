using UnityEngine;

public class CodeButton : MonoBehaviour
{
    public string buttonID; // Unique button ID
    private CodeDoor codeDoor;
    private Outline outline; // Reference to outline component

    private void Start()
    {
        codeDoor = FindObjectOfType<CodeDoor>(); // Find CodeDoor script
        outline = GetComponent<Outline>(); // Get Outline component

        if (outline != null)
        {
            outline.enabled = false; // Disable outline by default
        }
    }

    private void OnMouseEnter()
    {
        codeDoor.ShowCrosshair(true); // Show crosshair

        if (outline != null)
        {
            outline.enabled = true; // Enable outline on hover
        }
    }

    private void OnMouseExit()
    {
        codeDoor.ShowCrosshair(false); // Hide crosshair

        if (outline != null)
        {
            outline.enabled = false; // Disable outline when not hovering
        }
    }

    private void OnMouseDown()
    {
        codeDoor.PressButton(buttonID);
    }
}

using UnityEngine;

public class GridPlacement : MonoBehaviour
{
    public float gridSize = 1f; // Size of each grid cell
    
    // Method to snap an object to the grid
    public Vector3 SnapToGrid(Vector3 position)
    {
        // Snap the X and Z position to the nearest grid point
        float x = Mathf.Round(position.x / gridSize) * gridSize;
        float y = -0.5f;  // Keep the Y position fixed to -1 (ground level)
        float z = Mathf.Round(position.z / gridSize) * gridSize;

        return new Vector3(x, y, z);
    }

}

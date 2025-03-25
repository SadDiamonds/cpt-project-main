using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridPlacement))]
public class GridEditor : Editor
{
    // Reference to the GridPlacement script
    private GridPlacement gridPlacement;

    void OnEnable()
    {
        gridPlacement = (GridPlacement)target;
    }

    void OnSceneGUI()
    {
        // Get the current position of the selected object
        Transform objectTransform = ((GridPlacement)target).transform;
        
        // Get the snapped position using the SnapToGrid method
        Vector3 snappedPosition = gridPlacement.SnapToGrid(objectTransform.position);

        // Draw a handle to show the grid-snap position in the Scene view
        Handles.color = Color.green;
        Handles.DrawWireCube(snappedPosition, Vector3.one * gridPlacement.gridSize);

        // Allow the user to drag the object and snap it to the grid
        EditorGUI.BeginChangeCheck();
        Vector3 newPosition = Handles.PositionHandle(snappedPosition, Quaternion.identity);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(objectTransform, "Move Object");
            objectTransform.position = newPosition; // Apply the new position
        }
    }
}

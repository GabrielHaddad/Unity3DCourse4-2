using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color occupiedColor = Color.red;
    [SerializeField] Color availableColor = Color.white;
    [SerializeField] Color exploredColor = new Color(128,0,128);
    [SerializeField] Color pathColor = Color.blue;
    
    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    GridManager gridManager;

    void Awake() 
    {
        label = GetComponent<TextMeshPro>();
        label.enabled = false;

        gridManager = FindObjectOfType<GridManager>();

        DisplayCoordinates();
    }
   
    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
            label.enabled = true;
        }

        UpdateTextColor();
        ToggleLabels();
    }

    void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }

    void DisplayCoordinates()
    {
        if (gridManager == null) { return; }

        int unityGridSize = gridManager.UnityGridSize;
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / unityGridSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / unityGridSize);

        label.text = $"{coordinates.x},{coordinates.y}";
    }

    void UpdateTextColor()
    {
        if (gridManager == null) { return; }

        Node node = gridManager.GetNode(coordinates);

        if (node == null) { return; }

        if (!node.isWalkable)
        {
            label.color = occupiedColor;
        }
        else if (node.isPath)
        {
            label.color = pathColor;
        }
        else if (node.isExplored)
        {
            label.color = exploredColor;
        }
        else
        {
            label.color = availableColor;
        }
    }

    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }
}

using UnityEngine;
using UnityEngine.UI;

public class CrossHairSekimas : MonoBehaviour
{
    private RectTransform crosshairRectTransform;
    private RectTransform canvasRectTransform;

    void Start()
    {
        crosshairRectTransform = GetComponent<RectTransform>();

        // Find the UI Canvas
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas != null)
        {
            canvasRectTransform = canvas.GetComponent<RectTransform>();
        }
        else
        {
            Debug.LogError("UI Canvas not found!");
        }
    }

    void Update()
    {
        if (canvasRectTransform == null || crosshairRectTransform == null)
            return;

        // Set the crosshair's position to the mouse position
        crosshairRectTransform.position = Input.mousePosition;
    }
}

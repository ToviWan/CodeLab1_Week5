using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectVisibility : MonoBehaviour
{
    //GameObject indicatorPrefab;
    private GameObject indicator;
    private Camera mainCamera;
    private Canvas canvas;
    //private bool readPosition;

    void Start()
    {
        // Create the indicator and set its parent to the Canvas
        canvas = FindObjectOfType<Canvas>(); // Find the Canvas object in the scene
        if (canvas != null)
        {
            //indicator = Instantiate(indicatorPrefab, canvas.transform); // Instantiate the indicator prefab and set its parent to the Canvas
            indicator = canvas.transform.GetChild(0).gameObject;
            indicator.SetActive(false); // Hide the indicator by default
        }

        // Get a reference to the main camera
        mainCamera = Camera.main;
    }

    void Update()
    {
       
        // Check if the object is out of view
        if (!IsInView())
        {
            

            // Show the indicator
            if (indicator != null)
            {
                indicator.SetActive(true);

                // Point the indicator towards the object
                Vector2 screenPoint = mainCamera.WorldToScreenPoint(transform.position); // Get the position of the object on the screen
                Vector2 dir = (screenPoint - (Vector2)indicator.transform.position).normalized; // Calculate the direction between the indicator and the object
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg; // Calculate the angle between the indicator and the object
                indicator.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); // Set the rotation of the indicator to the calculated angle
            }
        }
        else
        {
            // Hide the indicator
            if (indicator != null)
            {
                indicator.SetActive(false);
            }
        }
    }

    bool IsInView()
    {
        Vector2 screenPoint = mainCamera.WorldToViewportPoint(transform.position); // Get the position of the object in the viewport
        return screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1 && screenPoint.y > 0; // Check if the object is within the view of the camera
    }
}
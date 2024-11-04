using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


// This script is used to modify the viewing of a camera based on the aspect ratio of the screen.
// The script will "shrink" the view to ensure that all of the playable space fits
//
// This is currently only implemented for Orthographic camera
// If the aspect ratio of the screen 
// - is wider than the ideal, then the game is shrunk vertically to fit, which means extra space will appear on the left and right
// - is narrower than the ideal, then the game is shrunk horizontally to fit, which means extra space will appear on the top and bottom. 

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class CameraAspect : MonoBehaviour
{
    public enum ResizeSettings
    {
        None,
        ChangeHeightToMaximizeWidth,
        ChangeWidthToMaximizeHeight,
    }
    public ResizeSettings resizeSetting = ResizeSettings.None;

    public float IdealHorizontalSize = 5.0f;
    public float IdealVerticalSize = 5.0f;

    public float ZoomFactor = 1.0f;

    public bool AlignViewportToTopOfScreen = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void UpdateCameraSizing()
    {
        Camera cam = GetComponent<Camera>();

        if (cam.orthographic)
        {
            float IdealWidthToHeight = IdealHorizontalSize / IdealVerticalSize;
            float screenRatio = (float)Screen.width / (float)Screen.height;

            if (resizeSetting == ResizeSettings.ChangeHeightToMaximizeWidth)
            {
                Rect currentRect = cam.rect;

                // our height is our width divided by our ideal ratio multiplied by the screen's aspect ratio since the rect co-ordinates are in absolute scaled values from 0..1
                currentRect.height = cam.rect.width / IdealWidthToHeight * screenRatio;

                if (AlignViewportToTopOfScreen)
                    currentRect.y = 1.0f - currentRect.height;

                cam.rect = currentRect;
            }
            else if (resizeSetting == ResizeSettings.ChangeWidthToMaximizeHeight)
            {
                Rect currentRect = cam.rect;
                
                // our width is our height times our ideal ratio divided by the screen's aspect ratio since the rect co-ordinates are in absolute scaled values from 0..1
                currentRect.width = cam.rect.height * IdealWidthToHeight / screenRatio;
                cam.rect = currentRect;
            }

            float currentRatio = cam.aspect;

            // This would be a good place to calculate the currentRatio with new properties if we ever became a vertical rather than horizontal aspect ratio play area.
            GetComponent<Camera>().orthographicSize = IdealHorizontalSize * (float)cam.pixelHeight / (float)cam.pixelWidth * ZoomFactor;
            // GetComponent<Camera>().orthographicSize = IdealHorizontalSize * (float)Screen.height / (float)Screen.width * ZoomFactor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCameraSizing();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class UISurfaceDeriveFromCamera : MonoBehaviour
{
    public enum SurfaceLayout
    {
        MatchCamera,
        FillBottom,
        FillTop,
    }

    public SurfaceLayout LayoutType = SurfaceLayout.MatchCamera;

    public Camera watchedCamera = null;

    private void Update()
    {
        if (watchedCamera != null)
        {
            RectTransform myRect = GetComponent<RectTransform>();
            switch (LayoutType)
            {
                case SurfaceLayout.MatchCamera:
                    myRect.anchorMin = watchedCamera.rect.min;
                    myRect.anchorMax = watchedCamera.rect.max;
                    break;

                case SurfaceLayout.FillBottom:
                    if (watchedCamera.rect.min.y > 0.0f)
                    {
                        myRect.anchorMin = Vector2.zero;
                        myRect.anchorMax = new Vector2(1.0f, watchedCamera.rect.min.y);
                    }
                    break;
                case SurfaceLayout.FillTop:
                    if (watchedCamera.rect.max.y < 1.0f)
                    {
                        myRect.anchorMin = new Vector2(0.0f, watchedCamera.rect.max.y);
                        myRect.anchorMax = Vector2.one;
                    }
                    break;
            }
        }
    }
}

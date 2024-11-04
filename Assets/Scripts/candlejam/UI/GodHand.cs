using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodHand : MonoBehaviour
{
    public bool TouchscreenInteractions = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Clickable object that was clicked this frame
    private UIClickable ClickedObject = null;

    public T GetClickedObject<T>()
    {
        if (ClickedObject == null)
            return default(T);

        T retval = ClickedObject.GetComponent<T>();

        return retval;
    }

    // Update is called once per frame
    // God hand responsibilities
    // 1. Every frame check where the mouse is and pick what our mouse is currently hovering
    // 2. If we're in desktop mode, set the hoverable component to indicate it's hoverable. 
    // 3. If we're in mobile mode, set the hoverable component if the mouse is down. 
    // 4. If we're in desktop mode, when the mouse is pressed down, notify listeners of a Selection event on a clickable component
    // 5. If we're in mobile mode, when the mouse is lifted up, notify listeners of a Click event on a Clickable component

    void Update()
    {

        // Clear all "hovered" flags
        UIHoverable[] hoverables = FindObjectsOfType<UIHoverable>();
        foreach (UIHoverable hoverComponent in hoverables)
            hoverComponent.Hovered = false;

        // Clear the clicked object
        ClickedObject = null;
        
        // Do a raycast for this frame and set all the hovered components
        RaycastHit hoverableCheck;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hoverableCheck);
        if (hoverableCheck.collider != null)
        {
            Transform transform= hoverableCheck.collider.transform;

            while (transform != null)
            {
                // We only set hover if touchscreen interaction is off OR the mouse is down
                if (TouchscreenInteractions == false || Input.GetMouseButton(0))
                {
                    UIHoverable hoverable = transform.GetComponent<UIHoverable>();
                    if (hoverable)
                        hoverable.Hovered = true;
                }

                // Activate clicked Clickable under the right touchscreen interactin/mouse interaction combination
                if ((!TouchscreenInteractions && Input.GetMouseButtonDown(0)) ||
                    (TouchscreenInteractions && Input.GetMouseButtonUp(0)))
                {
                    if (transform.GetComponent<UIClickable>() != null)
                        ClickedObject = transform.GetComponent<UIClickable>();
                }

                // Make my way up the transform tree so we can let all our parents know that they are hovered too
                transform = transform.parent;
            }
            
        }
    }
}

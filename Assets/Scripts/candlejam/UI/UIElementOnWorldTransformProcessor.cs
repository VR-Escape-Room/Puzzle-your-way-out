using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElementOnWorldTransformProcessor : MonoBehaviour
{

    private void PositionUIObjectOnTransform(GameObject uiObject, Transform worldTransform, string hardpointName)
    {
        Transform hardpoint = worldTransform;
        if (hardpointName != null && hardpointName != "")
            hardpoint = worldTransform.Find(hardpointName);

        //first you need the RectTransform component of your canvas
        RectTransform CanvasRect = UICanvas.GetComponent<RectTransform>();

        //then you calculate the position of the UI element
        //0,0 for the canvas is at the center of the screen, whereas WorldToViewPortPoint treats the lower left corner as 0,0. Because of this, you need to subtract the height / width of the canvas * 0.5 to get the correct position.

        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(hardpoint.position);
        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));

        //now you can set the position of the ui element
        uiObject.GetComponent<RectTransform>().anchoredPosition = WorldObject_ScreenPosition;
       // uiObject.GetComponent<RectTransform>().position = WorldObject_ScreenPosition;
    }

    public Canvas UICanvas = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UIElementOnWorldTransform[] listOfPositionables = FindObjectsOfType<UIElementOnWorldTransform>();
        foreach (UIElementOnWorldTransform element in listOfPositionables)
        {
            if (element.Target != null)
                PositionUIObjectOnTransform(element.gameObject, element.Target, element.Hardpoint);
        }
    }
}

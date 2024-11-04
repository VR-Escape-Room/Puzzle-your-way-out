using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Description
// This class is used with UIElementOnWorldTransformProcessor to position UI elements over game objects/transforms in the world
//
// Usage
// UIElementOnWorldTransform is attached as a component on UI elements that you want to position over a game object
// Set the transform property and an optional Hardpoint which is a child transform
//
// Important: Make sure there is a UIElementOnWorldTransformProcessor somewhere in your scene (usually the Processor object instance). This is where the work of positioning
//  these objects is acutally done


public class UIElementOnWorldTransform : MonoBehaviour
{
    public Transform Target = null;
    public string Hardpoint = null;

    // Start is called before the first frame update
    void Start()
    {
        // Register ourselves with the processor (?)

    }
}

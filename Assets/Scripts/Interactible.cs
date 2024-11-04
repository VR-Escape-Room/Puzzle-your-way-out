using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactible : MonoBehaviour
{
    public UnityEvent InteractionHandlers;
    public float InteractDistance = 1.2f;
    public void PerformInteraction()
    {
        InteractionHandlers.Invoke();
    }
}

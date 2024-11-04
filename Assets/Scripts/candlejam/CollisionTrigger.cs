using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTrigger : MonoBehaviour
{

    public delegate void VolumeTriggerEvent(Collider other);

    public event VolumeTriggerEvent TriggerEnter;
    public string triggerTag = null;

    private void OnTriggerEnter(Collider other)
    {
        if (triggerTag == null || triggerTag=="" || other.CompareTag(triggerTag))
        {
            TriggerEnter?.Invoke(other);
        }

    }
}

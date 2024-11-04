using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shakeable : MonoBehaviour
{
    public bool Active = false;

    public float StartTime = 0.0f;
    public float EndTime = 0.0f;
    public Vector3 CurrentShakeOffset = Vector3.zero;
    public Vector3 ShakeAxis = Vector3.one;
    public float Amplitude = 1.0f;
    public float CycleCount = 3.0f;
    public float Attenuation = 1.0f;
}

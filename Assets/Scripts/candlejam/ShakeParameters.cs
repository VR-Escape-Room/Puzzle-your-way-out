using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="candlejam/ShakeParameters")]
public class ShakeParameters : ScriptableObject
{
    [Tooltip("Direction of Shake")]
    public Vector3 ShakeAxis = Vector3.up;

    [Tooltip("Time in seconds of the shake")]
    [Range(0.0f, 3.0f)]
    public float Duration = 0.25f;

    [Range(0.0f, 10.0f)]
    [Tooltip("Amplitude of shake")]
    public float Amplitude = 1.0f;

    [Range(0.1f, 20.0f)]
    [Tooltip("How many full shake cycles to perform over the duration")]
    public float CycleCount = 2.0f;

    [Tooltip("Exponent(x) by which to attenuate shake over the duration. (1-t)^x")]
    [Range(1.0f, 5.0f)]
    public float Attenuation = 2.0f;

}

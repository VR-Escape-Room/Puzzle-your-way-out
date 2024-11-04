using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Shakeable_ExtensionMethods
{
    public static void Shake(this GameObject go, float amplitude, float attenuation, float duration, Vector3 shakeAxis, float CycleCount)
    {
        Shakeable shakeable = go.GetComponent<Shakeable>();
        if (shakeable == null)
        {
            shakeable = go.AddComponent<Shakeable>();
        }

        shakeable.StartTime = Time.timeSinceLevelLoad;
        shakeable.EndTime = Time.timeSinceLevelLoad + duration;
        shakeable.CurrentShakeOffset = Vector3.zero;
        shakeable.ShakeAxis = shakeAxis;
        shakeable.Amplitude = amplitude;
        shakeable.CycleCount = CycleCount;
        shakeable.Attenuation = attenuation;

        shakeable.Active = true;
    }

    public static void Shake(this GameObject go, ShakeParameters shake)
    {
        Shake(go, shake.Amplitude, shake.Attenuation, shake.Duration, shake.ShakeAxis, shake.CycleCount);
    }
}


public class ShakeableUpdater : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Shakeable[] objs = FindObjectsOfType<Shakeable>();

        foreach(Shakeable s in objs)
        {
            if (s.Active)
            {
                s.gameObject.transform.localPosition -= s.CurrentShakeOffset;

                if (Time.timeSinceLevelLoad < s.EndTime)
                {
                    float t = s.CycleCount * (Time.timeSinceLevelLoad - s.StartTime) / (s.EndTime - s.StartTime);
                    float angle = t * Mathf.PI * 2.0f;
                    float amplitude = Mathf.Cos(angle);

                    float percentIntoShake = (Time.timeSinceLevelLoad - s.StartTime) / (s.EndTime - s.StartTime);
                    float attenuation = s.Amplitude * Mathf.Pow((1.0f - percentIntoShake), s.Attenuation);

                    s.CurrentShakeOffset = s.ShakeAxis * amplitude * attenuation; 
                    s.gameObject.transform.localPosition += s.CurrentShakeOffset;
                }
                else
                {
                    s.Active = false;
                    s.CurrentShakeOffset = Vector3.zero;
                }
            }
        }
    }
}

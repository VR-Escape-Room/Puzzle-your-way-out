using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Usage:
 * Place one AnimatableUpdater on an object somewhere in your scene. 
 * This will process all Animatable objects in the scene every frame.
 * 
 * You can animate an object with the extension method
 *  gameObject.AnimateAlongPath(curve, duration)
 *  gameObject.AnimateAlongStraightLine(vecDest, duration)
 * */


public static class AnimatableUpdater_ExtensionMethods
{
    public static void AnimateAlongPath(this GameObject go, CubicBezierCurve curve, float duration, bool animationInLocalPosition = false)
    {
        Animatable animatable = go.GetComponent<Animatable>();
        if (animatable == null)
        {
            animatable = go.AddComponent<Animatable>();
        }

        animatable.AnimateAlongPath(curve, duration, animationInLocalPosition);
    }

    public static void AnimateAlongStraightLine(this GameObject go, Vector3 dest, float duration, bool animationInLocalPosition = false)
    {
        Vector3 srcPos = animationInLocalPosition ? go.transform.localPosition : go.transform.position;
        AnimateAlongPath(go,
            new CubicBezierCurve(
                srcPos,
                srcPos * 0.6666f + dest * 0.3333f,
                srcPos * 0.3333f + dest * 0.6666f,
                dest
                ),
            duration,
            animationInLocalPosition);
    }

    public static void AnimateAlongStraightLine(this GameObject go, Vector3 dest, float duration, bool animationInLocalPosition, Vector3 endScale)
    {

        Animatable animatable = go.GetComponent<Animatable>();
        if (animatable == null)
        {
            animatable = go.AddComponent<Animatable>();
        }

        animatable.AnimateAlongStraightLine(dest, duration, animationInLocalPosition, endScale);
    }

}

public class AnimatableUpdater : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Animatable[] targets = FindObjectsOfType<Animatable>();
        foreach(Animatable a in targets)
        {
            if (a.Animating)
            {
                float calctime = Time.timeSinceLevelLoad;
                if (Time.timeSinceLevelLoad > a.EndTime)
                {
                    calctime = a.EndTime;
                    a.Animating = false;
                }
                float t = (calctime - a.StartTime) / (a.EndTime - a.StartTime);

                if (a.AnimationInLocalPosition)
                    a.transform.localPosition = a.bezierCurve.CalculateCubicBezierPoint(t);
                else
                    a.transform.position = a.bezierCurve.CalculateCubicBezierPoint(t);

                if (a.AnimateScale)
                {
                    a.transform.localScale = new Vector3(
                        a.startScale.x + (a.endScale.x - a.startScale.x) * t,
                        a.startScale.y + (a.endScale.y - a.startScale.y) * t,
                        a.startScale.z + (a.endScale.z - a.startScale.z) * t);
                }
            }
        }
    }
}

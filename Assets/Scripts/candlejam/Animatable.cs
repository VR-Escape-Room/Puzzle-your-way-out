using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animatable : MonoBehaviour
{
    public CubicBezierCurve bezierCurve;
    public bool AnimationInLocalPosition = false;
    public bool AnimateScale = false;
    public Vector3 startScale = Vector3.one;
    public Vector3 endScale = Vector3.one;

    public float StartTime = 0.0f;
    public float EndTime = 0.0f;
    public bool Animating = false;


    public void AnimateAlongPath(CubicBezierCurve curve, float duration, bool animationInLocalPosition = false)
    {
        bezierCurve = curve;
        AnimationInLocalPosition = animationInLocalPosition;
        StartTime = Time.timeSinceLevelLoad;
        EndTime = Time.timeSinceLevelLoad + duration;
        Animating = true;
    }

    public void AnimateAlongStraightLine(Vector3 dest, float duration, bool animationInLocalPosition, Vector3 endingScale)
    {
        Vector3 srcPos = animationInLocalPosition ? transform.localPosition : transform.position;
        AnimateAlongPath(
            new CubicBezierCurve(
                srcPos,
                srcPos * 0.6666f + dest * 0.3333f,
                srcPos * 0.3333f + dest * 0.6666f,
                dest
                ),
            duration,
            animationInLocalPosition);

        AnimateScale = true;
        startScale = transform.localScale;
        endScale = endingScale;
    }


}

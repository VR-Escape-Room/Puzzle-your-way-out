using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CubicBezierCurve {

    public CubicBezierCurve(Vector3 _p0, Vector3 _p1, Vector3 _p2, Vector3 _p3)
    {
        p0 = _p0;
        p1 = _p1;
        p2 = _p2;
        p3 = _p3;
    }

    public Vector3 p0;
    public Vector3 p1;
    public Vector3 p2;
    public Vector3 p3;

    public Vector3 CalculateCubicBezierPoint(float t)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * p0;
        p += 3 * uu * t * p1;
        p += 3 * u * tt * p2;
        p += ttt * p3;

        return p;
    }

    public Vector3 CalculateCubicBezierTangent(float t)
    {
        Vector3 p = -3 * (1 - t) * (1 - t) * p0;
        p += 3 * (1 - t) * (1 - t) * p1;
        p -= 6 * t * (1 - t) * p1;
        p -= 3 * t * t * p2;
        p += 6 * t * (1 - t) * p2;
        p += 3 * t * t * p3;

        return p;
    }

    //public Vector3 CalculateCubicBezierTangent(float t)
    //{
    //    Vector3 p0 = CalculateCubicBezierPoint(t);
    //    Vector3 p1 = CalculateCubicBezierPoint(t + 0.001f);
    //    Vector3 tan = (p1 - p0).normalized;

    //    return tan;
    //}

    public void FlipHorizontally()
    {
        p0.x = -p0.x;
        p1.x = -p1.x;
        p2.x = -p2.x;
        p3.x = -p3.x;
    }

    public void FlipVertically()
    {
        p0.y = -p0.y;
        p1.y = -p1.y;
        p2.y = -p2.y;
        p3.y = -p3.y;
    }
}

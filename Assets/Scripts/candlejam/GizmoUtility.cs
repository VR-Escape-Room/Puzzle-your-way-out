using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class GizmoUtility 
{
    public static void DrawCircle(Vector3 center, float radius, Quaternion rotation)
    {
        int segments = 12;
        float anglePerSegment = 360.0f / (float)segments;

        for (int i = 0; i < segments; i++)
        {
            float startAngle = anglePerSegment * i;
            float endAngle = anglePerSegment * (i + 1);

            Vector3 pointA = center + Quaternion.Euler(0.0f, startAngle, 0f) * new Vector3(radius, 0f, 0f);
            Vector3 pointB = center + Quaternion.Euler(0.0f, endAngle, 0f) * new Vector3(radius, 0f, 0f);

            Gizmos.DrawLine(pointA, pointB);
        }
    }

    public static void DrawBezier(CubicBezierCurve curve)
    {
        int segments = 20;
        float t_s = 1.0f / (float)segments;

        for (int i = 0; i < segments; i++)
        {
            float t0 = i * t_s;
            float t1 = (i + 1) * t_s;

            Gizmos.DrawLine(curve.CalculateCubicBezierPoint(t0), curve.CalculateCubicBezierPoint(t1));
        }

    }

    public static void DrawThickLine(Vector3 p1, Vector3 p2, float thickness)
    {
#if UNITY_EDITOR
        Handles.DrawBezier(p1, p2, p1, p2, Gizmos.color, null, thickness);
#endif
    }
}

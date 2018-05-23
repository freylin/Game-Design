using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Omega
{
    public class Debug
    {
        public static float deltaAngle = 1f;

        public static void DrawArc(Vector3 center, Vector3 normal, Vector3 startPoint, float angle, Color color)
        {
            Vector3 p0 = startPoint;
            for (float a = deltaAngle; a <= angle; a += deltaAngle)
            {
                Vector3 p1 = Quaternion.AngleAxis(a, normal) * (startPoint - center) + center;
                UnityEngine.Debug.DrawLine(p0, p1, color);
                p0 = p1;
            }
        }

        public static void DrawArc(Vector3 center, Vector3 normal, Vector3 startPoint, float angle, Color color, float duration)
        {
            Vector3 p0 = startPoint;
            for (float a = deltaAngle; a <= angle; a += deltaAngle)
            {
                Vector3 p1 = Quaternion.AngleAxis(a, normal) * (startPoint - center) + center;
                UnityEngine.Debug.DrawLine(p0, p1, color, duration);
                p0 = p1;
            }
        }

        public static void DrawCircle(Vector3 center, Vector3 normal, Vector3 pointOnCircle, Color color)
        {
            DrawArc(center, normal, pointOnCircle, 360f, color);
        }

        public static void DrawCircle(Vector3 center, Vector3 normal, Vector3 pointOnCircle, Color color, float duration)
        {
            DrawArc(center, normal, pointOnCircle, 360f, color, duration);
        }
    }
}
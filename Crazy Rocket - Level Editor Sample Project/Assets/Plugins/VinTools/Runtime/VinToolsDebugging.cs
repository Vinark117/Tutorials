using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VinTools.Debugging
{
    public static class DebugGizmos
    {
        public static void VisualizeAnimationCurve(AnimationCurve curve) => VisualizeAnimationCurve(curve, Vector2.zero, Vector2.one, Color.white, 100);
        public static void VisualizeAnimationCurve(AnimationCurve curve, int resolution) => VisualizeAnimationCurve(curve, Vector2.zero, Vector2.one, Color.white, resolution);
        public static void VisualizeAnimationCurve(AnimationCurve curve, Color color) => VisualizeAnimationCurve(curve, Vector2.zero, Vector2.one, color, 100);
        public static void VisualizeAnimationCurve(AnimationCurve curve, Color color, int resolution) => VisualizeAnimationCurve(curve, Vector3.zero, Vector2.one, color, resolution);
        public static void VisualizeAnimationCurve(AnimationCurve curve, Vector2 position, Vector2 size) => VisualizeAnimationCurve(curve, position, size, Color.white, 100);
        public static void VisualizeAnimationCurve(AnimationCurve curve, Vector2 position, Vector2 size, int resolution) => VisualizeAnimationCurve(curve, position, size, Color.white, resolution);
        public static void VisualizeAnimationCurve(AnimationCurve curve, Vector2 position, Vector2 size, Color color) => VisualizeAnimationCurve(curve, position, size, color, 100);
        public static void VisualizeAnimationCurve(AnimationCurve curve, Vector2 position, Vector2 size, Color color, int resolution)
        {
            for (int i = 1; i < 10; i++)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(position + new Vector2(0, size.y / 10 * i), position + new Vector2(size.x, size.y / 10 * i));
                Gizmos.color = Color.green;
                Gizmos.DrawLine(position + new Vector2(size.x / 10 * i, 0), position + new Vector2(size.x / 10 * i, size.y));
            }

            Gizmos.color = Color.black;
            Gizmos.DrawWireCube(position + size / 2, size);

            Gizmos.color = color;
            for (int i = 0; i < resolution; i++)
            {
                float time = 1f / (float)resolution * (float)i;
                float value = curve.Evaluate(time);

                float time2 = 1f / (float)resolution * (float)(i + 1f);
                float value2 = curve.Evaluate(time2);

                Gizmos.DrawLine(position + new Vector2(time, value) * size, position + new Vector2(time2, value2) * size);
            }
        }


    }

    /*public static class DebugRuntime
    {

    }*/
}

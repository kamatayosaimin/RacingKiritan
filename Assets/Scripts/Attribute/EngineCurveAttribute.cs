using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class EngineCurveAttribute : PropertyAttribute
{
    public readonly Rect ranges;

    public EngineCurveAttribute(float x, float y, float width, float height)
    {
        ranges = new Rect(x, y, width, height);
    }
}

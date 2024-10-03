using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathUtil
{

    public static float Damp(float current, float target, float damper)
    {
        float t = damper * Time.deltaTime;

        return Mathf.Lerp(current, target, t);
    }
}

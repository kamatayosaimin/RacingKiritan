using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class VectorUtil
{

    public static Vector2 Average2(params Vector2[] values)
    {
        Vector2 result = new Vector2();

        if (values.Length == 0)
            return result;

        if (values.Length == 1)
            return values[0];

        result.x = values.Average(v => v.x);
        result.y = values.Average(v => v.y);

        return result;
    }

    public static Vector3 Average3(params Vector3[] values)
    {
        Vector3 result = new Vector3();

        if (values.Length == 0)
            return result;

        if (values.Length == 1)
            return values[0];

        result.x = values.Average(v => v.x);
        result.y = values.Average(v => v.y);
        result.z = values.Average(v => v.z);

        return result;
    }

    public static Vector4 Average4(params Vector4[] values)
    {
        Vector4 result = new Vector4();

        if (values.Length == 0)
            return result;

        if (values.Length == 1)
            return values[0];

        result.x = values.Average(v => v.x);
        result.y = values.Average(v => v.y);
        result.z = values.Average(v => v.z);
        result.w = values.Average(v => v.w);

        return result;
    }
}

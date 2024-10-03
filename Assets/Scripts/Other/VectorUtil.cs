using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class VectorUtil
{

    public static Vector2 Average2(params Vector2[] values)
    {
        return Average2Child(values);
    }

    public static Vector2 Average2(List<Vector2> list)
    {
        return Average2Child(list);
    }

    public static Vector2 Average2(IEnumerable<Vector2> values)
    {
        return Average2Child(values);
    }

    static Vector2 Average2Child(IEnumerable<Vector2> values)
    {
        if (values.Count() <= 1)
            return values.FirstOrDefault();

        Vector2 result = new Vector2();

        result.x = values.Average(v => v.x);
        result.y = values.Average(v => v.y);

        return result;
    }

    public static Vector3 Average3(params Vector3[] values)
    {
        return Average3Child(values);
    }

    public static Vector3 Average3(List<Vector3> list)
    {
        return Average3Child(list);
    }

    public static Vector3 Average3(IEnumerable<Vector3> values)
    {
        return Average3Child(values);
    }

    static Vector3 Average3Child(IEnumerable<Vector3> values)
    {
        if (values.Count() <= 1)
            return values.FirstOrDefault();

        Vector3 result = new Vector3();

        result.x = values.Average(v => v.x);
        result.y = values.Average(v => v.y);
        result.z = values.Average(v => v.z);

        return result;
    }

    public static Vector4 Average4(params Vector4[] values)
    {
        return Average4Child(values);
    }

    public static Vector4 Average4(List<Vector4> list)
    {
        return Average4Child(list);
    }

    public static Vector4 Average4(IEnumerable<Vector4> values)
    {
        return Average4Child(values);
    }

    static Vector4 Average4Child(IEnumerable<Vector4> values)
    {
        if (values.Count() <= 1)
            return values.FirstOrDefault();

        Vector4 result = new Vector4();

        result.x = values.Average(v => v.x);
        result.y = values.Average(v => v.y);
        result.z = values.Average(v => v.z);
        result.w = values.Average(v => v.w);

        return result;
    }
}

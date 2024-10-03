using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class EnumUtil
{

    public static T[] GetValues<T>() where T : Enum
    {
        return Enum.GetValues(typeof(T)).Cast<T>().ToArray();
    }
}

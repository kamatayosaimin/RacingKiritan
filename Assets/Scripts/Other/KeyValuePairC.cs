using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KeyValuePairC<TKey, TValue>
{
    [SerializeField] TKey _key;
    [SerializeField] TValue _value;

    public TKey Key
    {
        get
        {
            return _key;
        }
        set
        {
            _key = value;
        }
    }

    public TValue Value
    {
        get
        {
            return _value;
        }
        set
        {
            _value = value;
        }
    }

    public KeyValuePairC()
        : this(default(TKey), default(TValue))
    {
    }

    public KeyValuePairC(TKey key, TValue value)
    {
        _key = key;
        _value = value;
    }

    public static TResult ToDictionary<TResult>(IEnumerable<KeyValuePairC<TKey, TValue>> pairs) where TResult : Dictionary<TKey, TValue>, new()
    {
        TResult result = new TResult();

        foreach (var p in pairs)
            result.Add(p.Key, p.Value);

        return result;
    }
}

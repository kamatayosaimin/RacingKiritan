using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryC<TKey, TValue> : Dictionary<TKey, TValue>
{

    public DictionaryC()
        : base()
    {
    }

    public DictionaryC<TKey, TResult> SelectDictionary<TResult>(Func<TValue, TResult> selector)
    {
        DictionaryC<TKey, TResult> newDictionary = new DictionaryC<TKey, TResult>();

        foreach (var kvp in this)
        {
            TResult result = selector(kvp.Value);

            newDictionary.Add(kvp.Key, result);
        }

        return newDictionary;
    }

    public TResult DictionaryCast<TResult>() where TResult : DictionaryC<TKey, TValue>, new()
    {
        TResult newDictionary = new TResult();

        foreach (var kvp in this)
            newDictionary.Add(kvp.Key, kvp.Value);

        return newDictionary;
    }
}

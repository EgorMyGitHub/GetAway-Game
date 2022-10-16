using System;
using System.Collections;
using System.Collections.Generic;
using Mono.CompilerServices.SymbolWriter;
using UnityEngine;

public class ComponentRoot : MonoBehaviour
{
    private static Dictionary<Type, object> _binds = new();

    public static void Bind<T>(T bindObject)
    {
        var type = typeof(T);

        if (!_binds.ContainsKey(type))
        {
            _binds.Add(type, null);
        }

        _binds[type] = bindObject;
    }

    public static T Resolve<T>()
    {
        var type = typeof(T);

        if (!_binds.ContainsKey(type))
            throw new KeyNotFoundException($"{type} Not Found");

        return (T)_binds[type];
    }
}

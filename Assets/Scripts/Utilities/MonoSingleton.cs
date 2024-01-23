using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T inst = null;

    public static T instance
    {
        get{
            return inst;
        }
    }

    protected virtual void Awake()
    {
        inst = (T)this;
    }
}

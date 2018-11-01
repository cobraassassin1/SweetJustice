using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Tools {

    internal static float GenerateRand()
    {
        return UnityEngine.Random.value * 100f;
    }

    internal static bool Contains<T>(T[] array, T instance)
    {
        bool var1 = false;
        foreach (T var in array)
        {
            if(instance.Equals(var))
            {
                var1 = true;
            }
        }
        return var1;
    }
}

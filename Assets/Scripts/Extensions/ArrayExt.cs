using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ArrayExt {

    public static void ForEach<T>(this T[] array, Action<T> action) {
        for(int i = 0; i < array.Length; i++) {
            action.Invoke(array[i]);
        }
    }

    /// <summary>
    /// Checks if the provided index is within the bounds of the array
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <param name="x">First Index</param>
    /// <param name="y">Second Index</param>
    /// <returns></returns>
    public static bool ContainsIndex<T>(this T[,] array, int x, int y) {
        return x >= 0 && x < array.GetLength(0) && y >= 0 && y <= array.GetLength(1);
    }
    /// <summary>
    /// Checks if the provided index is within the bounds of the array
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public static bool ContainsIndex<T>(this T[,] array, Vector2Int index) {
        return array.ContainsIndex(index.x, index.y);
    }
    /// <summary>
    /// Checks if the provided index is within the bounds of the array
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <param name="i"></param>
    /// <returns></returns>
    public static bool ContainsIndex<T>(this T[] array, int index) {
        return index >= 0 && index < array.GetLength(0);
    }

    /// <summary>
    /// Returns a random entry from the array
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <returns></returns>
    public static T Random<T>(this T[] array) {
        if (array.Length == 0) {
            return default(T);
        } else if (array.Length == 1) {
            return array[0];
        }
        return array[UnityEngine.Random.Range(0, array.Length)];

    }
}
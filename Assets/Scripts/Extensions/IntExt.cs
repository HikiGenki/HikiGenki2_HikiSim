using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IntExt {

    public static int ClampIncrement(this int num, int max) {
        num++;
        return Mathf.Min(num, max);
    }

    public static int ClampDecrement(this int num, int min) {
        num--;
        return Mathf.Max(num, min);
    }

    public static int LoopIncrement(this int num, int min, int max) {
        num++;
        num = num > max ? min : num;
        return num;
    }

    public static bool WithinRange(this int num, int min, int max) {
        return num >= min && num <= max;
    }

    public static int LoopIncrement(this int num, int max) {
        return num.LoopIncrement(0, max);
    }

    public static int LoopDecrement(this int num, int min, int max) {
        num--;
        num = num < min ? max : num;
        return num;
    }

    public static int LoopDecrement(this int num, int max) {
        return num.LoopDecrement(0, max);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DictionaryExt {

    public delegate void DictionaryOperation<T, K>(T key);

    public static void ForEach<T,K>(this Dictionary<T,K> dict, DictionaryOperation<T, K> operation) {
        List<T> keys = new List<T>();
        keys.AddRange(dict.Keys);
        foreach(T key in keys) {
            operation(key);
        }
    }

    public static K GetValueByKeyIndex<T, K>(this Dictionary<T,K> dict, int index) {
        List<T> keys = new List<T>(dict.Keys);
        return dict[keys[index]];
    }

}

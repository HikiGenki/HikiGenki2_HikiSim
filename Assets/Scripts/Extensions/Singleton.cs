using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>{

    private static T instance;
    public static T Instance {
        get => instance;
    }
    public static bool HasInstance {
        get => instance != null;
    }
    
    protected virtual void Awake(){
        if(instance == null) {
            instance = (T)this;
        } else {
            Debug.LogError("Tried to create a new instance of singleton " + typeof(T).Name + " while one already exists");
        }
    }

    protected virtual void OnDestroy() {
        if(instance == this) {
            instance = null;
        }
    }
}

using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PathEvent : MonoBehaviour {

    public UnityEvent OnInteract;


    [Button, EnableIf("@UnityEngine.Application.isPlaying && this.gameObject.activeSelf")]
    public void Interact() {
        OnInteract.Invoke();
        
    }

}
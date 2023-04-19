using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PathEvent : MonoBehaviour {

    public Material outlineMaterial;
    Material defaultMaterial;

    public UnityEvent OnInteract;

    public UnityEvent MouseEnter;
    public UnityEvent MouseExit;

    SpriteRenderer sr;
    

    void Awake() {
        sr = GetComponent<SpriteRenderer>();
    }
    void Start() { }


    [Button, EnableIf("@UnityEngine.Application.isPlaying && this.gameObject.activeSelf")]
    public void Interact() {
        if (this.enabled) {
            OnInteract?.Invoke();
        }
        
    }


    private void OnMouseEnter() {
        //Debug.Log("Mouse On " + name);
        defaultMaterial = sr.material;
        sr.material = outlineMaterial;
        MouseEnter?.Invoke();
    }

    private void OnMouseExit() {
        //Debug.Log("Mouse left " + name);
        sr.material = defaultMaterial;
        MouseExit?.Invoke();
    }

    private void OnMouseDown() {
        Interact();
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ObjectActivationManager : MonoBehaviour {

    [SerializeField, ReadOnly] List<ObjectActivationController> objects;

    void OnEnable() {
        //Search the hierarchy and create a list of all ObjectActivationControllers
        objects = new List<ObjectActivationController>();
        objects.AddRange(GameObject.FindObjectsOfType<ObjectActivationController>());
    }


    void LateUpdate() {
        foreach(ObjectActivationController controller in objects) {
            controller.gameObject.SetActive(controller.ConditionsMet());
        }
    }





}
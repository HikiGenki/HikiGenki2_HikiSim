using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;

public class ObjectActivationManager : MonoBehaviour {

    [SerializeField, ReadOnly] List<ObjectActivationController> objects;

    void OnEnable() {
        //Search the hierarchy and create a list of all ObjectActivationControllers
        objects = new List<ObjectActivationController>();
        objects.AddRange(GameObject.FindObjectsOfType<ObjectActivationController>());
    }


    void LateUpdate() {
        foreach(ObjectActivationController controller in objects) {
            bool conditionsMet = controller.ConditionsMet();
            if (controller.deactivateObjectOnConditionFail) {
                controller.gameObject.SetActive(conditionsMet);
            }
            if (controller.disablePathEventsOnConditionFail) {
                List<PathEvent> pathEvents = controller.gameObject.GetComponents<PathEvent>().ToList();
                foreach(PathEvent pathEvent in pathEvents) {
                    pathEvent.enabled = conditionsMet;
                }
            }
        }
    }





}
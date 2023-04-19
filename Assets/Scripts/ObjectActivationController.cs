using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using static ReflectionAssistant;

public class ObjectActivationController : MonoBehaviour {


    public delegate bool Condition();
    
    public List<ConditionPath> conditions;

    public bool deactivateObjectOnConditionFail = true;
    public bool disablePathEventsOnConditionFail;


    [Button]
    public void TestConditions() {
        for(int i = 0; i < conditions.Count; i++) {
            Condition condition = conditions[i].GetCondition();
            if(condition != null) {
                Debug.Log("Condition at index " + i + " is " + condition());
            } else {
                Debug.Log("Condition at index " + i + " is invalid");
            }
        }
    }


    public bool ConditionsMet() {
        for (int i = 0; i < conditions.Count; i++) {
            Condition condition = conditions[i].GetCondition();
            if (condition != null) {
                if (!condition()) {
                    return false;
                }
            } else {
                Debug.LogError("Condition at index " + i + " is invalid");
            }
        }
        return true;
    }





    [System.Serializable]
    public class ConditionPath {

        public GameObject target;

        [ValueDropdown("MethodOptions")]
        public string functionPath;



        ValueDropdownList<string> MethodOptions() {

            ValueDropdownList<string> namedOptions = new ValueDropdownList<string>();

            if (target) {
                var behaviours = target.GetComponents<UnityEngine.Component>();

                foreach (var behaviour in behaviours) {
                    MethodInfo[] methods = ReflectionAssistant.GetMethods(behaviour.GetType());
                    foreach (MethodInfo method in methods) {
                        if (method.ReturnType == typeof(bool) && !method.HasParameters() && method.IsPublic) {
                            string path = behaviour.GetType().AssemblyQualifiedName + "/" + method.Name;
                            string pathName = behaviour.GetType().Name + "/" + method.Name;

                            namedOptions.Add(pathName, path);

                        }
                    }
                }
            }

            return namedOptions;
        }

        public Condition GetCondition() {
            if (target && !string.IsNullOrEmpty(functionPath)) {
                var typeString = functionPath.Substring(0, functionPath.IndexOf("/"));
                var nameString = functionPath.Substring(functionPath.IndexOf("/") + 1);

                var type = ReflectionAssistant.GetType(typeString);
                if (type != null) {
                    var method = ReflectionAssistant.GetMethod(type, nameString);


                    if (method != null) {
                        return () => {
                            return (bool)method.Invoke(target.GetComponent(type), new object[] { });
                        };
                    } else {
                        Debug.LogError("No method exists with path " + functionPath + " inside object " + target.name);
                    }
                } else {
                    Debug.LogError("Type " + typeString + " is invalid");
                }
            }

            return null;
        }
    }






}
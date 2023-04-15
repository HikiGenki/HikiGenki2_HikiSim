using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HikiLife.Data {
    [CreateAssetMenu(menuName = "Hiki/Stat")]
    public class Stat : ScriptableObject {

        public new string name;
        [TextArea]
        public string description;
        public bool isStartingStat;
        public int defaultValue = 5;
        public bool hasMaxValue = true;
        public int maxValue = 100;
        public bool isConsumable;

        



#if UNITY_EDITOR
        void OnValidate() {
            if (string.IsNullOrEmpty(name)) {
                name = base.name;
            }
            if (string.IsNullOrEmpty(description)) {
                description = "Stat that represents " + name;
            }
        }
#endif
    }
}
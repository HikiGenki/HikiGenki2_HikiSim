using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HikiLife.Data {
    [CreateAssetMenu(menuName = "Hiki/Item")]
    public class Item : ScriptableObject {

        public new string name;
        [TextArea]
        public string description;
        public bool isConsumable;
        [PreviewField]
        public Sprite sprite;




#if UNITY_EDITOR
        void OnValidate() {
            if (string.IsNullOrEmpty(name)) {
                name = base.name;
            }
            if (string.IsNullOrEmpty(description)) {
                description = "It's a " + name;
            }
        }
#endif
    }
}
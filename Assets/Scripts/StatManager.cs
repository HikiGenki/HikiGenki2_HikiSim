using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HikiLife.Data;

namespace HikiLife.Managers {
    public class StatManager : Singleton<StatManager> {

        [SerializeField]List<Stat> statList;
        Dictionary<Stat, int> statValues;
        Dictionary<Stat, bool> statUnlockedStates;

        new void Awake() {
            base.Awake();
            statValues = new Dictionary<Stat, int>();
            statUnlockedStates = new Dictionary<Stat, bool>();
            foreach(Stat stat in statList) {
                statValues.Add(stat, stat.defaultValue);
                statUnlockedStates.Add(stat, stat.isStartingStat);
            }
        }

        public static void ChangeStatValue(Stat stat, int changeAmount) {
            SetStatValue(stat, Instance.statValues[stat] + changeAmount);
        }
        public static void SetStatValue(Stat stat, int value) {
            Instance.statValues[stat] = Mathf.Clamp(value, 0, stat.hasMaxValue ? stat.maxValue : int.MaxValue);
        }

        public static int GetStatValue(Stat stat) {
            return Instance.statValues[stat];
        }

        public static bool IsStatUnlocked(Stat stat) {
            return Instance.statUnlockedStates[stat];
        }
    }
}
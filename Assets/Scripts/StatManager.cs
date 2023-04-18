using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HikiLife.Data;
using Sirenix.OdinInspector;
namespace HikiLife.Managers {
    public class StatManager : SerializedMonoBehaviour {

        public static StatManager Instance;



        public Stat energy;


        [SerializeField]List<Stat> statList;
        [SerializeField, ShowIf("@UnityEngine.Application.isPlaying")]Dictionary<Stat, int> statValues;
        [SerializeField, ShowIf("@UnityEngine.Application.isPlaying")] Dictionary<Stat, bool> statUnlockedStates;

        void Awake() {
            Instance = this;
            statValues = new Dictionary<Stat, int>();
            statUnlockedStates = new Dictionary<Stat, bool>();
            foreach(Stat stat in statList) {
                statValues.Add(stat, stat.defaultValue);
                statUnlockedStates.Add(stat, stat.isStartingStat);
            }
            
        }

        public static void IncreaseStat(Stat stat, int amount) {
            SetStatValue(stat, Instance.statValues[stat] + amount);
        }
        public static void DecreaseStat(Stat stat, int amount) {
            SetStatValue(stat, Instance.statValues[stat] - amount);
        }

        public static void SetStatValue(Stat stat, int value) {
            //If changing a stat for the first time, consider it unlocked
            if (!IsStatUnlocked(stat)) {
                UnlockStat(stat);
            }
            
            
            Instance.statValues[stat] = Mathf.Clamp(value, 0, stat.hasMaxValue ? stat.maxValue : int.MaxValue);
        }

        public static int GetStatValue(Stat stat) {
            return Instance.statValues[stat];
        }
        /// <summary>
        /// Checks if stat is at least the given amount
        /// </summary>
        /// <param name="stat"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static bool HasStat(Stat stat, int amount) {
            return GetStatValue(stat) >= amount;
        }

        public static bool IsStatUnlocked(Stat stat) {
            return Instance.statUnlockedStates[stat];
        }
        public static void UnlockStat(Stat stat) {
            Instance.statUnlockedStates[stat] = true;
        }

        public static void MaxStat(Stat stat) {
            SetStatValue(stat, stat.hasMaxValue ? stat.maxValue : int.MaxValue);
        }

        //Energy Gets it's own functions just for utility
        public static bool HasEnergy(int amount) {
            return GetStatValue(Instance.energy) >= amount;
        }
        public static void SpendEnergy(int amount) {
            DecreaseStat(Instance.energy, amount);
        }
        public static void RestoreEnergy(int amount) {
            IncreaseStat(Instance.energy, amount);
        }
    }
}
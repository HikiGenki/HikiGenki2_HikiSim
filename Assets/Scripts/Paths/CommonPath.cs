using HikiLife.Data;
using HikiLife.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HikiLife.Paths {
    public class CommonPath : MonoBehaviour {

        [Header("Path Stats")]
        public Stat energy;
        public Stat confidence;


        public void RestEffect() {
            StatManager.MaxStat(energy);
            TimeManager.Instance.CurrentDay++;
        }

        public void WorkOutEffect() {
            StatManager.IncreaseStat(confidence, 3);
            StatManager.SpendEnergy(20);
        }

        public bool WorkOutCondition() {
            return StatManager.HasEnergy(20);
        }

        

    }
}
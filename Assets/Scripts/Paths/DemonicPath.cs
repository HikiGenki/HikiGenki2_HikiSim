using HikiLife.Data;
using HikiLife.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HikiLife.Paths {
    public class DemonicPath : MonoBehaviour {

        [Header("Path Stats")]
        public Stat confidence;
        public Stat demonic;
        public Stat health;
        public Stat worldsChaos;
        public Stat demonWorshippers;
        public Stat money;

        [Header("Path Items")]
        public Item demonsForDummies;

        [Header("Event Variables")]
        public bool isFirstSummoningComplete;
        public bool isContractOfChaosComplete;
        public bool isStartCultOfChaosComplete;
        public bool isContractWithDemonKingComplete;

        public bool DemonicPathCondition() {
            return Variables.Instance.alignment == Alignment.ChaoticEvil &&
                    StatManager.GetStatValue(confidence) >= 20 && 
                    Inventory.Instance.HasItem(demonsForDummies) &&
                    TimeManager.Instance.CurrentDay >= 10;
    
        }

        public void ReadDemonsForDummiesEffect() {
            StatManager.IncreaseStat(demonic, 1);
            StatManager.SpendEnergy(20);
        }
        public bool ReadDemonsForDummiesCondition() {
            return DemonicPathCondition() &&
                StatManager.HasEnergy(20) &&
                StatManager.GetStatValue(demonic) < 10;
        }

        public void BloodRitualEffect() {
            StatManager.IncreaseStat(demonic, 3);
            StatManager.DecreaseStat(health, 10);
            StatManager.SpendEnergy(30);
        }
        public bool BloodRitualCondition() {
            return StatManager.GetStatValue(demonic) >= 10 &&
                StatManager.GetStatValue(health) > 10 &&
                StatManager.HasEnergy(30);
        }

        public void FirstSummoningEffect() {
            StatManager.IncreaseStat(demonic, 10);
            StatManager.IncreaseStat(worldsChaos, 5);
            StatManager.DecreaseStat(health, 50);
            StatManager.SpendEnergy(80);
            isFirstSummoningComplete = true;
        }

        public bool FirstSummoningCondition() {
            return StatManager.HasStat(demonic, 25) &&
                StatManager.HasStat(health, 51) &&
                StatManager.HasEnergy(80) && 
                !isFirstSummoningComplete;
        }

        public void ConverseWithDemonEffect() {
            StatManager.IncreaseStat(demonic, 10);
            StatManager.SpendEnergy(15);
        }
        public bool ConverseWithDemonCondition() {
            return isFirstSummoningComplete &&
                StatManager.HasEnergy(15);
        }
        public void ContractOfChaosEffect() {
            StatManager.SpendEnergy(80);
            StatManager.IncreaseStat(demonic, 20);
            StatManager.IncreaseStat(worldsChaos, 5);
            isContractOfChaosComplete = true;
        }
        public bool ContractOfChaosCondition() {
            return StatManager.HasStat(demonic, 100) &&
                StatManager.HasEnergy(80) &&
                !isContractOfChaosComplete && 
                isFirstSummoningComplete;
        }

        public void SummonDemonEffect() {
            StatManager.SpendEnergy(50);
            StatManager.IncreaseStat(demonic, 25);
            StatManager.IncreaseStat(worldsChaos, 3);
        }
        public bool SummonDemonCondition() {
            return isContractOfChaosComplete && 
                StatManager.HasEnergy(50);
        }
        public void SummonGreaterDemonEffect() {
            StatManager.SpendEnergy(70);
            StatManager.IncreaseStat(demonic, 50);
            StatManager.IncreaseStat(worldsChaos, 5);
        }
        public bool SummonGreaterDemonCondition() {
            return isContractOfChaosComplete &&
                StatManager.HasStat(demonic, 300) &&
                StatManager.HasEnergy(70) &&
                StatManager.HasStat(worldsChaos, 50);
        }
        public void StartCultOfChaosEffect() {
            StatManager.SpendEnergy(75);
            StatManager.IncreaseStat(demonWorshippers, 10);
            StatManager.IncreaseStat(money, 500);

            isStartCultOfChaosComplete = true;
        }
        public bool StartCultOfChaosCondition() {
            return !isStartCultOfChaosComplete &&
                StatManager.HasEnergy(75) &&
                StatManager.HasStat(demonic, 500) &&
                StatManager.HasStat(worldsChaos, 90);
        }
        public void PublicCultRitualEffect() {
            StatManager.SpendEnergy(50);
            StatManager.IncreaseStat(demonWorshippers, 50);
            StatManager.IncreaseStat(money, 2500);
            StatManager.IncreaseStat(confidence, 10);
        }
        public bool PublicCultRitualCondition() {
            return StatManager.HasEnergy(50) &&
                isStartCultOfChaosComplete &&
                StatManager.HasStat(confidence, 50);
        }
        public void ContractWithDemonKingEffect() {
            StatManager.SpendEnergy(100);
            StatManager.IncreaseStat(demonWorshippers, 1000);
            StatManager.IncreaseStat(money, 50000);
            isContractWithDemonKingComplete = true;
            
            PathCompleted();
        }
        public bool ContractWithDemonKingCondition() {
            return !isContractWithDemonKingComplete &&
                StatManager.HasEnergy(100) &&
                StatManager.HasStat(demonic, 666) &&
                StatManager.HasStat(worldsChaos, 100) &&
                StatManager.HasStat(confidence, 100) &&
                StatManager.HasStat(demonWorshippers, 666);
        }

        public void PathCompleted() {
            Debug.Log("Demonic Path Complete");
        }

    }
}
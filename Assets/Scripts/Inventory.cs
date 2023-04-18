using HikiLife.Data;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : SerializedMonoBehaviour {

    public static Inventory Instance;
    void Awake() => Instance = this;

    public Dictionary<Item, int> items;

    public bool HasItem(Item item) {
        return HasItem(item, 1);
    }
    public bool HasItem(Item item, int amount) {
        return items.ContainsKey(item) && items[item] >= amount;
    }

    public void GiveItem(Item item) {
        GiveItem(item, 1);
    }

    public void GiveItem(Item item, int amount) {
        if (items.ContainsKey(item)) {
            items[item] = items[item] + amount;
        } else {
            items.Add(item, amount);
        }

    }

}
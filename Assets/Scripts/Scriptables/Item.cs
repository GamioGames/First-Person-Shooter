using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Ammunition
}

public class Item : ScriptableObject
{
    [Header("UI")]
    public Sprite icon;

    [Header("Common")]
    public string name;
    public ItemType itemType;
    public LayerMask hittableLayers = 1;
    public GameObject assosiatedPrefab;
}

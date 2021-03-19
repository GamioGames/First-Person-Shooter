using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapon", order = 1)]
public class Weapon : Item
{
    public GameObject alternatePrefab;

    [Header("Shoot Paramaters")]
    public float fireRange = 200;
    public ShotType shotType;
    public float recoilForce = 4f; //Fuerza de retroceso del arma
    public float fireRate = 0.6f;
    public int maxAmmo = 8;

    [Header("Weapon Parameters")]
    public float reloadTime = 1.5f;
    public float drawTime = 0.5f;
    public float hideTime = 0.5f;

    [Header("Damage")]
    public float damage = 10f;
    public float headDamage = 15f;

    [Header("Sounds & Visuals")]
    public GameObject flashEffectPrefab;
    public GameObject bulletHolePrefab;
    public BulletTrial_VFX bulletTrialPrefab;

    public Weapon()
    {
        itemType = ItemType.Weapon;
    }
}

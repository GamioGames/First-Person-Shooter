using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerHUD : MonoBehaviour
{
    public GameObject weaponInfoPrefab;

    private void OnEnable()
    {
        EventManager.current.NewGunEvent.AddListener(CreateWeaponInfo);
    }

    private void OnDisable()
    {
        EventManager.current.NewGunEvent.RemoveListener(CreateWeaponInfo);
    }

    public void CreateWeaponInfo()
    {
        Instantiate(weaponInfoPrefab, transform);
    }
}

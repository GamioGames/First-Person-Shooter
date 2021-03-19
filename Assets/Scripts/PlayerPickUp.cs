using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    private PickUp actualPickUp;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && actualPickUp != null)
        {
            PickUp();
        }
    }
    void PickUp()
    {
        switch (actualPickUp.weapon.itemType)
        {
            case ItemType.Weapon:
                PickUpWeapon(actualPickUp.weapon, actualPickUp.actualBullets);
                actualPickUp.Picked();
                EventManager.current.PickDownEvent.Invoke();
                break;
            case ItemType.Ammunition:
                break;
        }
    }

    void PickUpWeapon(Weapon pickWeapon, int actualBullets)
    {
        if (gameObject.GetComponent<PlayerWeaponManager>())
        {
            gameObject.GetComponent<PlayerWeaponManager>().AddOrUpdateWeapon(pickWeapon, actualBullets);
        }
        else
        {
            Debug.LogError("PlayerPickUp: No PlayerWeaponManager associaten in this game object");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PickUp>())
        {
            actualPickUp = other.gameObject.GetComponent<PickUp>();
            EventManager.current.PickUpEvent.Invoke(actualPickUp.weapon.name, actualPickUp.weapon.icon);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PickUp>())
        {
            actualPickUp = null;
            EventManager.current.PickDownEvent.Invoke();
        }
    }
}

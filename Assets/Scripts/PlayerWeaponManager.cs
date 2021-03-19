using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    public List<Weapon> startingWeapons = new List<Weapon>();

    [Header("References")]
    public Transform weaponParentSocket;
    public Transform defaultWeaponPosition;
    public Transform aimingPosition;
    public int activeWeaponIndex { get; private set; }

    private WeaponController[] weaponSlots = new WeaponController[2];

    // Start is called before the first frame update
    void Start()
    {
        activeWeaponIndex = -1;

        foreach (Weapon startingWeapon in startingWeapons)
        {
            AddWeapon(startingWeapon);
        }

        SwitchNextWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchNextWeapon();
        }
    }
    // Switch to specific index
    public void SwitchWeapon(int index, bool avoidHide = false)
    {
        if (index < 0 || index > weaponSlots.Length) return;
        StartCoroutine(SwitchWeaponD(index, avoidHide));
    }

    // To swith to the next weapon
    public void SwitchNextWeapon(bool avoidHide = false)
    {
        int tempIndex = (activeWeaponIndex + 1) % weaponSlots.Length;
        if (weaponSlots[tempIndex] == null)
            return;

        StartCoroutine(SwitchWeaponD(tempIndex, avoidHide));
    }

    private IEnumerator SwitchWeaponD(int index, bool avoidHide)
    {
        // To avoid the hide animation
        if (!avoidHide)
        {
            // Hide the actual weapon and wait
            foreach (WeaponController weapon in weaponSlots)
            {
                if (weapon != null && !avoidHide) weapon.Hide();
            }
            yield return new WaitForSeconds(0.5f);
        }

        // Hide all weapons
        foreach (WeaponController weapon in weaponSlots)
        {
            if (weapon != null) weapon.gameObject.SetActive(false);
        }

        weaponSlots[index].gameObject.SetActive(true);
        activeWeaponIndex = index;

        EventManager.current.NewGunEvent.Invoke();
        EventManager.current.UpdateBulletsEvent.Invoke(weaponSlots[activeWeaponIndex].currentAmmo, weaponSlots[activeWeaponIndex].weapon.maxAmmo);
    }

    
    public void AddOrUpdateWeapon(Weapon newWeapon, int actualAmmo = -1)
    {
        if (isFull())
        {
            UpdateWeapon(newWeapon, actualAmmo);
            SwitchWeapon(activeWeaponIndex, true);
        }
        else
        {
            AddWeapon(newWeapon, actualAmmo);
            SwitchNextWeapon(true);
        }
    }
    private void AddWeapon(Weapon newWeapon, int actualAmmo = -1)
    {
        //Debug.Log(actualAmmo);
        weaponParentSocket.position = defaultWeaponPosition.position;

        //Añadir arma al jugador pero no mostrarla
        for (int i = 0; i < weaponSlots.Length; i++)
        {
            if (weaponSlots[i] == null)
            {
                GameObject weaponClone = Instantiate(newWeapon.assosiatedPrefab, weaponParentSocket);
                WeaponController weaponCloneC = weaponClone.GetComponent<WeaponController>();
                weaponCloneC.owner = gameObject;
                weaponCloneC.gameObject.SetActive(false);
                if (actualAmmo > -1)
                {
                    weaponCloneC.SetAmmo(actualAmmo);
                }
                    

                weaponSlots[i] = weaponCloneC;
                return;
            }
        }
    }

    private void UpdateWeapon(Weapon newWeapon, int actualAmmo = -1)
    {
        DropWeapon();
        AddWeapon(newWeapon, actualAmmo);
    }

    private void DropWeapon()
    {
        Vector3 frontP = transform.position + transform.forward;
        GameObject newWeapon = Instantiate(weaponSlots[activeWeaponIndex].weapon.alternatePrefab, new Vector3(frontP.x, transform.position.y + 1.5f, frontP.z), Quaternion.Euler(60f, transform.rotation.y + 90f, 0f));
        if (newWeapon.GetComponent<Rigidbody>())
            newWeapon.GetComponent<Rigidbody>().AddForce(transform.forward, ForceMode.Impulse);
        if (newWeapon.GetComponent<PickUp>())
            newWeapon.GetComponent<PickUp>().Setup(weaponSlots[activeWeaponIndex].currentAmmo);

        Destroy(weaponSlots[activeWeaponIndex].gameObject);
        weaponSlots[activeWeaponIndex] = null;
    }

    // Check if the slots are full
    private bool isFull()
    {
        foreach (WeaponController w in weaponSlots)
        {
            if (w == null) return false;
        }
        return true;
    }
}

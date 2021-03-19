using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickUp : MonoBehaviour
{
    //public ItemType pickUpType;
    public Weapon weapon;
    public int actualBullets { get; set; }

    private void Awake()
    {
        actualBullets = weapon.maxAmmo;
    }

    public void Setup(int actualBullets)
    {
        this.actualBullets = actualBullets;
    }
    
    public void Picked()
    {
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }

    private void Update()
    {
        if (transform.position.y <= -20f) Destroy(gameObject);
    }
}

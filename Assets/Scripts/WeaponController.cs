using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShotType
{
    Manual,
    Automatic
}
public class WeaponController : MonoBehaviour
{
    public Weapon weapon;

    [Header("References")]
    public Transform weaponMuzzle;
    public Animator animator;

    public int currentAmmo { get; private set; }
    public GameObject owner { set; get; }

    private float lastTimeShoot = Mathf.NegativeInfinity;
    private Transform cameraPlayerTransform;
    private bool isReloading;

    private void Awake()
    {
        cameraPlayerTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        currentAmmo = weapon.maxAmmo;
        EventManager.current.UpdateBulletsEvent.Invoke(currentAmmo, weapon.maxAmmo);
    }

    public void SetAmmo(int newAmmo)
    {
        currentAmmo = newAmmo;
        EventManager.current.UpdateBulletsEvent.Invoke(currentAmmo, weapon.maxAmmo);
    }

    private void OnEnable()
    {
        isReloading = true;
        StartCoroutine(Draw(weapon.drawTime));
    }


    private void Update()
    {
        if(!isReloading)
        {
            if (weapon.shotType == ShotType.Manual)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    TryShoot();
                }
            }
            else if (weapon.shotType == ShotType.Automatic)
            {
                if (Input.GetButton("Fire1"))
                {
                    TryShoot();
                }
            }
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, Time.deltaTime * 5f);
    }

    private IEnumerator Draw(float time)
    {
        yield return new WaitForSeconds(time - 0.15f);
        isReloading = false;
    }

    private bool TryShoot()
    {
        if (lastTimeShoot + weapon.fireRate < Time.time)
        {
            if (currentAmmo >= 1)
            {
                HandleShoot();
                currentAmmo -= 1;
                EventManager.current.UpdateBulletsEvent.Invoke(currentAmmo, weapon.maxAmmo);
                return true;
            }
        }

        return false;
    }

    private void HandleShoot()
    {
        // Create the flash effect
        GameObject flashClone = Instantiate(weapon.flashEffectPrefab, weaponMuzzle.position, Quaternion.Euler(transform.forward.x, transform.forward.y, transform.forward.z), transform);
        Destroy(flashClone, 1f);

        AddRecoil();

        RaycastHit hit;
        if (Physics.Raycast(cameraPlayerTransform.position, cameraPlayerTransform.forward, out hit, weapon.fireRange, weapon.hittableLayers) && hit.collider.gameObject != owner)
        {
            GameObject bulletHoleClone = Instantiate(weapon.bulletHolePrefab, hit.point + hit.normal * 0.001f, Quaternion.LookRotation(hit.normal), hit.collider.gameObject.transform);
            Destroy(bulletHoleClone, 4f);

            // If Can be hitted
            if (hit.collider.gameObject.GetComponent<Damageable>())
            {
                hit.collider.gameObject.GetComponent<Damageable>().InflictDamage(weapon.damage, false, owner);
            }
        }

        AddBulletTrial();

        lastTimeShoot = Time.time;
    }

    private void AddRecoil()
    {
        transform.Rotate(-weapon.recoilForce, 0f, 0f);
        transform.position = transform.position - transform.forward * (weapon.recoilForce /50f);
    }

    private void AddBulletTrial()
    {
        if (weapon.bulletTrialPrefab == null) return;

        //GameObject bulletTrialEffect = Instantiate
    }

    IEnumerator Reload()
    {
        if (isReloading || currentAmmo == weapon.maxAmmo)
            yield break;

        isReloading = true;
        if (animator)
        {
            animator.SetTrigger("Reloading");
        }

        yield return new WaitForSeconds(weapon.reloadTime - 0.15f);
        currentAmmo = weapon.maxAmmo;
        EventManager.current.UpdateBulletsEvent.Invoke(currentAmmo, weapon.maxAmmo);
        Debug.Log("Recargada");
        isReloading = false;
    }

    public void Hide()
    {
        if (animator)
        {
            isReloading = false;
            animator.SetTrigger("Hiding");
        }
    }
}

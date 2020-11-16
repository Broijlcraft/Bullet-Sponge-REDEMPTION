using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunBase : MonoBehaviour
{
    [Header("Standard Settings")]
    public float reloadSpeed;
    public float damageAmount;
    public float fireRate;
    public float range;

    public int maxBulletAmmoCount;
    public int bulletsPerShot = 1;

    [Header("Misc")]
    public float hitMarkTimer = 5f;
    public LayerMask gunLayer;
    public Text bulletCountText;
    public Transform firePoint;
    public GameObject bulletHole;
    public GameObject crossHair;
    Quaternion startingRot;

    protected bool coolDown;
    protected int currentBulletAmmoCount;

    #region Get
    protected int GetCurrentBulletAmount()
    {
        int curBul = currentBulletAmmoCount;
        return curBul;
    }

    protected void SetCurrentBulletAmount(int newBulletAmount)
    {
        currentBulletAmmoCount = newBulletAmount;
        UpdateBulletCounter();
    }
    #endregion 

    private void Start()
    {
        currentBulletAmmoCount = maxBulletAmmoCount;
        UpdateBulletCounter();
        startingRot = transform.localRotation;
    }

    public virtual void Update()
    {
        if(PlayerMovement.single.pMode != PlayerMode.normal)
        {
            SetGunRotation();
        }
        else
        {
            transform.localRotation = startingRot;
        }

        if (Input.GetButton("Fire2"))
        {
            PlayerMovement.single.pMode = PlayerMode.aim;
        }
        else if(Input.GetButtonUp("Fire2"))
        {
            PlayerMovement.single.pMode = PlayerMode.normal;
        }

    }

    private void SetGunRotation()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit crossHit, range, gunLayer))
        {
            Vector3 dir = crossHit.point - transform.position;
            Quaternion lookDir = Quaternion.LookRotation(dir);
            transform.rotation = lookDir;
        }
    }

    public virtual void CoolDown()
    {
        coolDown = false;
    }

    public virtual void Fire()
    {
        SetGunRotation();
        if (Physics.Raycast(firePoint.position, firePoint.forward, out RaycastHit hit, range, gunLayer))
        {
            GameObject newDecal = Instantiate(bulletHole, hit.point, hit.transform.rotation);
            Destroy(newDecal, hitMarkTimer);
        }
        coolDown = true;
        Invoke(nameof(CoolDown), fireRate);
    }

    public virtual void SpreadShot()
    {
        SetGunRotation();
        for (int i = 0; i < bulletsPerShot; i++)
        {
            Vector3 dir = firePoint.transform.forward;
            Vector3 spread = Vector3.zero;
            spread += firePoint.transform.up * Random.Range(-.5f, .5f);
            spread += firePoint.transform.right * Random.Range(-.5f, .5f);

            dir += spread.normalized * Random.Range(0f, 0.2f);

            if (Physics.Raycast(firePoint.position,dir, out RaycastHit hit, range, gunLayer))
            {
                GameObject newDecal = Instantiate(bulletHole, hit.point, hit.transform.rotation);
                Destroy(newDecal, hitMarkTimer);
            }
            else
            {
                Debug.DrawRay(firePoint.transform.position, dir * range, Color.red);
            }
        }
        coolDown = true;
        Invoke(nameof(CoolDown), fireRate);
    }

    public virtual void Reload()
    {
        currentBulletAmmoCount = maxBulletAmmoCount;
        UpdateBulletCounter();
    }

    public void UpdateBulletCounter()
    {
        if (bulletCountText)
        {
            bulletCountText.text = GetCurrentBulletAmount().ToString();
        }
    }

}

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

    [Header("If Multiple Bullets Shot")]
    public float spread;
    public float delayBetweenBulletShot;

    [Header("Misc")]
    public float hitMarkTimer = 5f;
    public LayerMask gunLayer;
    public Text bulletCountText;
    public Transform firePoint;
    public GameObject bulletHole;
    public GameObject crossHair;
    Quaternion startingRot;

    protected bool coolDown;
    protected int currentBulletAmount;

    #region Get
    protected int GetCurrentBulletAmount()
    {
        int curBul = currentBulletAmount;
        return curBul;
    }

    protected void SetCurrentBulletAmount(int newBulletAmount)
    {
        currentBulletAmount = newBulletAmount;
        UpdateBulletCounter();
    }
    #endregion 

    private void Start()
    {
        currentBulletAmount = maxBulletAmmoCount;
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

    //public virtual void SpreadShot()
    //{
    //    if(Physics.Raycast(firePoint.position,transform.forward,))
    //}

    public virtual void Reload()
    {
        currentBulletAmount = maxBulletAmmoCount;
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

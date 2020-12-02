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
    public float bulletSpread;

    [Header("Misc")]
    public float hitMarkTimer = 5f;
    public LayerMask gunLayer;
    public Text bulletCountText;
    public Transform firePoint;
    public GameObject bulletHole;
    public GameObject crossHair;
    Quaternion startingRot;

    protected const PlayerMode playerModenormal = PlayerMode.normal;
    protected const PlayerMode playerModeAim = PlayerMode.aim;
    protected const PlayerMode playerModeFire = PlayerMode.fire;

    protected PlayerMovement player;
    protected bool coolDown;
    protected bool holding;
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
        player = PlayerMovement.single;
        currentBulletAmmoCount = maxBulletAmmoCount;
        UpdateBulletCounter();
        startingRot = transform.localRotation;
    }

    public virtual void Update()
    {
        if (player.pMode != playerModenormal)
        {
            SetGunRotation();
        }
        else
        {
            transform.localRotation = startingRot;
        }

        if (Input.GetButton("Fire2"))
        {
            player.pMode = playerModeAim;
        }
        else if(Input.GetButtonUp("Fire2"))
        {
            player.pMode = playerModenormal;
        }

        if (Input.GetButtonUp("Fire1") && player.pMode != playerModeAim)
        {
            if(player.pMode == PlayerMode.fire || !IsInvoking(nameof(Reload)))
            {
                player.pMode = playerModenormal;
            }
            if(holding || IsInvoking(nameof(IHold)))
            {
                CancelInvoke(nameof(IHold));
                holding = false;
            }
        }

        if (Input.GetButtonDown("Reload") && GetCurrentBulletAmount() < maxBulletAmmoCount && !IsInvoking(nameof(Reload)))
        {
            Invoke(nameof(Reload), reloadSpeed);
        }
    }

    protected void SetGunRotation()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit crossHit, range, gunLayer))
        {
            Vector3 dir = crossHit.point - transform.position;
            Quaternion lookDir = Quaternion.LookRotation(dir);
            transform.rotation = lookDir;
        }
    }

    protected virtual void CoolDown()
    {
        coolDown = false;
    }

    protected virtual void Fire()
    {
        SetGunRotation();
        if (Physics.Raycast(firePoint.position, firePoint.forward, out RaycastHit hit, range, gunLayer))
        {
            SpawnHitMark(hit);
            DamageCheck(hit);
        }
        coolDown = true;
    }

    protected virtual void SpreadShot()
    {
        
        SetGunRotation();
        if (Physics.Raycast(firePoint.position, firePoint.transform.forward, out RaycastHit normalHit, range, gunLayer))
        {
            SpawnHitMark(normalHit);
            DamageCheck(normalHit);
        }

        for (int i = 0; i < bulletsPerShot-1; i++)
        {
            Vector3 dir = firePoint.transform.forward;
            Vector3 spread = Vector3.zero;
            float newBulletspread = bulletSpread;
            if(player.pMode == PlayerMode.aim)
            {
                newBulletspread *= .5f;
            }
            
            spread += firePoint.transform.up * Random.Range(-newBulletspread, newBulletspread);
            spread += firePoint.transform.right * Random.Range(-newBulletspread, newBulletspread);
            

            dir += spread.normalized * Random.Range(0f, 0.2f);

            if (Physics.Raycast(firePoint.position,dir, out RaycastHit hit, range, gunLayer))
            {
                SpawnHitMark(hit);
                DamageCheck(hit);
            }
        }
        coolDown = true;
        Invoke(nameof(CoolDown), fireRate);
    }

    protected virtual void Reload()
    {
        SetCurrentBulletAmount(maxBulletAmmoCount);
        UpdateBulletCounter();
    }

    private void SpawnHitMark(RaycastHit hit)
    {
        if (!hit.transform.CompareTag("Enviorment"))
        {
            return;
        }
        Vector3 pos = hit.point;
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, hit.normal);
        GameObject newDecal = Instantiate(bulletHole, pos, rot);
        Destroy(newDecal, hitMarkTimer);
    }

    private void DamageCheck(RaycastHit hit)
    {
        if (hit.transform.CompareTag("Enemy"))
        {
            EnemyCombat enemy = hit.transform.GetComponent<EnemyCombat>();
            enemy.SetCurrentHealth(enemy.GetCurrentHealth() - damageAmount);
            Debug.Log("Hit");
        }
    }

    protected void IHold()
    {
        holding = true;
    }

    public void UpdateBulletCounter()
    {
        if (bulletCountText)
        {
            bulletCountText.text = GetCurrentBulletAmount().ToString();
        }
    }

}

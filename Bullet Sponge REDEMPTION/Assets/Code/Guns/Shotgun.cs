using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : GunBase
{
    public override void Update()
    {
        base.Update();
        if (Input.GetButtonDown("Fire1"))
        {
            if (player.pMode != playerModeAim)
            {
                Debug.Log("Changing Mode");
                player.pMode = playerModeFire;
            }
            if (GetCurrentBulletAmount() > 0 && !IsInvoking(nameof(CoolDown)))
            {
                SpreadShot();
                Invoke(nameof(CoolDown), fireRate);
                SetCurrentBulletAmount(GetCurrentBulletAmount() - 1);
            }
        }
        if (Input.GetButton("Fire1"))
        {
            if (!holding || !IsInvoking(nameof(IHold)))
            {
                Invoke(nameof(IHold), fireRate);
                return;
            }
            if (player.pMode != playerModeAim)
            {
                Debug.Log("Changing Mode");
                player.pMode = playerModeFire;
            }
            if (GetCurrentBulletAmount() > 0 && !IsInvoking(nameof(CoolDown)))
            {
                SpreadShot();
                Invoke(nameof(CoolDown), fireRate + 1f);
                SetCurrentBulletAmount(GetCurrentBulletAmount() - 1);
            }
        }
    }
}

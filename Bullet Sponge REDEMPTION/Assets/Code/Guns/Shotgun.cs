using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : GunBase
{
    public override void Update()
    {
        base.Update();
        if (Input.GetButton("Fire1"))
        {
            if (PlayerMovement.single.pMode != PlayerMode.aim)
            {
                PlayerMovement.single.pMode = PlayerMode.fire;
            }
            if (GetCurrentBulletAmount() > 0 && !IsInvoking(nameof(CoolDown)))
            {
                SpreadShot();
                SetCurrentBulletAmount(GetCurrentBulletAmount() - 1);
            }
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            PlayerMovement.single.pMode = PlayerMode.normal;
        }

        if (Input.GetButtonDown("Reload") && GetCurrentBulletAmount() < maxBulletAmmoCount && !IsInvoking(nameof(Reload)))
        {
            Invoke(nameof(Reload), reloadSpeed);
        }
    }
}

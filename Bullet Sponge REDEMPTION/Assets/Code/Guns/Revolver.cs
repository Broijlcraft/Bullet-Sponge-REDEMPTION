using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : GunBase
{
    // Update is called once per frame
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
            if (GetCurrentBulletAmount() > 0)
            {
                Fire();
                Invoke(nameof(CoolDown), 0.1f);
                Debug.Log("Fire once");
                SetCurrentBulletAmount(GetCurrentBulletAmount() - 1);
            }
        }
        if (Input.GetButton("Fire1"))
        {
            if (!holding || !IsInvoking(nameof(IHold)))
            {
                Invoke(nameof(IHold),fireRate);
                return;
            }
            if(player.pMode != playerModeAim)
            {
                Debug.Log("Changing Mode");
                player.pMode = playerModeFire;
            }
            if (GetCurrentBulletAmount() > 0 && !IsInvoking(nameof(CoolDown)))
            {
                Fire();
                Invoke(nameof(CoolDown), fireRate);
                Debug.Log("Keep Firing");
                SetCurrentBulletAmount(GetCurrentBulletAmount() - 1);
            }
        }
    }
}

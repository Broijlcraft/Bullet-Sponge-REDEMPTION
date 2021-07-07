using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunHolder : MonoBehaviour
{
    public List<GunBase> allGuns = new List<GunBase>();
    public int currentlyEquippedGun;
    // Start is called before the first frame update
    void Start()
    {
        GunBase[] allGunsArray = GetComponentsInChildren<GunBase>();
        foreach (GunBase gun in allGunsArray)
        {
            allGuns.Add(gun);
            gun.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if(currentlyEquippedGun < allGuns.Count-1)
            {
                currentlyEquippedGun++;
            }
            else
            {
                currentlyEquippedGun = 0;
            }
            TurnOffAllGuns();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (currentlyEquippedGun > 0)
            {
                currentlyEquippedGun--;
            }
            else
            {
                currentlyEquippedGun = allGuns.Count-1;
            }
            TurnOffAllGuns();
            
        }
        if (!allGuns[currentlyEquippedGun].gameObject.activeSelf)
        {
            allGuns[currentlyEquippedGun].gameObject.SetActive(true);
            allGuns[currentlyEquippedGun].UpdateBulletCounter();
        }
    }

    public void TurnOffAllGuns()
    {
        foreach (GunBase gun in allGuns)
        {
            gun.gameObject.SetActive(false);
        }
    }

}

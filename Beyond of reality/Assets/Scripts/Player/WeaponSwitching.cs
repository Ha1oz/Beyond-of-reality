using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{

    private  int selectedWeapon;
    public GameObject[] weapons;



    void Start()
    {
        GameManager.Instance.OnOrOffForceBar(selectedWeapon);
        GameManager.Instance.SwitchingWeaponIcon(selectedWeapon);
        SelectWeapon();
    }


    void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        

        if (Input.GetAxis("Mouse ScrollWheel") > 0f ) { //&& RayCastShootMainWeapon.canChangeWeapon
            if (selectedWeapon >= weapons.Length - 1)
            {
                selectedWeapon = 0;
                GameManager.Instance.OnOrOffForceBar(selectedWeapon);
                GameManager.Instance.SwitchingWeaponIcon(selectedWeapon);
            }
            else {
                selectedWeapon++;
                GameManager.Instance.OnOrOffForceBar(selectedWeapon);
                GameManager.Instance.SwitchingWeaponIcon(selectedWeapon);
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f) // && RayCastShootMainWeapon.canChangeWeapon
        {
            if (selectedWeapon <= 0)
            {
                selectedWeapon = weapons.Length - 1;
                GameManager.Instance.OnOrOffForceBar(selectedWeapon);
                GameManager.Instance.SwitchingWeaponIcon(selectedWeapon);
            }
            else
            {
                selectedWeapon--;
                GameManager.Instance.OnOrOffForceBar(selectedWeapon);
                GameManager.Instance.SwitchingWeaponIcon(selectedWeapon);
            }
        }

        if (previousSelectedWeapon!=selectedWeapon) {
            SelectWeapon();
        }
        
    }

    private void SelectWeapon()
    {
        for (int i = 0; i < weapons.Length; i++) {
            if (i == selectedWeapon)
            {
                weapons[i].SetActive(true);

            }
            else {
                weapons[i].SetActive(false);
            }
        }
    }
}


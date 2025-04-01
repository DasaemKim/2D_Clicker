using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponShopUI : MonoBehaviour
{
    public GameObject Shop;
    public GameObject[] weapons;  // 상점에 있는 무기 목록
    public Equiped[] Equipeds;
    public Enforced[] Enforceds;

    public Transform WeaponBag;

    Weapon selectWeaponData;
    private int selectWeaponIndex;

    private void Start()
    {
        Equipeds = new Equiped[WeaponBag.childCount];

        for (int i = 0; i < weapons.Length; i++)
        {
            Equipeds[i] = WeaponBag.GetChild(i).GetComponent<Equiped>();
            Equipeds[i].EquipBtnIndex = i;
            Enforceds[i].EnforceBtnIndex = i;
        }
    }

    public void BuyBtn()
    {
        //if (GameManager.Instance.player.playerData.weaponPoint >= )
    }

    

    public void OnClickEquipBtn()
    {
        if (selectWeaponData.isEquiped)
        {

        }
        else
        {
            GameManager.Instance.player.playerData.damage += selectWeaponData.Attack;
            GameManager.Instance.player.playerData.criticalRate += selectWeaponData.Critical_Rate;
        }
    }

    public void SelectWeapon(int index)
    {
        
    }

    public void OpenWeaponShop()
    {
        Shop.SetActive(true);
    }

    public void BackShopBtn()
    {
        Shop.SetActive(false);
    }
}

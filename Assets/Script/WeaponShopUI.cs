using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WeaponShopUI : MonoBehaviour
{
    public GameObject Shop;
    public GameObject[] weapons;  // 상점에 있는 무기 목록
    public Equiped[] Equipeds;
    public Enforced[] Enforceds;

    public Transform WeaponBag;
    public GameObject notEnoughPoint; // 포인트 부족 팝업 UI
    public List<WeaponUpgradeData> weaponUpgrades = new List<WeaponUpgradeData>();
    public static Inventory inst;
    
    public WeaponData selectWeaponData;
    public WeaponData SelectViewSlot; 
    private int selectWeaponIndex;

    public TMP_Text WeaponName;

    public Button WoodBtn;
    public Button RockBtn;



    private void Start()
    {
        WoodBtn.onClick.AddListener(WoodSword);
        RockBtn.onClick.AddListener(RockSword);
        
        Equipeds = new Equiped[WeaponBag.childCount];

        for (int i = 0; i < weapons.Length; i++)
        {
            Equipeds[i] = WeaponBag.GetChild(i).GetComponent<Equiped>();
            Equipeds[i].EquipBtnIndex = i;
            Enforceds[i].EnforceBtnIndex = i;
        }

        notEnoughPoint.SetActive(false); //비활성화 상태로 시작
    }

    private void Awake()
    {
        //inst = this;

    }
    public void BuyBtn()
    {
        //if (GameManager.Instance.player.playerData.weaponPoint >= )
    }

    

    public void OnClickEquipBtn()
    {
        /*if (selectWeaponData.isEquiped)
        {
            Debug.Log("이미 장착된 무기입니다");
        }
        else
        {
            GameManager.Instance.player.playerData.damage += selectWeaponData.Attack;
            GameManager.Instance.player.playerData.criticalRate += selectWeaponData.Critical_Rate;
            //selectWeaponData.isEquiped = true;
        }*/
    }

    public void SelectWeapon(int index)
    {
        selectWeaponIndex = index;
        selectWeaponData = weapons[index].GetComponent<WeaponData>();
    }

    public void EnforceWeapon()
    {
        if (selectWeaponData == null)
        {
            Debug.Log("선택된 무기가 없습니다");
            return;
        }

        WeaponUpgradeData upgradeData = weaponUpgrades.Find(w => w.WeaponName == selectWeaponData.weaponName);

        if (upgradeData == null)
        {
            Debug.Log("강화 데이터를 찾을 수 없다");
            return;
        }

        int enforceCost = upgradeData.BaseUpgradeCost * selectWeaponData.level; //무기별 강화 비용 계산

        if (GameManager.Instance.player.playerData.weaponPoint >= enforceCost)
        {
            GameManager.Instance.player.playerData.weaponPoint -= enforceCost;

            // 무기별 강화 능력치 반영

            selectWeaponData.level++;
            selectWeaponData.attack += upgradeData.AttackIncrease;
            selectWeaponData.criticalrate += upgradeData.criticalIncrease;

            Debug.Log($"강화 성공 Lv. {selectWeaponData.level}, 공격력 : {selectWeaponData.attack}, 치명타 확률 : {selectWeaponData.criticalrate}");
            UpdateUI();
        }
        else
        {
            notEnoughPoint.SetActive(true);
            Invoke("CloseNotEnoughPointPopup", 2f); // CloseNotEnoughPointPopup을 실행시키고 2초 뒤에 종료
        }
    }

    public void OpenWeaponShop()
    {
        Shop.SetActive(true);
    }

    public void BackShopBtn()
    {
        Shop.SetActive(false);
    }

    public void CloseNotEnoughPointPopup()
    {
        notEnoughPoint.SetActive(false);
    }

    public void UpdateUI()
    {

    }

    public void WoodSword()
    {
         //WoodBtn
    }

    public void RockSword()
    {
    
    }
}

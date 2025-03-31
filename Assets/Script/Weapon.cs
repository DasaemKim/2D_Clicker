using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class Weapon
{
    public string WeaponName;
    public int Attack;
    public float Critical;

    public int UpgradeLevel;
    public List<int> UpgradeAttack;
    public List<float> UpgradeCritical;

    public bool IsObatained = false;
}
public class WeaponManager : MonoBehaviour
{

    public List<Weapon> weaponLists;
    public Weapon equippedWeapon;
    public int UpgradeCost = 10;
    public int Balance = 0;
    public Button WeaponChangeButton;
    public Button BackButton;
    public GameObject WeaponBag;

    public TextMeshProUGUI WeaponName;
    public TextMeshProUGUI Attack;
    public TextMeshProUGUI Critical;
    public TextMeshProUGUI Upgrade_Cost;
    public TextMeshProUGUI Balance_;
    public TextMeshProUGUI WeaponLevel;
    public Button UpgradeButton;

    private int upgradeCount = 0;

    private void Start()
    {
        if (weaponLists.Count > 0)
        {
            EquipWeapon(weaponLists[0]);
        }

        UpgradeButton.onClick.AddListener(UpgradeWeapon);
        WeaponChangeButton.onClick.AddListener(ToggleWeaponBag);
    }

    private void UpgradeWeapon()
    {
        if (equippedWeapon == null || !equippedWeapon.IsObatained)
        {
            Debug.Log("아직 무기를 획득하지 못했습니다");
            return;
        }

        if (equippedWeapon.UpgradeLevel >= equippedWeapon.UpgradeAttack.Count)
        {
            Debug.Log("최대 레벨에 도달하셔서 올리실 수 없습니다.");
            return;
        }

        if (Balance < UpgradeCost)
        {
            Debug.Log("잔액이 부족합니다.");
            return;
        }

        Balance -= UpgradeCost;
        upgradeCount++;

        if (equippedWeapon.UpgradeLevel >= equippedWeapon.UpgradeAttack.Count)
        {
            equippedWeapon.UpgradeLevel = equippedWeapon.UpgradeAttack.Count - 1;
        }

        equippedWeapon.UpgradeLevel = UpgradeCost;

        equippedWeapon.Attack = equippedWeapon.UpgradeAttack[equippedWeapon.UpgradeLevel];
        equippedWeapon.Critical = equippedWeapon.UpgradeCritical[equippedWeapon.UpgradeLevel];

        UpdateWeaponUI();
    }

    private void EquipWeapon(Weapon weapon)
    {
        equippedWeapon = weapon;
        upgradeCount = equippedWeapon.UpgradeLevel;
        UpdateWeaponUI();
    }

    private void UpdateWeaponUI()
    {
        if (equippedWeapon != null)
        {
            WeaponName.text = $"{equippedWeapon.WeaponName}";
            Attack.text = $"공격력 : {equippedWeapon.Attack}";
            Critical.text = $"치명타 확률 : {(equippedWeapon.Critical * 100).ToString("F1")}%";
            WeaponLevel.text = $"LV. {equippedWeapon.UpgradeLevel + 1}";
            Upgrade_Cost.text = $"{UpgradeCost}";
            Balance_.text = $"{Balance}";
        }
        else
        {
            WeaponName.text = "???";
            Attack.text = "공격력 : 10";
            Critical.text = "치명타 확률 : 20%";
        }

        WeaponLevel.text = $"레벨 : {equippedWeapon.UpgradeLevel + 1}";
    }

    public void ToggleWeaponBag()
    {
        WeaponBag.SetActive(!WeaponBag.activeSelf);
    }

    public void CloseWeaponBag()
    {
        WeaponBag.SetActive(false);
    }
}

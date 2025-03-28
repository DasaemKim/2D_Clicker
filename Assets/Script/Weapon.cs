using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [System.Serializable]
    public class WeaponList
    {
        public string WeaponName;
        public int Attack;
        public float Critical;
        public float Critical_Damage;

        public int UpgradeLevel;
        public List<int> UpgradeAttack;
        public List<float> UpgradeCritical;
        public List<float> UpgradeCritical_Damage;
    }

    public List<WeaponList> weaponLists;
    public WeaponList equippedWeapon;
    public int UpgradeCost = 10;
    public int Balance = 0;

    public TextMeshProUGUI WeaponName;
    public TextMeshProUGUI Attack;
    public TextMeshProUGUI Critical;
    public TextMeshProUGUI Critical_Damage;
    public TextMeshProUGUI Upgrade_Cost;
    public TextMeshProUGUI Balance_;
    public Button UpgradeButton;

    private void Start()
    {
        if (weaponLists.Count > 0)
        {
            EquipWeapon(weaponLists[0]);
        }

        UpgradeButton.onClick.AddListener(UpgradeWeapon);
    }

    private void UpgradeWeapon()
    {
        if (equippedWeapon == null) return;

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

        equippedWeapon.Attack = equippedWeapon.UpgradeAttack[equippedWeapon.UpgradeLevel];
        equippedWeapon.Critical = equippedWeapon.UpgradeCritical[equippedWeapon.UpgradeLevel];
        equippedWeapon.Critical_Damage = equippedWeapon.UpgradeCritical_Damage[equippedWeapon.UpgradeLevel];

        equippedWeapon.UpgradeLevel++;

        UpdateWeaponUI();
    }

    private void EquipWeapon(WeaponList weapon)
    {
        equippedWeapon = weapon;
        UpdateWeaponUI();
    }

    private void UpdateWeaponUI()
    {
        if (equippedWeapon != null)
        {
            WeaponName.text = $"{equippedWeapon.WeaponName}";
            Attack.text = $"공격력 : {equippedWeapon.Attack}";
            Critical.text = $"치명타 확률 : {(equippedWeapon.Critical * 100).ToString("F1")}%";
            Critical_Damage.text = $"치명타 데미지 : {(equippedWeapon.Critical_Damage * 100).ToString("F1")}%";
            Upgrade_Cost.text = $"{UpgradeCost}";
            Balance_.text = $"{Balance}";
        }
    }
}

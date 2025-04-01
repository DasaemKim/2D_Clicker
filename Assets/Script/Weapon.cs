using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public List<WeaponData> weaponLists;
    public WeaponData equippedWeapon;
    public int UpgradeCost = 10;
    public Button WeaponChangeButton;
    public Button BackButton;
    public GameObject WeaponBag;

    public TextMeshProUGUI WeaponName;
    public TextMeshProUGUI Attack;
    public TextMeshProUGUI Critical;
    public TextMeshProUGUI Upgrade_Cost;
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
            Debug.Log("���� ���⸦ ȹ������ ���߽��ϴ�");
            return;
        }

        equippedWeapon.UpgradeLevel++; // ���� ����

        equippedWeapon.UpgradeLevel = UpgradeCost;

        // �ö� �ɷ�ġ �ݿ�
        equippedWeapon.Attack += equippedWeapon.UpgradeAttack;
        equippedWeapon.Critical_Rate += equippedWeapon.UpgradeCritical;

        UpdateWeaponUI();
    }

    private void EquipWeapon(WeaponData weapon)
    {
        equippedWeapon = weapon;
        upgradeCount = equippedWeapon.UpgradeLevel;
        UpdateWeaponUI();
    }

    private void UpdateWeaponUI()
    {
        if (equippedWeapon != null) // ������ ���Ⱑ �ִٸ�
        {
            WeaponName.text = $"{equippedWeapon.WeaponName}";
            Attack.text = $"���ݷ� : {equippedWeapon.Attack}";
            Critical.text = $"ġ��Ÿ Ȯ�� : {(equippedWeapon.Critical_Rate * 100).ToString("F1")}%";
            WeaponLevel.text = $"LV. {equippedWeapon.UpgradeLevel + 1}";
            Upgrade_Cost.text = $"{UpgradeCost}";
        }
        else // ��ȹ���� ����
        {
            WeaponName.text = "???";
            Attack.text = "���ݷ� : 10";
            Critical.text = "ġ��Ÿ Ȯ�� : 20%";
        }

        WeaponLevel.text = $"���� : {equippedWeapon.UpgradeLevel + 1}";
    }

    public void ToggleWeaponBag() // ����UI Ȱ��ȭ
    {
        WeaponBag.SetActive(true);
    }

    public void CloseWeaponBag() // �ڷΰ��� ��ư
    {
        WeaponBag.SetActive(false);
    }
}

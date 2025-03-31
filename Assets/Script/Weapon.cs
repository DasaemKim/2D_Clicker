using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [System.Serializable]
    public class WeaponList // 무기 정보 저장
    {
        public string WeaponName; // 무기 이름
        public int Attack; // 무기 공격력
        public float Critical; // 무기 치명타 확률

        public int UpgradeLevel; // 무기의 업그레이드 레벨
        public List<int> UpgradeAttack; // 무기 업그레이드 공격력
        public List<float> UpgradeCritical; // 무기 업그레이드 크리티컬

        public bool IsObatained = false; // 미획득한 무기
    }

    public List<WeaponList> weaponLists; // 게임에서 사용할 수 있는 무기들 저장
    public WeaponList equippedWeapon; // 현재 장착된 무기
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
            Debug.Log("아직 무기를 획득하지 못했습니다");
            return;
        }

        if (equippedWeapon.UpgradeLevel >= equippedWeapon.UpgradeAttack.Count)
        {
            Debug.Log("최대 레벨에 도달하셔서 올리실 수 없습니다.");
            return;
        }

        equippedWeapon.UpgradeLevel++; // 레벨 증가

        if (equippedWeapon.UpgradeLevel >= equippedWeapon.UpgradeAttack.Count)
        {
            equippedWeapon.UpgradeLevel = equippedWeapon.UpgradeAttack.Count - 1;
        }

        equippedWeapon.UpgradeLevel = UpgradeCost;

        // 올라간 능력치 반영
        equippedWeapon.Attack = equippedWeapon.UpgradeAttack[equippedWeapon.UpgradeLevel];
        equippedWeapon.Critical = equippedWeapon.UpgradeCritical[equippedWeapon.UpgradeLevel];

        UpdateWeaponUI();
    }

    private void EquipWeapon(WeaponList weapon)
    {
        equippedWeapon = weapon;
        upgradeCount = equippedWeapon.UpgradeLevel;
        UpdateWeaponUI();
    }

    private void UpdateWeaponUI()
    {
        if (equippedWeapon != null) // 장착된 무기가 있다면
        {
            WeaponName.text = $"{equippedWeapon.WeaponName}";
            Attack.text = $"공격력 : {equippedWeapon.Attack}";
            Critical.text = $"치명타 확률 : {(equippedWeapon.Critical * 100).ToString("F1")}%";
            WeaponLevel.text = $"LV. {equippedWeapon.UpgradeLevel + 1}";
            Upgrade_Cost.text = $"{UpgradeCost}";
        }
        else // 미획득한 무기
        {
            WeaponName.text = "???";
            Attack.text = "공격력 : 10";
            Critical.text = "치명타 확률 : 20%";
        }

        WeaponLevel.text = $"레벨 : {equippedWeapon.UpgradeLevel + 1}";
    }

    public void ToggleWeaponBag() // 무기UI 활성화
    {
        WeaponBag.SetActive(!WeaponBag.activeSelf);
    }

    public void CloseWeaponBag() // 뒤로가기 버튼
    {
        WeaponBag.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour
{
    public WeaponData data;

    public TextMeshProUGUI nameText; // 이름
    public TextMeshProUGUI levelText; // 레벨
    public TextMeshProUGUI attackText; // 공격력
    public TextMeshProUGUI criticalrate; // 치명타 확률
    public Image IconImage; // 무기 이미지

    public void Equip()
    {
        if (data == null)
        {
            nameText.text = "???";
            levelText.text = "Lv. ?";
            attackText.text = "공격력 : ???";
            criticalrate.text = "치명타 확률 : ???";
            IconImage.sprite = null;
        }
        else
        {
            nameText.text = data.weaponName;
            levelText.text = "Lv. " + data.level;
            attackText.text = "공격력 : " + data.attack;
            criticalrate.text = "치명타 확률 : " + data.criticalrate;
            IconImage.sprite = data.iconSpr;
        }
    }

    
    
    public void UpdateUI()
    {
        nameText.text = data.weaponName;
        levelText.text = data.level.ToString();
        attackText.text = data.attack.ToString();
        criticalrate.text = data.criticalrate.ToString("N1");
        IconImage.sprite = data.iconSpr;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class UIButtonController : MonoBehaviour
{
    [Header("UI 버튼")]
    public Button criBtn;
    public Button autoNumBtn;
    public Button coinGetBtn;

    [Header("UI 재화")]
    public TextMeshProUGUI weaponText;
    public TextMeshProUGUI statText;

    public Player player;
    
    
    private void Start()
    {
        criBtn.onClick.AddListener(OnClickCriBtn);
        autoNumBtn.onClick.AddListener(OnClickAutoNumBtn);
        coinGetBtn.onClick.AddListener(OnClickCoinGetBtn);
        
        RefreshUI();
    }

    public void OnClickCriBtn()
    {
        UIBtnManager.Instance.statUpgrade.CriDamageUpgrade();
    }

    public void OnClickAutoNumBtn()
    {
        UIBtnManager.Instance.statUpgrade.AutoNumUpgrade();
    }

    public void OnClickCoinGetBtn()
    {
        UIBtnManager.Instance.statUpgrade.CoinGetUpgrade();
    }

    public void RefreshUI()
    {
        weaponText.text = player.characterData.weaponPoint.ToString("N0");
        statText.text = player.characterData.statPoint.ToString("N0");
    }
}
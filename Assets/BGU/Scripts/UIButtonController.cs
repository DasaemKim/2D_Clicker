using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class UIButtonController : MonoBehaviour
{
    [Header("UI 버튼")] public Button criBtn;
    public Button autoNumBtn;
    public Button coinGetBtn;

    [Header("UI 재화")] public TextMeshProUGUI weaponText;
    public TextMeshProUGUI statText;

    [Header("UI 패널")]
    public GameObject errorPanel;

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

    // 플레이어의 보유 금액에 따라 변경
    public void RefreshUI()
    {
        weaponText.text = player.characterData.weaponPoint.ToString("N0");
        statText.text = player.characterData.statPoint.ToString("N0");
    }

    // ErrorPanel 패널 생성
    public void ErrorPanel()
    {
        errorPanel.SetActive(true);
    }
    
    // TODO
    // 에러 패널이 3 ~ 5초 뒤에 사라지도록 설정
    //
    
    // CloseErrorPanel 패널 생성
    public void CloseErrorPanel()
    {
        errorPanel.SetActive(false);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonController : MonoBehaviour
{
    [Header("UI 버튼")]
    public Button criBtn;
    public Button autoNumBtn;
    public Button coinGetBtn;

    [Header("UI 재화")]
    public TextMeshProUGUI weaponText;  // 현재 보유 무기 포인트
    public TextMeshProUGUI statText;    // 현재 보유 스탯 포인트
    public TextMeshProUGUI criInfoText; // 현재 크리 강화 수치
    public TextMeshProUGUI autoInfoText; // 현재 자동 공격 강화 수치
    public TextMeshProUGUI coinInfoText; // 현재 코인 획득 강화 수치
    public TextMeshProUGUI criUpgradeText; // 현재 강화 표시
    public TextMeshProUGUI autoUpgradeText; // 현재 강화 표시
    public TextMeshProUGUI coinUpgradeText; // 현재 강화 표시

    [Header("UI 패널")]
    public GameObject errorPanel;   // 에러 패널

    public StatUpgrade statUpgrade;
    public AttackSystem attackSystem;

    public void Start()
    {
        // 버튼 활성화
        criBtn.onClick.AddListener(OnClickCriBtn);
        autoNumBtn.onClick.AddListener(OnClickAutoNumBtn);
        coinGetBtn.onClick.AddListener(OnClickCoinGetBtn);

        RefreshUI();
    }

    // CriBtn을 눌렀을 때
    public void OnClickCriBtn()
    {
        UIBtnManager.Instance.statUpgrade.CriDamageUpgrade();
    }

    // AutoNumBtn을 눌렀을 때
    public void OnClickAutoNumBtn()
    {
        UIBtnManager.Instance.statUpgrade.AutoNumUpgrade();
    }

    // CoinGetBtn을 눌렀을 때
    public void OnClickCoinGetBtn()
    {
        UIBtnManager.Instance.statUpgrade.CoinGetUpgrade();
    }

    // 플레이어의 행동에 따른 UI Text 변경
    public void RefreshUI()
    {
        // 플레이어가 보유 중인 포인트 변경
        weaponText.text = GameManager.Instance.player.playerData.weaponPoint.ToString("N0");
        statText.text = GameManager.Instance.player.playerData.statPoint.ToString("N0");

        // 플레이어가 업그레이드 시 텍스트 변경
        criInfoText.text = GameManager.Instance.player.playerData.criDamage.ToString("N2") + " %";
        autoInfoText.text = GameManager.Instance.player.playerData.autoNum.ToString("N1") + " 초/회";
        coinInfoText.text = GameManager.Instance.player.playerData.coinGet.ToString("N2") + " %";

        // 플레이어가 업그레이드 시 포인트 값 변경
        criUpgradeText.text = statUpgrade.criUpgradePoint.ToString("N0");
        autoUpgradeText.text = statUpgrade.autoUpgradePoint.ToString("N0");
        coinUpgradeText.text = statUpgrade.coinUpgradePoint.ToString("N0");
        
        // 비용에 따라 글자 색상 적용
        criUpgradeText.color = SetColor(statUpgrade.criUpgradePoint, GameManager.Instance.player.playerData.statPoint);
        autoUpgradeText.color = SetColor(statUpgrade.autoUpgradePoint, GameManager.Instance.player.playerData.statPoint);
        coinUpgradeText.color = SetColor(statUpgrade.coinUpgradePoint, GameManager.Instance.player.playerData.statPoint);
    }

    // ErrorPanel 패널 생성
    public void ErrorPanel()
    {
        errorPanel.SetActive(true);
    }

    // ErrorPanel 패널 사라짐
    public IEnumerator CloseErrorPanel()
    {
        yield return new WaitForSeconds(1);
        errorPanel.SetActive(false);
    }
    
    // 비용에 따른 색 변화
    public Color SetColor(float upgradeCost, float currentStatPoint)    // 업그레이드 비용, 현재 보유 재화
    {
        if (currentStatPoint >= upgradeCost)
        {
            return Color.black; // 재화가 충분할 때
        }
        else
        {
            return Color.red;   // 재화가 부족할 때
        }
    }
}
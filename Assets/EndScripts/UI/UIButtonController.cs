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

    public Player player;
    public StatUpgrade statUpgrade;

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
        weaponText.text = player.characterData.weaponPoint.ToString("N0");
        statText.text = player.characterData.statPoint.ToString("N0");

        // 플레이어가 업그레이드 시 텍스트 변경
        criInfoText.text = player.characterData.criDamage.ToString("N2") + " %";
        autoInfoText.text = player.characterData.autoNum.ToString("N1") + " 초/회";
        coinInfoText.text = player.characterData.coinGet.ToString("N2") + " %";

        // 플레이어가 업그레이드 시 포인트 값 변경
        criUpgradeText.text = statUpgrade.criUpgradePoint.ToString("N0");
        autoUpgradeText.text = statUpgrade.autoUpgradePoint.ToString("N0");
        coinUpgradeText.text = statUpgrade.coinUpgradePoint.ToString("N0");
    }

    // ErrorPanel 패널 생성
    public void ErrorPanel()
    {
        errorPanel.SetActive(true);
    }

    // TODO 완료
    // 에러 패널이 3초 뒤에 사라지도록 설정
    // 코루틴을 이용하여 1초뒤 사라지도록 설정
    // ErrorPanel 패널 사라짐
    public IEnumerator CloseErrorPanel()
    {
        yield return new WaitForSeconds(1);
        errorPanel.SetActive(false);
    }
}
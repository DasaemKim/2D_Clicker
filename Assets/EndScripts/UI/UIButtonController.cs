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
    public TextMeshProUGUI weaponText;
    public TextMeshProUGUI statText;
    public TextMeshProUGUI criInfoText; // 현재 크리 강화 수치
    public TextMeshProUGUI autoInfoText; // 현재 자동 공격 강화 수치
    public TextMeshProUGUI coinInfoText; // 현재 코인 획득 강화 수치
    public TextMeshProUGUI criUpgradeText; // 현재 강화 표시
    public TextMeshProUGUI autoUpgradeText; // 현재 강화 표시
    public TextMeshProUGUI coinUpgradeText; // 현재 강화 표시

    [Header("UI 패널")]
    public GameObject errorPanel;

    public Player player;
    public StatUpgrade statUpgrade;

    public void Start()
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

        criInfoText.text = player.characterData.criDamage.ToString("N2") + " %";
        autoInfoText.text = player.characterData.autoNum.ToString("N1") + " 초/회";
        coinInfoText.text = player.characterData.coinGet.ToString("N2") + " %";

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
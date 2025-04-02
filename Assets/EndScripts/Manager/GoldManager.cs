using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public float popupDuration = 1.0f;

    //골드 텍스트 UI
    public TMPro.TextMeshProUGUI goldText;

    private void Start()
    {
        RefreshGoldUI();
    }

    //골드 추가
    public void AddGold(int amount)
    {
        GameManager.Instance.player.playerData.statPoint += amount;
        RefreshGoldUI();
    }

    //골드 사용(성공 여부 반환)
    public bool UseGold(int amount)
    {
        if (GameManager.Instance.player.playerData.statPoint >= amount)
        {
            GameManager.Instance.player.playerData.statPoint -= amount;
            RefreshGoldUI();
            return true;
        }
        else
        {
            ShowErrorPopup();
            return false;
        }
    }

    //골드 UI갱신
    public void RefreshGoldUI()
    {
        if (goldText != null &&
             GameManager.Instance.player != null &&
             GameManager.Instance.player.playerData != null)
        {
            goldText.text = $"Gold: {GameManager.Instance.player.playerData.statPoint}";
        }
    }

    //골드 부족 시 에러 팝업 표시
    private void ShowErrorPopup()
    {
        var controller = UIBtnManager.Instance.uiBtnController;
        controller.ErrorPanel();
        controller.StartCoroutine(controller.CloseErrorPanel());
    }
}

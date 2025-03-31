using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public float popupDuration = 1.0f;

    public TMPro.TextMeshProUGUI goldText;

    private void Start()
    {
        RefreshGoldUI();
    }
    public void AddGold(int amount)
    {
        GameManager.Instance.playerData.gold += amount;
        RefreshGoldUI();
    }
    public bool UseGold(int amount)
    {
        if (GameManager.Instance.playerData.gold >= amount)
        {
            GameManager.Instance.playerData.gold -= amount;
            RefreshGoldUI();
            return true;
        }
        else
        {
            ShowErrorPopup();
            return false;
        }
    }
    public void RefreshGoldUI()
    {
        if (goldText != null)
        {
            goldText.text = $"Gold: {GameManager.Instance.playerData.gold}";
        }
    }
    private void ShowErrorPopup()
    {
        var controller = UIBtnManager.Instance.uiBtnController;
        controller.ErrorPanel();
        controller.StartCoroutine(controller.CloseErrorPanel());
    }
}

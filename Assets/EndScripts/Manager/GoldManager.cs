using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public GameObject errorPopup;
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
            StartCoroutine(ShowErrorPopup());
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
    private IEnumerator ShowErrorPopup()
    {
        errorPopup.SetActive(true);
        yield return new WaitForSeconds(popupDuration);
        errorPopup.SetActive(false);
    }
}

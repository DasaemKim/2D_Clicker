using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonController : MonoBehaviour
{
    public Button criBtn;
    public Button autoNumBtn;
    public Button coinGetBtn;
    
    private void Start()
    {
        criBtn.onClick.AddListener(OnClickCriBtn);
        autoNumBtn.onClick.AddListener(OnClickAutoNumBtn);
        coinGetBtn.onClick.AddListener(OnClickCoinGetBtn);
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
}

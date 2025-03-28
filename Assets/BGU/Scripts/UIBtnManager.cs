using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBtnManager : MonoBehaviour
{
    public static UIBtnManager Instance;
    
    public UIButtonController uiBtnController;
    
    public StatUpgrade statUpgrade;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}

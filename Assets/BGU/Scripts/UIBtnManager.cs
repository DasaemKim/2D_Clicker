using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class UIBtnManager : MonoBehaviour
{
    public static UIBtnManager Instance;
    
    public StatUpgrade statUpgrade;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}

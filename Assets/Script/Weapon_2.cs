using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

[Serializable]
public class Weapon_2
{
    public Weapon_2(WeaponData data)
    {
        Attack = data.Attack;
        Critical_Rate = data.Critical_Rate;
        UpgradeLevel = data.UpgradeLevel;
    }

    public float Attack;
    public float Critical_Rate;
    public int UpgradeLevel;
}

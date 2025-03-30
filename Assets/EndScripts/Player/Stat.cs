using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat
{
    public int baseAttack;
    public int baseCriticalRate;
    public int baseCriticalDamage;
    public int baseGoldBonus;

    public int FinalAttack(PlayerData playerData, Weapon.WeaponList weapon)
    {
        return baseAttack+weapon.Attack;
    }
    public float FinalCriticaRate(PlayerData playerData, Weapon.WeaponList weapon)
    {
        return baseCriticalRate + weapon.Critical;
    }
    public int FinalCriticalDamage(PlayerData playerData, Weapon.WeaponList weapon)
    {
        int upgradeBonus = 50 * playerData.criticalDamageLevel;
        return Mathf.RoundToInt(weapon.Critical_Damage * 100) + upgradeBonus;
    }
    public int FinalGoldBonus(PlayerData playerData)
    {
        return 100 * playerData.goldBonusLevel;
    }
}

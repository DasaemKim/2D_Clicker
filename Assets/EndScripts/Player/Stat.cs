using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat
{
    public int baseAttack;
    public float baseCriticalRate;
    public int baseCriticalDamage;
    public int baseGoldBonus;

    public int FinalAttack(PlayerData playerData, Weapon.WeaponList weapon)
    {
        return baseAttack + weapon.Attack;
    }
    public float FinalCriticaRate(PlayerData playerData, Weapon.WeaponList weapon)
    {
        return baseCriticalRate + weapon.Critical;
    }
    public int FinalCriticalDamage(PlayerData playerData)
    {
        return baseCriticalDamage + (50 * playerData.criticalDamageLevel);
    }
    public int FinalGoldBonus(PlayerData playerData)
    {
        return baseGoldBonus + (100 * playerData.goldBonusLevel);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public PlayerData(CharacterData data)
    {
        damage = data.damage;
        autoNum = data.autoNum;
        stage = 1;
        gold = 0;
        criticalDamageLevel = 0;
        autoAttackLevel = 0; 
        goldBonusLevel = 0;
        equippedWeaponLevel = 0;
        selectedCharacter = "";
        equippedWeaponName = "";
        weaponList = new List<Weapon>();
    }

    public float damage;
    public float criticalRate;
    public int stage;
    public int gold;
    
    public float autoNum;

    //스탯 업그레이드 레벨
    public int criticalDamageLevel;
    public int autoAttackLevel;
    public int goldBonusLevel;

    //무기 정보
    public string equippedWeaponName;
    public int equippedWeaponLevel;

    public List<Weapon> weaponList;

    public Weapon equippedWeapon;

    //기타
    public int statPoint;
    public string selectedCharacter;

    public int baseGoldBonus;

    public float FinalAttack()
    {
        return damage + equippedWeapon.Attack;
    }
    public float FinalCriticaRate()
    {
        return criticalRate + equippedWeapon.Critical;
    }
    public float FinalCriticalDamage()
    {
        return (damage * (1.5f + (criticalDamageLevel * 0.1f)));
    }
    public int FinalGoldBonus() 
    {
        return baseGoldBonus + (100 * goldBonusLevel);
    }
}

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
        statPoint = data.statPoint;
        weaponPoint = data.weaponPoint;
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
    public int statPoint;
    public float coinGet;
    public float criDamage;

    public float autoNum;

    //스탯 업그레이드 레벨
    public int criticalDamageLevel;
    public int autoAttackLevel;
    public int goldBonusLevel;

    //무기 정보
    public string equippedWeaponName;
    public int equippedWeaponLevel;

    public List<Weapon> weaponList;

    public Weapon equippedWeapon = null;

    //기타
    public int weaponPoint;
    public string selectedCharacter;

    public int baseGoldBonus;

    
}

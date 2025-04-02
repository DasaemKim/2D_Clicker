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
        stage = 0;
        step = 0;
        enemyIndex = 0;
        statPoint = data.statPoint;
        weaponPoint = data.weaponPoint;
        criticalDamageLevel = 0;
        autoAttackLevel = 0; 
        goldBonusLevel = 0;
        criUpgradeCost = 10f;
        autoUpgradeCost = 10f;
        coinUpgradeCost = 10f;
        equippedWeaponLevel = 0;
        selectedCharacter = "";
        equippedWeaponName = "";
        weaponList = new List<Weapon>();
    }

    
    public float damage;
    public float criticalRate;
    public int statPoint;
    public float coinGet;
    public float criDamage;

    public float autoNum;

    //스테이지 정보
    public int enemyIndex;
    public int step;
    public int stage;
    public float enemyHP;

    //업그레이드 소모량 저장
    public float criUpgradeCost;
    public float autoUpgradeCost;
    public float coinUpgradeCost;

    //스탯 업그레이드 레벨
    public int criticalDamageLevel;
    public int autoAttackLevel;
    public int goldBonusLevel;

    //Stage UI연동용 레벨
    public int criUpLevel;
    public int autoUpLevel;
    public int coinGetUpLevel;

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

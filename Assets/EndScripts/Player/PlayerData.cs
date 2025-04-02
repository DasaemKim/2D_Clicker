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
        criUpgradeCost = 10f;
        autoUpgradeCost = 10f;
        coinUpgradeCost = 10f;
        equippedWeaponLevel = 0;
        selectedCharacter = "";
        equippedWeaponName = "";
        weaponList = new List<Weapon>();

        isAutoAttack = false;
    }

    //스탯관련
    public float damage; //기본 공격력
    public float criticalRate; //치명타 확률
    public int statPoint; //보유 골드
    public float coinGet; //추가 코인획득량
    public float criDamage; //치명타 데미지
    public float autoNum; //자동 공격 횟수

    //스테이지 정보
    public int enemyIndex;
    public int step;
    public int stage;
    public float enemyHP;

    //업그레이드 소모량 저장
    public int criUpgradeCost;
    public int autoUpgradeCost;
    public int coinUpgradeCost;

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

    // 자동 공격 상태 
    public bool isAutoAttack;

}

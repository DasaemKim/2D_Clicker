using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public int stage;
    public int gold;

    //스탯 업그레이드 레벨
    public int criticalDamageLevel;
    public int autoAttackLevel;
    public int goldBonusLevel;

    //무기 정보
    public string equippedWeaponName;
    public int equippedWeaponLevel;

    //기타
    public int statPoint;
    public string selectedCharacter;
}

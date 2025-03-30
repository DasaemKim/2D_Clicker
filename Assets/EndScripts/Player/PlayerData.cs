using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public int stage;
    public int gold;

    //���� ���׷��̵� ����
    public int criticalDamageLevel;
    public int autoAttackLevel;
    public int goldBonusLevel;

    //���� ����
    public string equippedWeaponName;
    public int equippedWeaponLevel;

    //��Ÿ
    public int statPoint;
    public string selectedCharacter;
}

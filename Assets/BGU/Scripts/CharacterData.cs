using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Data/ CharacterDataData")]
public class CharacterData : ScriptableObject
{
    // 플레이어 데이터 관리
    [Header("Character Stats")]
    public float damage;
    public float criDamage;
    public int coinPoint;
    public int levelPoint;
}

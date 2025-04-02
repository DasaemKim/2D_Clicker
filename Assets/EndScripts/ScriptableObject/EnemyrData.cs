using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster", menuName = "New Monster")]
public class EnemyData : ScriptableObject
{
    [Header("MonsterInfo")]
    public string Name;
    public float Health;
    public float HealthGrowth; // Step에 따른 성장체력
    public int FallStatPoint; // 스탯포인트 드랍량
    public int FallWeaponPoint; // 무기포인트 드랍량
}

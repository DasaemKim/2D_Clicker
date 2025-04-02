using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster", menuName = "New Monster")]
public class EnemyData : ScriptableObject
{
    [Header("MonsterInfo")]
    public string Name;
    public float Health;
    public float HealthGrowth;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Circle,
    Triangle,
    Square
}

[CreateAssetMenu(fileName = "Monster", menuName = "New Monster")]
public class EnemyData : ScriptableObject
{
    [Header("MonsterInfo")]
    public string Name;
    public float Health;
    public float HealthGrowth;

    public float GetHealth(int stage)
    {
        return Health + stage * HealthGrowth;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private Enemy enemy;
    private EnemyStat enemyStat;

    public event Action OnUpdateEnemy;

    public Enemy Enemy
    {
        get { return enemy; }
        set { enemy = value; }
    }

    public EnemyStat EnemyStat
    {
        get { return enemyStat; }
        set { enemyStat = value; }
    }

    private void Awake()
    {
        Instance = this;
    }
}

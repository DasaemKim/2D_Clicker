using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    public List<EnemyObject> EnemyObject;

    public Action<GameObject> returnPool;

    private PoolManager PoolManager;

    public EnemyData EnemyData;

    public event Action UpdateStageNum;
    public event Action UpdateStepNum;
    public event Action UpdateEnemyName;
    public event Action UpdateEnemyHealth;

    public event Action EnemyStatUpdate;
    //public Enemy Enemy;

    public int Step;
    public int EnemyIndex;
    public int SpawnCount = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        foreach (var enemy in PoolManager.Instance.Prefabs)
        {
            EnemyObject.Add(enemy.GetComponent<EnemyObject>());
        }

        EnemyData = EnemyObject[EnemyIndex].EnemyData;

        GameManager.Instance.EnemyStat = new EnemyStat(EnemyData);

        PoolManager = PoolManager.Instance;

        Respawn();
    }

    public void Respawn()
    {
        if (SpawnCount < 11)
        {
            PoolManager.GetObject(EnemyIndex, Vector2.zero, Quaternion.identity);
        }
        else
        {
            SpawnCount = 1;            
            EnemyIndex++;

            if (EnemyIndex > 2)
            {
                EnemyData = EnemyObject[EnemyIndex].EnemyData;

                GameManager.Instance.EnemyStat = new EnemyStat(EnemyData);
                GameManager.Instance.EnemyStat.MaxHealth += EnemyData.HealthGrowth;

                EnemyStatUpdate?.Invoke();
                Step++;
                EnemyIndex = 0;
                UpdateStepNum?.Invoke();
            }

            EnemyData = EnemyObject[EnemyIndex].EnemyData;

            GameManager.Instance.EnemyStat = new EnemyStat(EnemyData);

            PoolManager.GetObject(EnemyIndex, Vector2.zero, Quaternion.identity);
        }

        SpawnCount++;
        UpdateEnemyName?.Invoke();
        UpdateStageNum?.Invoke();
    }



    public void Onbt()
    {
        GameManager.Instance.Enemy.TakeDamage(100);
        UpdateEnemyHealth?.Invoke();
    }
    
}

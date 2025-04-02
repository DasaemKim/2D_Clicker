using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

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

        PoolManager = PoolManager.Instance;

        Respawn();
    }

    public void Respawn()
    {
        if (SpawnCount < 11)
        {
            PoolManager.GetObject(transform.position, Quaternion.identity, EnemyIndex);
        }
        else
        {
            SpawnCount = 0;            
            EnemyIndex++;

            if (EnemyIndex > 2)
            {
                EnemyIndex = 0;
                Step++;
                UpdateStepNum?.Invoke();
            }

            EnemyData = EnemyObject[EnemyIndex].EnemyData;

            PoolManager.GetObject(transform.position, Quaternion.identity, EnemyIndex);
        }

        SpawnCount++;
        UpdateEnemyName?.Invoke();
        UpdateStageNum?.Invoke();
    }



    public void Onbt()
    {
        GameManager.Instance.Enemy.TakeDamage(100);
    }
    
}

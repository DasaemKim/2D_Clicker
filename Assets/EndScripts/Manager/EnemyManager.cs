using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    private PoolManager PoolManager;

    public event Action UpdateStageNum; // 스테이지 텍스트 액션
    public event Action UpdateEnemyName; // Enemy이름 변경 액션

    public int Step;
    public int EnemyIndex;
    public int SpawnCount;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PoolManager = PoolManager.Instance;

        if (GameManager.Instance.player.playerData != null)
        {
            SpawnCount = GameManager.Instance.player.playerData.stage;
            EnemyIndex = GameManager.Instance.player.playerData.enemyIndex;
            Step = GameManager.Instance.player.playerData.step;
        }

        Respawn();
        GameManager.Instance.RestartAutoAttack();
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
            }

            PoolManager.GetObject(transform.position, Quaternion.identity, EnemyIndex);
        }

        SpawnCount++;

        GameManager.Instance.player.playerData.stage = SpawnCount - 1;
        GameManager.Instance.player.playerData.step = Step;
        GameManager.Instance.player.playerData.enemyIndex = EnemyIndex;

        UpdateEnemyName?.Invoke();
        UpdateStageNum?.Invoke();

        
        GameManager.Instance.SaveGame();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;

public class Enemy : MonoBehaviour, IPoolable
{
    public EnemyObject EnemyObject;

    public Action<GameObject> returnPool;

    public float bounceForce = 5f;  // 튕기는 힘
    public float deathGravityScale = 2f;  // 죽을 때 중력 증가

    public float MaxHealth;
    public float CurrentHealth;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 0;

        rb.velocity = Vector2.zero;  // 기존 움직임 정지

        MaxHealth = GameManager.Instance.EnemyStat.MaxHealth;
        CurrentHealth = GameManager.Instance.EnemyStat.CurrentHealth;

        EnemyObject = EnemyManager.Instance.EnemyObject[EnemyManager.Instance.EnemyIndex];
        EnemyObject.GetEnemyData();
    }

    private void OnEnable()
    {
        GameManager.Instance.Enemy = this;
    }

    void Die()
    {
        rb.gravityScale = 1;
        rb.AddForce(new Vector2(UnityEngine.Random.Range(-4f, 4f), 7f), ForceMode2D.Impulse);

        EnemyManager.Instance.Respawn();

        GameManager.Instance.EnemyStat.CurrentHealth = GameManager.Instance.EnemyStat.MaxHealth;
        CurrentHealth = GameManager.Instance.EnemyStat.CurrentHealth;

        Invoke(nameof(OnDespawn), 3f);
    }


    public void Initialize(Action<GameObject> returnaction)
    {
        returnPool = returnaction;
    }

    public void OnSpawn()
    {
        
    }

    public void OnDespawn()
    {
        rb.gravityScale = 0;
        returnPool?.Invoke(gameObject);
    }

    public void TakeDamage(int damage)
    {
        GameManager.Instance.EnemyStat.CurrentHealth -= damage;
        CurrentHealth = GameManager.Instance.EnemyStat.CurrentHealth;

        if (GameManager.Instance.EnemyStat.CurrentHealth <= 0)
        {
            Die();
        }
    }

}

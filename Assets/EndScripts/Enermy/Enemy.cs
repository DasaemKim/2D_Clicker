using System;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IPoolable
{
    public EnemyObject EnemyObject;

    public EnemyStat EnemyStat;

    public Action<GameObject> returnPool;

    public event Action OnHealthChanged;

    public float bounceForce = 5f;  // ƨ��� ��
    public float deathGravityScale = 2f;  // ���� �� �߷� ����

    public float MaxHealth;
    public float CurrentHealth;

    private Rigidbody2D rb;


    private void Start()
    {
        GameManager.Instance.Enemy = this;

        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 0;

        rb.velocity = Vector2.zero;  // ���� ������ ����

        EnemyObject = EnemyManager.Instance.EnemyObject[EnemyManager.Instance.EnemyIndex];
        EnemyObject.GetEnemyData();

        EnemyStat = new EnemyStat(EnemyManager.Instance.EnemyData);
        MaxHealth = EnemyManager.Instance.Step > 0 ? EnemyStat.MaxHealth + (EnemyManager.Instance.Step * EnemyManager.Instance.EnemyData.HealthGrowth) : EnemyStat.MaxHealth;
        CurrentHealth = EnemyStat.CurrentHealth;

        OnHealthChanged?.Invoke();
    }

    private void OnEnable()
    {
        GameManager.Instance.Enemy = this;

        if (EnemyStat != null)
        {
            MaxHealth = EnemyManager.Instance.Step > 0 ? EnemyStat.MaxHealth + (EnemyManager.Instance.Step * EnemyManager.Instance.EnemyData.HealthGrowth) : EnemyStat.MaxHealth;
            CurrentHealth = MaxHealth;
        }

        OnHealthChanged += StageUI.Instance.UpdateEnemyHP;
    }

    private void OnDisable()
    {
        OnHealthChanged -= StageUI.Instance.UpdateEnemyHP;
    }

    void Die()
    {
        float randomX = (UnityEngine.Random.value < 0.5f) ? -5f : 5f;
        float randomTorque = (UnityEngine.Random.value < 0.5f) ? -10f : 10f;

        rb.gravityScale = 1;
        rb.AddForce(new Vector2(randomX, 7f), ForceMode2D.Impulse);
        rb.AddTorque(randomTorque, ForceMode2D.Impulse); // ȸ���� �߰�

        EnemyManager.Instance.Respawn();

        OnHealthChanged?.Invoke();

        EnemyStat.CurrentHealth = MaxHealth;
        CurrentHealth = EnemyStat.CurrentHealth;

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

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            Die();
            return;
        }

        OnHealthChanged?.Invoke();
    }

    

}

using System;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IPoolable
{
    public EnemyObject EnemyObject;

    public EnemyStat EnemyStat;

    public Action<GameObject> returnPool; // ������Ʈ ��Ȱ��ȭ �׼�

    public event Action OnHealthChanged; // ü�� ������Ʈ

    public float bounceForce = 5f;  // ƨ��� ��
    public float deathGravityScale = 2f;  // ���� �� �߷� ����

    public float MaxHealth;
    public float CurrentHealth;

    private Rigidbody2D rb;


    private void Start()
    {
        GameManager.Instance.Enemy = this;

        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 0; // �߷°� 0

        rb.velocity = Vector2.zero;  // ���� ������ ����

        EnemyObject = EnemyManager.Instance.EnemyObject[EnemyManager.Instance.EnemyIndex];
        EnemyObject.GetEnemyData();

        EnemyStat = new EnemyStat(EnemyManager.Instance.EnemyData); // ���ο� �� ���� �� ���� ����
        MaxHealth = EnemyManager.Instance.Step > 0 ? EnemyStat.MaxHealth + (EnemyManager.Instance.Step * EnemyManager.Instance.EnemyData.HealthGrowth) : EnemyStat.MaxHealth; // ���������� ���� ü�� ����
        CurrentHealth = EnemyStat.CurrentHealth; // ü�� �ʱ�ȭ

        OnHealthChanged?.Invoke();
    }

    private void OnEnable()
    {
        GameManager.Instance.Enemy = this; // �� �ʱ�ȭ

        if (EnemyStat != null) // ���������� ���� ü�� ����
        {
            MaxHealth = EnemyManager.Instance.Step > 0 ? EnemyStat.MaxHealth + (EnemyManager.Instance.Step * EnemyManager.Instance.EnemyData.HealthGrowth) : EnemyStat.MaxHealth;
            CurrentHealth = MaxHealth;
        }

        OnHealthChanged += StageUI.Instance.UpdateEnemyHP; // ü�� ������Ʈ �̺�Ʈ ����
    }

    private void OnDisable()
    {
        OnHealthChanged -= StageUI.Instance.UpdateEnemyHP;
    }

    void Die()
    {
        float randomX = (UnityEngine.Random.value < 0.5f) ? -5f : 5f; // �¿� ƨ��� ��
        float randomTorque = (UnityEngine.Random.value < 0.5f) ? -10f : 10f; // ȸ�� ��

        rb.gravityScale = 1; // �߷°� �ʱ�ȭ
        rb.AddForce(new Vector2(randomX, 7f), ForceMode2D.Impulse); // �� ��� �� �¿�� ƨ���
        rb.AddTorque(randomTorque, ForceMode2D.Impulse); // �� ��� �� ȸ���� �߰�

        EnemyManager.Instance.Respawn(); // �� ��� �� ������

        OnHealthChanged?.Invoke(); // ü�� ������Ʈ

        EnemyStat.CurrentHealth = MaxHealth;
        CurrentHealth = EnemyStat.CurrentHealth;

        Invoke(nameof(OnDespawn), 3f); // ��� ���� 3�� �ڿ� ��Ȱ��ȭ
    }


    public void Initialize(Action<GameObject> returnaction) // Ǯ�Ŵ��� ReturnObject�Լ� �����ͼ� �׼� ���
    {
        returnPool = returnaction;
    }

    public void OnSpawn()
    {
        
    }

    public void OnDespawn() // ������Ʈ ��Ȱ��ȭ
    {
        rb.gravityScale = 0;
        returnPool?.Invoke(gameObject);
    }

    public void TakeDamage(int damage) // �� �޴� ����
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            Die();
            return;
        }

        OnHealthChanged?.Invoke(); // ü�� ������Ʈ
    }

    

}

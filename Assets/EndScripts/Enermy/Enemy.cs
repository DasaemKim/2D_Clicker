using System;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IPoolable
{
    public EnemyObject EnemyObject;

    public EnemyStat EnemyStat;

    public Action<GameObject> returnPool; // ������Ʈ ��Ȱ��ȭ �׼�

    public event Action OnHealthChanged; // ü�� ������Ʈ
    public event Action<float> OnDamageText; // ������ �ؽ�Ʈ

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
        OnDamageText += StageUI.Instance.CreateText.CreateTextDamage;

        if (gameObject == null)
        {
            Debug.LogError("[Enemy] gameObject�� null�Դϴ�.");
        }

        if (transform == null)
        {
            Debug.LogError("[Enemy] transform�� null�Դϴ�.");
        }

        Debug.Log("[Enemy] OnEnable �����");

    }

    private void OnDisable()
    {
        OnHealthChanged -= StageUI.Instance.UpdateEnemyHP;
        OnDamageText -= StageUI.Instance.CreateText.CreateTextDamage;
    }

    void Die()
    {
        float randomX = (UnityEngine.Random.value < 0.5f) ? -3f : 3f; // �¿� ƨ��� ��
        float randomTorque = (UnityEngine.Random.value < 0.5f) ? -10f : 10f; // ȸ�� ��

        rb.gravityScale = 1; // �߷°� �ʱ�ȭ
        rb.AddForce(new Vector2(randomX, 3f), ForceMode2D.Impulse); // �� ��� �� �¿�� ƨ���
        rb.AddTorque(randomTorque, ForceMode2D.Impulse); // �� ��� �� ȸ���� �߰�

        EnemyManager.Instance.Respawn(); // �� ��� �� ������

        OnHealthChanged?.Invoke(); // ü�� ������Ʈ

        StageUI.Instance.DelayedHP.fillAmount = 1;
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

    public void TakeDamage(float damage) // �� �޴� ����
    {
        CurrentHealth -= damage;

        OnDamageText?.Invoke(damage);

        if (CurrentHealth <= 0)
        {
            Die();
            return;
        }

        OnHealthChanged?.Invoke(); // ü�� ������Ʈ
    }

    

}

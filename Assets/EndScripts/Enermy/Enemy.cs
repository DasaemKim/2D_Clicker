using System;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IPoolable
{
    public EnemyObject EnemyObject;

    public EnemyStat EnemyStat;

    public Action<GameObject> returnPool; // 오브젝트 비활성화 액션

    public event Action OnHealthChanged; // 체력 업데이트
    public event Action<float> OnDamageText; // 데미지 텍스트

    public float MaxHealth;
    public float CurrentHealth;

    private Rigidbody2D rb;


    private void Start()
    {
        GameManager.Instance.Enemy = this;

        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 0; // 중력값 0

        rb.velocity = Vector2.zero;  // 기존 움직임 정지

        EnemyObject = EnemyManager.Instance.EnemyObject[EnemyManager.Instance.EnemyIndex];
        EnemyObject.GetEnemyData();

        EnemyStat = new EnemyStat(EnemyManager.Instance.EnemyData); // 새로운 적 생성 시 스탯 생성
        MaxHealth = EnemyManager.Instance.Step > 0 ? EnemyStat.MaxHealth + (EnemyManager.Instance.Step * EnemyManager.Instance.EnemyData.HealthGrowth) : EnemyStat.MaxHealth; // 스테이지에 따라 체력 증가
        CurrentHealth = EnemyStat.CurrentHealth; // 체력 초기화

        OnHealthChanged?.Invoke();
        OnDamageText += StageUI.Instance.CreateText.CreateTextDamage;
    }

    private void OnEnable()
    {
        GameManager.Instance.Enemy = this; // 적 초기화

        if (EnemyStat != null) // 스테이지에 따라 체력 증가
        {
            MaxHealth = EnemyManager.Instance.Step > 0 ? EnemyStat.MaxHealth + (EnemyManager.Instance.Step * EnemyManager.Instance.EnemyData.HealthGrowth) : EnemyStat.MaxHealth;
            CurrentHealth = MaxHealth;
        }

        OnHealthChanged += StageUI.Instance.UpdateEnemyHP; // 체력 업데이트 이벤트 구독
        OnDamageText += StageUI.Instance.CreateText.CreateTextDamage;

        if (gameObject == null)
        {
            Debug.LogError("[Enemy] gameObject가 null입니다.");
        }

        if (transform == null)
        {
            Debug.LogError("[Enemy] transform이 null입니다.");
        }

        Debug.Log("[Enemy] OnEnable 실행됨");

    }

    private void OnDisable()
    {
        if (StageUI.Instance != null)
        {
            OnHealthChanged -= StageUI.Instance.UpdateEnemyHP;
            OnDamageText -= StageUI.Instance.CreateText.CreateTextDamage;
        }
    }

    void Die()
    {
        float randomX = (UnityEngine.Random.value < 0.5f) ? -3f : 3f; // 좌우 튕기는 값
        float randomTorque = (UnityEngine.Random.value < 0.5f) ? -10f : 10f; // 회전 값

        rb.gravityScale = 1; // 중력값 초기화
        rb.AddForce(new Vector2(randomX, 3f), ForceMode2D.Impulse); // 적 사망 시 좌우로 튕기기
        rb.AddTorque(randomTorque, ForceMode2D.Impulse); // 적 사망 시 회전력 추가

        EnemyManager.Instance.Respawn(); // 적 사망 시 리스폰

        OnHealthChanged?.Invoke(); // 체력 업데이트

        StageUI.Instance.DelayedHP.fillAmount = 1;
        EnemyStat.CurrentHealth = MaxHealth;
        CurrentHealth = EnemyStat.CurrentHealth;

        Invoke(nameof(OnDespawn), 3f); // 사망 이후 3초 뒤에 비활성화
    }


    public void Initialize(Action<GameObject> returnaction) // 풀매니저 ReturnObject함수 가져와서 액션 등록
    {
        returnPool = returnaction;
    }

    public void OnSpawn()
    {
        
    }

    public void OnDespawn() // 오브젝트 비활성화
    {
        rb.gravityScale = 0;
        returnPool?.Invoke(gameObject);
    }

    public void TakeDamage(float damage) // 적 받는 피해
    {
        CurrentHealth -= damage;

        OnDamageText?.Invoke(damage);

        if (CurrentHealth <= 0)
        {
            Die();
            return;
        }

        OnHealthChanged?.Invoke(); // 체력 업데이트
    }

    

}

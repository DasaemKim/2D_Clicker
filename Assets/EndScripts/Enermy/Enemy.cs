using System;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IPoolable
{
    public EnemyData EnemyData;

    public Action<GameObject> returnPool; // 오브젝트 비활성화 액션

    public event Action OnHealthChanged; // 체력 업데이트

    public float MaxHealth;
    public float CurrentHealth;

    private Rigidbody2D rb;

    public GameObject MonsterDieParticle;

    private void Start()
    {
        GameManager.Instance.Enemy = this;

        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 0; // 중력값 0

        rb.velocity = Vector2.zero;  // 기존 움직임 정지
        
        MaxHealth = EnemyManager.Instance.Step > 0 ? EnemyData.Health + (EnemyManager.Instance.Step * EnemyData.HealthGrowth) : EnemyData.Health; // Step에 따른 Enemy 최대체력 증가
        CurrentHealth = MaxHealth; // 체력 초기화

        if (GameManager.Instance.player.playerData.enemyHP > 0 && GameManager.Instance.player.playerData.enemyHP < CurrentHealth) // 저장한 체력 불러오기
        {
            CurrentHealth = GameManager.Instance.player.playerData.enemyHP;
        }

        OnHealthChanged?.Invoke(); // 체력 리프레쉬
    }

    private void OnEnable()
    {
        GameManager.Instance.Enemy = this; // 적 초기화

        MaxHealth = EnemyManager.Instance.Step > 0 ? EnemyData.Health + (EnemyManager.Instance.Step * EnemyData.HealthGrowth) : EnemyData.Health; // Step에 따른 Enemy 최대체력 증가
        CurrentHealth = MaxHealth;

        OnHealthChanged += StageUI.Instance.UpdateEnemyHP; // 체력 업데이트 이벤트 구독
    }

    private void OnDisable()
    {
        if (StageUI.Instance != null)
        {
            OnHealthChanged -= StageUI.Instance.UpdateEnemyHP;
        }
    }

    void Die()
    {
        // 골드 획득량 추가
        
        float dropStatPoint = EnemyManager.Instance.Step > 0 ? EnemyManager.Instance.Step * 1.2f  : 1; // Step에 따른 드랍 포인트량 증가

        dropStatPoint = GameManager.Instance.player.playerData.coinGet > 0 ? dropStatPoint * GameManager.Instance.player.playerData.coinGet : dropStatPoint; // 포인트 획득량 적용
        
        GameManager.Instance.player.playerData.statPoint += (int)(EnemyData.FallStatPoint * (dropStatPoint)); // 포인트 추가
        GameManager.Instance.player.playerData.weaponPoint += (int)(EnemyData.FallWeaponPoint * (dropStatPoint)); // 포인트 추가

        UIBtnManager.Instance.uiBtnController.RefreshUI(); // 업그레이드 수치 변경 시 최신화

        float randomX = (UnityEngine.Random.value < 0.5f) ? -3f : 3f; // 좌우 튕기는 값
        float randomTorque = (UnityEngine.Random.value < 0.5f) ? -10f : 10f; // 회전 값

        rb.gravityScale = 1; // 중력값 초기화
        rb.AddForce(new Vector2(randomX, 3f), ForceMode2D.Impulse); // 적 사망 시 좌우로 튕기기
        rb.AddTorque(randomTorque, ForceMode2D.Impulse); // 적 사망 시 회전력 추가

        StageUI.Instance.DelayedHP.fillAmount = 1; // 체력바
        CurrentHealth = MaxHealth;

        GameManager.Instance.player.playerData.enemyHP = CurrentHealth; // 저장한 Enemy체력 초기화

        EnemyManager.Instance.Respawn(); // 적 사망 시 리스폰

        OnHealthChanged?.Invoke(); // 체력 업데이트

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

    public void TakeDamage(float damage, bool isCri) 
    {
        CurrentHealth -= damage; // 적 받는 피해

        StageUI.Instance.CreateText.CreateTextDamage(damage, isCri); // 데미지 텍스트 출력

        if (CurrentHealth <= 0)
        {
            Die();
            SpawnParticleAtMonster(gameObject, MonsterDieParticle); // 적 사망 시 파티클 생성
            return;
        }

        OnHealthChanged?.Invoke(); // 체력 업데이트
        GameManager.Instance.player.playerData.enemyHP = CurrentHealth; // Enemy체력 저장
        GameManager.Instance.SaveGame();
    }

    public void SpawnParticleAtMonster(GameObject monster, GameObject particlePrefab) // 몬스터 위치에 파티클 생성
    {
        if (monster != null) // 몬스터가 null이 아닌지 확인
        {
            Vector3 spawnPosition = monster.transform.position; // 몬스터의 월드 좌표 가져오기
            Instantiate(particlePrefab, spawnPosition, Quaternion.identity); // 몬스터 위치에 파티클 생성
        }
    }
}

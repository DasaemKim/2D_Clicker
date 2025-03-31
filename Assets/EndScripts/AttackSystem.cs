using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AttackSystem : MonoBehaviour
{

    public GameManager gameManager;

    public GameObject HitRed;
    public GameObject HitYello;

    //public Button AutoAttackBtn;
    public bool isAutoAttacking = false;
    private float attackRate = 0f; // 초기 공격 속도 (0초에 1회)
    private readonly float maxAttackRate = 10f; // 최대 공격 속도 (초당 10회)

    public void Start()
    {

    }


    public void Attack()
    {
        Debug.Log("공격 실행!");

        GameManager.Instance.Enemy.TakeDamage(100);
        SpawnParticle(HitRed);

    }

    public void OnAutoAttack()
    {
        // 버튼 클릭 시 공격 속도 0.3회/초 증가 (최대 10회/초 제한)
        attackRate = Mathf.Min(attackRate + 0.3f, maxAttackRate);
        Debug.Log($"자동공격 실행! 현재 공격 속도: {attackRate:F1}회/초");

        if (!isAutoAttacking)
        {
            isAutoAttacking = true;
            StartCoroutine(AutoAttackCoroutine());
        }
    }

    private IEnumerator AutoAttackCoroutine()
    {
        while (isAutoAttacking)
        {
            Attack();
            yield return new WaitForSeconds(1f / attackRate); // 초당 공격 횟수에 따라 대기 시간 조정
            Debug.Log($"공격 대기 시간: {1f / attackRate:F2}초");
        }
    }

    public void Critical()
    {
        Debug.Log("크리티컬 공격 실행!");
    }


    public void SpawnParticle(GameObject particlePrefab) // 마우스 위치에 파티클 생성
    {
        Vector3 mousePosition = Input.mousePosition; // 마우스 포인터 위치 (스크린 좌표)
        mousePosition.z = Camera.main.nearClipPlane; // z값을 카메라의 클리핑 평면으로 설정

        Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(mousePosition); // 월드 좌표로 변환

        Instantiate(particlePrefab, spawnPosition, Quaternion.identity); // 마우스 위치에 파티클 생성
        Debug.Log("마우스 위치에 파티클 생성!");
    }
}
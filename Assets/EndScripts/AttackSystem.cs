using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AttackSystem : MonoBehaviour
{

    public GameManager gameManager;
    public Player player;

    public GameObject HitRed;
    public GameObject HitYello;

    public Coroutine autoAttackCoroutine; // 자동 공격 코루틴

    public void Start()
    {
        player = GameManager.Instance.player;
    }

    public void Attack()
    {
        Debug.Log("공격 실행!");

        bool isCri = player.CheckCriticalHit();
        GameManager.Instance.Enemy.TakeDamage(player.GetDamage(isCri));

        // 치명타일 경우 메시지 출력
        if (isCri)
        {
            Debug.Log("치명타 공격 발생!");

            SpawnParticle(HitRed);
            return;
        }

        // 파티클 생성
        SpawnParticle(HitYello);

    }

    // 자동 공격 시작
    public void StartAutoAttack()
    {
        if (autoAttackCoroutine != null)
        {
            StopCoroutine(autoAttackCoroutine);
        }
        autoAttackCoroutine = StartCoroutine(AutoAttackCoroutine());
        
    }

    // 자동 공격 코루틴
    public IEnumerator AutoAttackCoroutine()
    {
        while (true)
        {
            Attack();
            yield return new WaitForSeconds(GameManager.Instance.player.characterData.autoNum); // 0.3초마다 공격 실행
            Debug.Log("자동 공격 시작!");
        }
    }

    public void SpawnParticle(GameObject particlePrefab) // 마우스 위치에 파티클 생성
    {
        Vector3 mousePosition = Input.mousePosition; // 마우스 포인터 위치 (스크린 좌표)
        mousePosition.z = Camera.main.nearClipPlane; // z값을 카메라의 클리핑 평면으로 설정

        Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(mousePosition); // 월드 좌표로 변환

        //Instantiate(particlePrefab, spawnPosition, Quaternion.identity); // 마우스 위치에 파티클 생성
        Debug.Log("마우스 위치에 파티클 생성!");
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AttackSystem : MonoBehaviour
{

    public GameManager gameManager;

    public GameObject HitRed;
    public GameObject HitYello;

    public AudioSource sfxSource;
    public AudioClip hitSfx;

    public Coroutine autoAttackCoroutine;


    public void Attack() 
    {
        bool isCri = GameManager.Instance.player.CheckCriticalHit();
        GameManager.Instance.Enemy.TakeDamage(GameManager.Instance.player.GetDamage(isCri), isCri);

        // 치명타일 경우 메시지 출력
        if (isCri)
        {
            SpawnParticle(HitRed);
            return;
        }

        if (sfxSource != null && hitSfx != null)
        {
            sfxSource.PlayOneShot(hitSfx);
        }
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
            yield return new WaitForSeconds(GameManager.Instance.player.playerData.autoNum);
            Attack();
        }
    }

    public void SpawnParticle(GameObject particlePrefab) // 마우스 위치에 파티클 생성
    {
        Vector3 mousePosition = Input.mousePosition; // 마우스 포인터 위치 (스크린 좌표)
        mousePosition.z = Camera.main.nearClipPlane; // z값을 카메라의 클리핑 평면으로 설정

        Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(mousePosition); // 월드 좌표로 변환

        Instantiate(particlePrefab, spawnPosition, Quaternion.identity); // 마우스 위치에 파티클 생성
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 골드를 소모하여 플레이어 스탯 업그레이드 로직 구성
public class StatUpgrade : MonoBehaviour
{
    // CharacterData의 주소값 없음

    // 버튼의 스탯 포인트 기본값
    public float criUpgradePoint;
    public float autoUpgradePoint;
    public float coinUpgradePoint;

    public AttackSystem attackSystem;

    //TODO 진행 중
    // 현재 강화 시 값은 올라가나 프로그램 종료 후 다시 키면 초기 값으로 돌아감.
    // 내일 UI텍스트를 추가하여 RefreshUI 메서드를 통하여 나가더라도 값이 고정되도록 변경
    // PlayerData.cs에 데이더 저장 진행

    private Coroutine upgradeCoroutine;
    private System.Action upgradeAction;

    // 크리티컬 데미지 증가
    public void CriDamageUpgrade()
    {
        // 플레이어의 스탯 포인트가 강화 스탯 포인트 보다 클때
        if (GameManager.Instance.player.playerData.statPoint >= criUpgradePoint)
        {
            GameManager.Instance.player.playerData.statPoint -= (int)criUpgradePoint; // 업그레이드 포인트 만큼 스탯 포인트에서 값 감소
            GameManager.Instance.player.playerData.criDamage =
                0.1f + GameManager.Instance.player.playerData.criDamage * 1.5f; // 스탯 포인트 증가값
            criUpgradePoint *= 1.5f; // 강화 포인트 사용 시 다음 사용할 때 1.5배 추가 증가

            UIBtnManager.Instance.uiBtnController.RefreshUI(); // 업그레이드 수치 변경 시 최신화
        }
        else
        {
            UIBtnManager.Instance.uiBtnController.ErrorPanel(); // 에러 발생 시 에러 패널 생성
            StartCoroutine(UIBtnManager.Instance.uiBtnController.CloseErrorPanel()); // 코루틴을 이용하여 3초뒤 사라짐
        }

        GameManager.Instance.SaveGame();
    }

    // 자동 공격 횟수 증가
    public void AutoNumUpgrade()
    {
        // 플레이어의 스탯 포인트가 강화 스탯 포인트 보다 클때
        if (GameManager.Instance.player.playerData.statPoint >= autoUpgradePoint)
        {
            GameManager.Instance.player.playerData.statPoint -= (int)autoUpgradePoint; // 업그레이드 포인트 만큼 스탯 포인트에서 값 감소
            // GameManager.Instance.player.PlayerData.autoNum = 1 - GameManager.Instance.player.PlayerData.autoNum + 0.3f; // 스탯 포인트 증가값
            GameManager.Instance.player.playerData.autoNum *= 0.9f; // 0.9배씩 감소
            autoUpgradePoint *= 1.5f; // 강화 포인트 사용 시 다음 사용할 때 1.5배 추가 증가

            UIBtnManager.Instance.uiBtnController.RefreshUI(); // 업그레이드 수치 변경 시 최신화
            attackSystem.StartAutoAttack();
        }
        else
        {
            UIBtnManager.Instance.uiBtnController.ErrorPanel(); // 에러 발생 시 에러 패널 생성
            StartCoroutine(UIBtnManager.Instance.uiBtnController.CloseErrorPanel()); // 코루틴을 이용하여 3초뒤 사라짐
        }

        GameManager.Instance.SaveGame();
    }

    // 코인 획득량 증가
    public void CoinGetUpgrade()
    {
        // 플레이어의 스탯 포인트가 강화 스탯 포인트 보다 클때
        if (GameManager.Instance.player.playerData.statPoint >= coinUpgradePoint)
        {
            GameManager.Instance.player.playerData.statPoint -= (int)coinUpgradePoint; // 업그레이드 포인트 만큼 스탯 포인트에서 값 감소
            GameManager.Instance.player.playerData.coinGet =
                0.1f + GameManager.Instance.player.playerData.coinGet * 2f; // 스탯 포인트 증가값
            coinUpgradePoint *= 1.5f; // 강화 포인트 사용 시 다음 사용할 때 1.5배 추가 증가

            UIBtnManager.Instance.uiBtnController.RefreshUI(); // 업그레이드 수치 변경 시 최신화
        }
        else
        {
            UIBtnManager.Instance.uiBtnController.ErrorPanel(); // 에러 발생 시 에러 패널 생성
            StartCoroutine(UIBtnManager.Instance.uiBtnController.CloseErrorPanel()); // 코루틴을 이용하여 3초뒤 사라짐
        }

        GameManager.Instance.SaveGame();
    }

    // TODO
    // 마우스 키 다운시 0.2초 간격으로 강화 하는거 만들기
    public void RunUpgrade(System.Action upgradeMethod)
    {
        upgradeAction = upgradeMethod;
        upgradeCoroutine = StartCoroutine(IsHolding());
    }

    public void StopUpgrade()
    {
        StopCoroutine(upgradeCoroutine);
    }

    public IEnumerator IsHolding()
    {
        while (true)
        {
            upgradeAction?.Invoke();
            yield return new WaitForSeconds(1);
        }
    }
    
    public void StartCriDamageUpgrade()
    {
        RunUpgrade(CriDamageUpgrade);
    }

    public void StartAutoNumUpgrade()
    {
        RunUpgrade(AutoNumUpgrade);
    }

    public void StartCoinGetUpgrade()
    {
        RunUpgrade(CoinGetUpgrade);
    }
}
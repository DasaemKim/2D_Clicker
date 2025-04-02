using System;
using System.Collections;
using UnityEngine;

// 골드를 소모하여 플레이어 스탯 업그레이드 로직 구성
public class StatUpgrade : MonoBehaviour
{
    // 버튼의 스탯 포인트 기본값
    public float criUpgradePoint;
    public float autoUpgradePoint;
    public float coinUpgradePoint;

    public AttackSystem attackSystem;

    // 캐릭터 스탯의 업그레이드 Lv 수치를 나타냄
    public int criUpLevel;
    public int autoUpLevel;
    public int coinGetUpLevel;

    private Coroutine upgradeCoroutine;
    private System.Action upgradeAction;

    private void Start()
    {
        criUpgradePoint = GameManager.Instance.player.playerData.criUpgradeCost;
        autoUpgradePoint = GameManager.Instance.player.playerData.autoUpgradeCost;
        coinUpgradePoint = GameManager.Instance.player.playerData.coinUpgradeCost;
        UIBtnManager.Instance.uiBtnController.RefreshUI();
    }

    // 크리티컬 데미지 증가
    public void CriDamageUpgrade()
    {
        // 플레이어의 스탯 포인트가 강화 스탯 포인트 보다 클때
        if (GameManager.Instance.player.playerData.statPoint >= criUpgradePoint)
        {
            GameManager.Instance.player.playerData.statPoint -= Mathf.FloorToInt(criUpgradePoint); // 업그레이드 포인트 만큼 스탯 포인트에서 값 감소
            GameManager.Instance.player.playerData.criDamage += 0.8f; // 스탯 포인트 증가값
            criUpgradePoint *= 1.5f; // 강화 포인트 사용 시 다음 사용할 때 1.5배 추가 증가
            GameManager.Instance.player.playerData.criUpgradeCost = criUpgradePoint;

            // 구매했을때 +1 되도록 설정
            GameManager.Instance.player.playerData.criUpLevel += 1;
            GameManager.Instance.player.playerData.criticalDamageLevel += 1;


            UIBtnManager.Instance.uiBtnController.RefreshUI(); // 업그레이드 수치 변경 시 최신화

            // 텍스트 활성화
            UIBtnManager.Instance.uiBtnController.isInfoVisible = true;
            UIBtnManager.Instance.uiBtnController.criInfoText.gameObject.SetActive(UIBtnManager.Instance.uiBtnController.isInfoVisible);
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
            GameManager.Instance.player.playerData.statPoint -= Mathf.FloorToInt(autoUpgradePoint); // 업그레이드 포인트 만큼 스탯 포인트에서 값 감소
            GameManager.Instance.player.playerData.autoNum *= 0.9f; // 0.9배씩 감소
            autoUpgradePoint *= 1.5f; // 강화 포인트 사용 시 다음 사용할 때 1.5배 추가 증가
            GameManager.Instance.player.playerData.autoUpgradeCost = autoUpgradePoint;

            GameManager.Instance.player.playerData.autoUpLevel += 1;
            GameManager.Instance.player.playerData.autoAttackLevel += 1;
            // 구매했을때 +1 되도록 설정

            UIBtnManager.Instance.uiBtnController.RefreshUI(); // 업그레이드 수치 변경 시 최신화
            attackSystem.StartAutoAttack();

            // 텍스트 활성화
            UIBtnManager.Instance.uiBtnController.isInfoVisible = true;
            UIBtnManager.Instance.uiBtnController.autoInfoText.gameObject.SetActive(UIBtnManager.Instance.uiBtnController.isInfoVisible);
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
            GameManager.Instance.player.playerData.statPoint -= Mathf.FloorToInt(coinUpgradePoint); // 업그레이드 포인트 만큼 스탯 포인트에서 값 감소
            GameManager.Instance.player.playerData.coinGet += 1.5f; // 스탯 포인트 증가값
            coinUpgradePoint *= 1.5f; // 강화 포인트 사용 시 다음 사용할 때 1.5배 추가 증가
            GameManager.Instance.player.playerData.coinUpgradeCost = coinUpgradePoint;

            GameManager.Instance.player.playerData.coinGetUpLevel += 1;
            // 구매했을때 +1 되도록 설정
            GameManager.Instance.player.playerData.goldBonusLevel += 1;

            UIBtnManager.Instance.uiBtnController.RefreshUI(); // 업그레이드 수치 변경 시 최신화

            // 텍스트 활성화
            UIBtnManager.Instance.uiBtnController.isInfoVisible = true;
            UIBtnManager.Instance.uiBtnController.coinInfoText.gameObject.SetActive(UIBtnManager.Instance.uiBtnController.isInfoVisible);
        }
        else
        {
            UIBtnManager.Instance.uiBtnController.ErrorPanel(); // 에러 발생 시 에러 패널 생성
            StartCoroutine(UIBtnManager.Instance.uiBtnController.CloseErrorPanel()); // 코루틴을 이용하여 3초뒤 사라짐
        }

        GameManager.Instance.SaveGame();
    }

    // 키 다운 시
    public void RunUpgrade(System.Action upgradeMethod)
    {
        if (upgradeCoroutine != null)
            return;

        upgradeAction = upgradeMethod;
        upgradeAction.Invoke();
        upgradeCoroutine = StartCoroutine(IsHolding());
    }

    // 키 다운 종료 시
    public void StopUpgrade()
    {
        if (upgradeCoroutine != null)
        {
            StopCoroutine(upgradeCoroutine);
            upgradeCoroutine = null;
        }
    }

    public IEnumerator IsHolding()
    {
        yield return new WaitForSeconds(1);

        while (true)
        {
            upgradeAction?.Invoke();
            yield return new WaitForSeconds(0.2f);
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
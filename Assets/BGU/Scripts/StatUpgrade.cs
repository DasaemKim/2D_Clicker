using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 골드를 소모하여 플레이어 스탯 업그레이드 로직 구성
public class StatUpgrade : MonoBehaviour
{
    // CharacterData의 주소값 없음
    CharacterData characterData;

    public float criUpgradePoint;
    public float autoUpgradePoint;
    public float coinUpgradePoint;

    void Start()
    {
        // 힙 메모리 영역의 주소값을 가져옴
        characterData = GetComponent<Player>().characterData;
    }
    
    //TODO
    // 현재 강화 시 값은 올라가나 프로그램 종료 후 다시 키면 초기 값으로 돌아감.
    // 내일 UI텍스트를 추가하여 RefreshUI 메서드를 통하여 나가더라도 값이 고정되도록 변경
    
    // TODO
    // 마우스 키 다운시 0.2초 간격으로 강화 하는거 만들기
    // 아마도 input.MounseButtonDown을 이용하여 작성

    // 크리티컬 데미지 증가
    public void CriDamageUpgrade()
    {
        if (characterData.statPoint >= criUpgradePoint)
        {
            characterData.statPoint -= (int)criUpgradePoint;
            characterData.criDamage = 0.1f + characterData.criDamage * 1.5f;
            criUpgradePoint *= 1.5f;

            UIBtnManager.Instance.uiBtnController.RefreshUI();
        }
        else
        {
            UIBtnManager.Instance.uiBtnController.ErrorPanel();
            StartCoroutine(UIBtnManager.Instance.uiBtnController.CloseErrorPanel()); // 코루틴을 이용하여 3초뒤 사라짐
        }
    }

    // 자동 공격 횟수 증가
    public void AutoNumUpgrade()
    {
        if (characterData.statPoint >= autoUpgradePoint)
        {
            characterData.statPoint -= (int)autoUpgradePoint;
            characterData.autoNum = 1 + characterData.autoNum + 0.3f;
            autoUpgradePoint *= 1.5f;

            UIBtnManager.Instance.uiBtnController.RefreshUI();
        }
        else
        {
            UIBtnManager.Instance.uiBtnController.ErrorPanel();
            StartCoroutine(UIBtnManager.Instance.uiBtnController.CloseErrorPanel()); // 코루틴을 이용하여 3초뒤 사라짐
        }
    }

    // 코인 획득량 증가
    public void CoinGetUpgrade()
    {
        if (characterData.statPoint >= coinUpgradePoint)
        {
            characterData.statPoint -= (int)coinUpgradePoint;
            characterData.coinGet = 0.1f + characterData.coinGet * 2f;
            coinUpgradePoint *= 1.5f;

            UIBtnManager.Instance.uiBtnController.RefreshUI();
        }
        else
        {
            UIBtnManager.Instance.uiBtnController.ErrorPanel();
            StartCoroutine(UIBtnManager.Instance.uiBtnController.CloseErrorPanel()); // 코루틴을 이용하여 3초뒤 사라짐
        }
    }
}
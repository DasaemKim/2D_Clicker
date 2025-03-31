using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 골드를 소모하여 플레이어 스탯 업그레이드 로직 구성
public class StatUpgrade : MonoBehaviour
{
    // CharacterData의 주소값 없음
    CharacterData characterData;

    // 버튼의 스탯 포인트 기본값
    public float criUpgradePoint;
    public float autoUpgradePoint;
    public float coinUpgradePoint;

    //TODO 진행 중
    // 현재 강화 시 값은 올라가나 프로그램 종료 후 다시 키면 초기 값으로 돌아감.
    // 내일 UI텍스트를 추가하여 RefreshUI 메서드를 통하여 나가더라도 값이 고정되도록 변경
    // PlayerData.cs에 데이더 저장 진행
    
    void Start()
    {
        // 힙 메모리 영역의 주소값을 가져옴
        characterData = GetComponent<Player>().characterData;
    }

    // 크리티컬 데미지 증가
    public void CriDamageUpgrade()
    {
        // 플레이어의 스탯 포인트가 강화 스탯 포인트 보다 클때
        if (characterData.statPoint >= criUpgradePoint)
        {
            characterData.statPoint -= (int)criUpgradePoint;    // 업그레이드 포인트 만큼 스탯 포인트에서 값 감소
            characterData.criDamage = 0.1f + characterData.criDamage * 1.5f;  // 스탯 포인트 증가값
            criUpgradePoint *= 1.5f;   // 강화 포인트 사용 시 다음 사용할 때 1.5배 추가 증가

            UIBtnManager.Instance.uiBtnController.RefreshUI(); // 업그레이드 수치 변경 시 최신화
        }
        else
        {
            UIBtnManager.Instance.uiBtnController.ErrorPanel(); // 에러 발생 시 에러 패널 생성
            StartCoroutine(UIBtnManager.Instance.uiBtnController.CloseErrorPanel()); // 코루틴을 이용하여 3초뒤 사라짐
        }
    }

    // 자동 공격 횟수 증가
    public void AutoNumUpgrade()
    {
        // 플레이어의 스탯 포인트가 강화 스탯 포인트 보다 클때
        if (characterData.statPoint >= autoUpgradePoint)
        {
            characterData.statPoint -= (int)autoUpgradePoint;    // 업그레이드 포인트 만큼 스탯 포인트에서 값 감소
            characterData.autoNum = 1 + characterData.autoNum + 0.3f;  // 스탯 포인트 증가값
            autoUpgradePoint *= 1.5f;   // 강화 포인트 사용 시 다음 사용할 때 1.5배 추가 증가

            UIBtnManager.Instance.uiBtnController.RefreshUI(); // 업그레이드 수치 변경 시 최신화
        }
        else
        {
            UIBtnManager.Instance.uiBtnController.ErrorPanel(); // 에러 발생 시 에러 패널 생성
            StartCoroutine(UIBtnManager.Instance.uiBtnController.CloseErrorPanel()); // 코루틴을 이용하여 3초뒤 사라짐
        }
    }

    // 코인 획득량 증가
    public void CoinGetUpgrade()
    {
        // 플레이어의 스탯 포인트가 강화 스탯 포인트 보다 클때
        if (characterData.statPoint >= coinUpgradePoint)
        {
            characterData.statPoint -= (int)coinUpgradePoint;    // 업그레이드 포인트 만큼 스탯 포인트에서 값 감소
            characterData.coinGet = 0.1f + characterData.coinGet * 2f;  // 스탯 포인트 증가값
            coinUpgradePoint *= 1.5f;   // 강화 포인트 사용 시 다음 사용할 때 1.5배 추가 증가

            UIBtnManager.Instance.uiBtnController.RefreshUI(); // 업그레이드 수치 변경 시 최신화
        }
        else
        {
            UIBtnManager.Instance.uiBtnController.ErrorPanel(); // 에러 발생 시 에러 패널 생성
            StartCoroutine(UIBtnManager.Instance.uiBtnController.CloseErrorPanel()); // 코루틴을 이용하여 3초뒤 사라짐
        }
    }

    // TODO
    // 마우스 키 다운시 0.2초 간격으로 강화 하는거 만들기
    // 아마도 input.MounseButtonDown을 이용하여 작성
    public void RunUpgrade()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 골드를 소모하여 플레이어 스탯 업그레이드 로직 구성
public class StatUpgrade : MonoBehaviour
{
    // CharacterData의 주소값 없음
    CharacterData characterData;

    int statUpgrade = 10;
    
    void Start()
    {
        // 힙 메모리 영역의 주소값을 가져옴
        characterData = GetComponent<Player>().characterData;
    }
    
    // 크리티컬 확률 증가
    public void CriDamageUpgrade()
    {
        if (characterData.statPoint > statUpgrade)
        {
            characterData.statPoint -= statUpgrade;
            characterData.criDamage = 0.1f + characterData.criDamage * 1.5f;
            statUpgrade *= 2;
            Debug.Log(characterData.criDamage);
        }
        else
        {
            Debug.Log("골드가 부족합니다.");
        }
    }

    // 자동 공격 횟수 증가
    public void AutoNumUpgrade()
    {
        if (characterData.statPoint > statUpgrade)
        {
            characterData.statPoint -= statUpgrade;
            characterData.autoNum = 1 + characterData.autoNum + 0.3f;
            statUpgrade *= 2;
            Debug.Log(characterData.autoNum);
        }
        else
        {
            Debug.Log("골드가 부족합니다.");
        }
    }

    // 코인 획득량 증가
    public void CoinGetUpgrade()
    {    
        if (characterData.statPoint > statUpgrade)
        {   
            characterData.statPoint -= statUpgrade;
            characterData.coinGet = 0.1f + characterData.coinGet * 2f;  
            statUpgrade *= 2;
            Debug.Log(characterData.coinGet);
        }
        else
        {
            Debug.Log("골드가 부족합니다.");
        }
    }
}
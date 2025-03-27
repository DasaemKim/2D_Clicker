using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 골드를 소모하여 플레이어 스탯 업그레이드 로직 구성
public class StatUpgrade : MonoBehaviour
{
    // CharacterData의 주소값이 없음
    CharacterData characterData;

    int coinUpgrade = 10;
    
    void Start()
    {
        // player = GetComponent<Player>();
        // characterData = player.characterData;
        
        // 힙 메모리 영역의 주소값을 가져옴
        characterData = GetComponent<Player>().characterData;
    }
    
    // 크리티컬 확률 증가
    public void CriDamageUpgrade()
    {
        if (characterData.coinPoint > coinUpgrade)
        {
            characterData.coinPoint -= coinUpgrade;
            characterData.criDamage = 1 + characterData.criDamage * 1.5f;
            coinUpgrade *= 2;
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
        if (characterData.coinPoint > coinUpgrade)
        {
            characterData.coinPoint -= coinUpgrade;
            characterData.autoNum = 1 + characterData.autoNum + 0.3f;
            coinUpgrade *= 2;
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
        if (characterData.coinPoint > coinUpgrade)
        {   
            characterData.coinPoint -= coinUpgrade;
            characterData.coinGet = 1 + characterData.coinGet * 1.1f;  
            coinUpgrade *= 2;
            Debug.Log(characterData.coinGet);
        }
        else
        {
            Debug.Log("골드가 부족합니다.");
        }
    }
}
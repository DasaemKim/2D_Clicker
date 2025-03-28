using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Player : MonoBehaviour
{
    // 캐릭터 데이터 힙 메모리 주소값을 캐싱함
    public CharacterData characterData;

    private Random random = new Random();
    
    // 플레이어 공격
    public void PlayerAtk()
    {
        // 15% 확률로 치명타
        if (CriProb(15))
        {
            characterData.damage = (int)(characterData.damage * 1.5);
        }
    }
    
    // 크리티컬 계산
    private bool CriProb(int prob)
    {
        int isOccur = random.Next(1, 101);
        return isOccur <= prob;
    }
}

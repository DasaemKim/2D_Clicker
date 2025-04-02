using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 힙 영역에 메모리 할당
[CreateAssetMenu(menuName = "New Data/ CharacterDataData")]
public class CharacterData : ScriptableObject
{
    // 플레이어 데이터 관리
    [Header("Character Stats")]
    public float damage;    // 플레이어 데미지
    public float criDamage; // 플레이어 크리데미지
    public int statPoint;   // 플레이어 보유 스탯
    public int weaponPoint; // 플레이어 무기 경험치
    public float autoNum;   // 자동 공격 시간
    public float coinGet;   // 골드 획득량               // 스테이지 코드랑 연관 되어 있음
}

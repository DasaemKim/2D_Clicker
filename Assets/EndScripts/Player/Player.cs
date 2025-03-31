using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 캐릭터 데이터 힙 메모리 주소값을 캐싱함
    public CharacterData characterData;
    public Weapon equipweapon;

    public PlayerData playerData;
    public Stat stat;

    public AttackSystem attackSystem;

    public bool CheckCriticalHit()
    {
        return Random.Range(0f, 1f) < stat.FinalCriticaRate(playerData, equipweapon); // 랜덤값이 criticalChance보다 작으면 치명타 발생

    }

    public float GetDamage(bool isCri)
    {
        if(isCri)
        {
            return stat.FinalCriticalDamage(playerData);  //  치명타 데미지 계산
        }
        else
        {
            return stat.FinalAttack(playerData, equipweapon); // 일반 데미지 계산
        }
    }

    public void AutoAttackLevel()
    {
        playerData.autoAttackLevel++;

        // 자동 공격 주기 설정 (기본 0.3회/초로 설정)
        characterData.autoNum = 0.3f; // 0.3회/초 (즉, 3.33초당 한 번의 공격)

        // 자동 공격 주기 출력
        Debug.Log($"자동 공격 레벨: {playerData.autoAttackLevel}, 자동 공격 시간: {characterData.autoNum}");

        // 자동 공격을 시작
        if (!attackSystem.isAutoAttacking)
        {
            attackSystem.StartAutoAttack();
        }
    }


    public void Init() 
    {
        playerData = new PlayerData()
        {
            stage = 1,
            gold = 0,
            criticalDamageLevel = 0,
            autoAttackLevel = 0,
            goldBonusLevel = 0,
            equippedWeaponLevel = 0,
            selectedCharacter = "default",
            equippedWeaponName = "나무검"
        };
       GameManager.Instance.SaveGame();
    }
    
    
}

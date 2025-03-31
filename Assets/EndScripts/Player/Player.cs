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

    public void Start()
    {
        stat = new Stat();
        stat.baseAttack = GameManager.Instance.player.characterData.damage;
    }

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

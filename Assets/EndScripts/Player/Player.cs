using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 캐릭터 데이터 힙 메모리 주소값을 캐싱함
    public CharacterData characterData;
    public WeaponData equipweapon;

    public PlayerData playerData; 

    public void Start()
    {
        
    }

    public bool CheckCriticalHit()
    {
        return Random.Range(0f, 1f) < FinalCriticaRate(); // 랜덤값이 criticalChance보다 작으면 치명타 발생

    }

    public float GetDamage(bool isCri)
    {
        if(isCri)
        {
            return FinalCriticalDamage();  //  치명타 데미지 계산
        }
        else
        {
            return FinalAttack(); // 일반 데미지 계산
        }
    }

    public void Init() 
    {
        playerData = new PlayerData(characterData);

        GameManager.Instance.SaveGame();
    }

    public float FinalAttack()
    {
        if (playerData.equippedWeapon == null) return playerData.damage;
        return playerData.damage + playerData.equippedWeapon.Attack;
    }
    public float FinalCriticaRate()
    {
        if (playerData.equippedWeapon == null) return playerData.criticalRate;
        return playerData.criticalRate + playerData.equippedWeapon.Critical;
    }
    public float FinalCriticalDamage()
    {
        if (playerData.equippedWeapon == null) return playerData.damage;
        return (playerData.damage * (1.5f + (playerData.criticalDamageLevel * 0.1f)));
    }
    public int FinalGoldBonus()
    {
        return playerData.baseGoldBonus + (100 * playerData.goldBonusLevel);
    }
}

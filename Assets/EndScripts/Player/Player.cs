using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 캐릭터 데이터 참조가능
    public CharacterData characterData;

    //현재 장착중인 무기
    public Weapon equipweapon;

    //플레이어 저장 데이터
    public PlayerData playerData;

    //치명타 여부 확인
    public bool CheckCriticalHit()
    {
        return Random.Range(0f, 1f) < FinalCriticaRate(); // 랜덤값이 criticalChance보다 작으면 치명타 발생

    }

    //최종 데미지 계산(치명타 여부에 따라)
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

    //플레이어 데이터 초기화
    public void Init() 
    {
        playerData = new PlayerData(characterData);

        GameManager.Instance.SaveGame();
    }

    //최종 공격력 계산
    public float FinalAttack()
    {
        if (playerData.equippedWeapon == null) return playerData.damage;
        return playerData.damage + playerData.equippedWeapon.Attack;
    }

    //최종 치명타확률 계산
    public float FinalCriticaRate()
    {
        if (playerData.equippedWeapon == null) return playerData.criticalRate;
        return playerData.criticalRate + playerData.equippedWeapon.Critical;
    }

    //최종 치명타 데미지 계산
    public float FinalCriticalDamage()
    {
        return (FinalAttack() * (1.5f + (playerData.criUpLevel * 0.1f)));
    }

    //최종 골드 획득 보너스 계산
    public int FinalGoldBonus()
    {
        return playerData.baseGoldBonus + (100 * playerData.coinGetUpLevel);
    }
}

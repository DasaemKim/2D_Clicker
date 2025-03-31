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

    public AttackSystem attackSystem;

    public void Start()
    {

    }

    public bool CheckCriticalHit()
    {
        return Random.Range(0f, 1f) < playerData.FinalCriticaRate(); // 랜덤값이 criticalChance보다 작으면 치명타 발생

    }

    public float GetDamage(bool isCri)
    {
        if(isCri)
        {
            return playerData.FinalCriticalDamage();  //  치명타 데미지 계산
        }
        else
        {
            return playerData.FinalAttack(); // 일반 데미지 계산
        }
    }

    public void Init() 
    {
        playerData = new PlayerData(characterData);

        GameManager.Instance.SaveGame();
    }
    
    
}

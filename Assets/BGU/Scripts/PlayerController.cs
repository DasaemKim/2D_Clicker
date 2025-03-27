using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
    // 플레이어 공격 및 자동 공격 로직 작성
    public CharacterData characterData;

    public GameObject enemy;

    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        
        Debug.Log(characterData.damage);
        Debug.Log(characterData.criDamage);
        Debug.Log(characterData.coinPoint);
        Debug.Log(characterData.levelPoint);
        Debug.Log(characterData.autoAtk);
        Debug.Log(characterData.coinGet);
    }

    void Update()
    {
    }

    // 클릭 시 캐릭터 공격
    public void ClickAtk()
    {
        if (Input.GetMouseButtonDown(0))
        {
        }
    }

    // 캐릭터 자동 공격
    public void AutoAtk()
    {
    }
}
    using System;
    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    // 캐릭터 데이터
    public CharacterData characterData;

    private void Start()
    {
        Debug.Log(characterData.damage);
        Debug.Log(characterData.criDamage);
        Debug.Log(characterData.coinPoint);
        Debug.Log(characterData.levelPoint);
        Debug.Log(characterData.autoAtk);
        Debug.Log(characterData.coinGet);
    }
}

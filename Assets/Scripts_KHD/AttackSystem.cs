using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AttackSystem : MonoBehaviour
{

    public void Attack()
    {
        Debug.Log("공격 실행!");

    }

    public void AutoAttack()
    {
        Debug.Log("자동공격 실행!");
    }

    public void Critical()
    {
        Debug.Log("크리티컬 공격 실행!");
    }

    public void AttackEffect()
    {
        Debug.Log("공격 이펙트 실행!");
    }
}
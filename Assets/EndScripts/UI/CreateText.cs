using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateText : MonoBehaviour
{
    public int DamageIndex;

    private void Awake()
    {
        StageUI.Instance.CreateText = this;
    }

    public void CreateTextDamage(float damage, bool isCri) // 데미지 텍스트 생성
    {
        PoolManager.Instance.GetObject(transform.position, Quaternion.identity, DamageIndex);

        StageUI.Instance.DamageTextUI.DownTextDamage(damage, isCri); // 텍스트 생성될때 효과
    }
}
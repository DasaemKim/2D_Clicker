using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateText : MonoBehaviour
{
    public int DamageIndex;
    //public DamageTextUI damageTextUI;

    private void Start()
    {
        StageUI.Instance.CreateText = this;
    }

    public void CreateTextDamage(float damage)
    {
        PoolManager.Instance.GetObject2(transform.position, Quaternion.identity, DamageIndex);

        StageUI.Instance.DamageTextUI.DownTextDamage(damage);
    }
}
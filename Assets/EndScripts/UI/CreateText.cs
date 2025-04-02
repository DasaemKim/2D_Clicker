using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateText : MonoBehaviour
{
    public int DamageIndex;
    //public DamageTextUI damageTextUI;

    private void Awake()
    {
        StageUI.Instance.CreateText = this;
    }

    public void CreateTextDamage(float damage, bool isCri)
    {
        PoolManager.Instance.GetObject(transform.position, Quaternion.identity, DamageIndex);

        StageUI.Instance.DamageTextUI.DownTextDamage(damage, isCri);
    }
}
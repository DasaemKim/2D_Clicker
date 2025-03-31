using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject : MonoBehaviour
{
    public EnemyData EnemyData;

    public void GetEnemyData() // 积己等 利 单捞磐 颗扁扁
    {
        EnemyManager.Instance.EnemyData = EnemyData;
    }
}

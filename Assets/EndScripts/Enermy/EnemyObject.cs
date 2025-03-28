using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject : MonoBehaviour
{
    public EnemyData EnemyData;

    public void GetEnemyData()
    {
        EnemyManager.Instance.EnemyData = EnemyData;
    }
}

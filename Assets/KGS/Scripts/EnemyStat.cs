using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyStat
{
    private float currentHealth;
    private float maxHealth;

    public event Action UpdateEnemyHealth;

    public EnemyStat (EnemyData enemyData)
    {
        maxHealth = enemyData.Health;
        currentHealth = maxHealth;
    }

    public float CurrentHealth
    {
        get => currentHealth;
        set
        {
            if (currentHealth != value)
            {
                currentHealth = value;
                UpdateEnemyHealth?.Invoke();
            }
        }
    }

    public float MaxHealth
    {
        get => maxHealth;
        set
        {
            if (maxHealth != value)
            {
                maxHealth = value;
                UpdateEnemyHealth?.Invoke();
            }
        }
    }
}

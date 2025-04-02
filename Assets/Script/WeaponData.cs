using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "New Weapon")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public int level;
    public int attack;
    public float criticalrate;

    public Sprite iconSpr;
}

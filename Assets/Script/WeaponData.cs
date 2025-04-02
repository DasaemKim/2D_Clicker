using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "New Weapon")]
public class WeaponData : ScriptableObject
{
    [Header("Stats")]
    public string WeaponName;
    public int Attack;
    public float Critical_Rate; 
    public int UpgradeLevel; 
    public int UpgradeAttack; 
    public float UpgradeCritical;
    public Sprite WeaponObject;

    public bool IsObatained;
}

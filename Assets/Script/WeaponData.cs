using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "New Weapon")]
public class WeaponData : ScriptableObject
{
    public string WeaponName; // ���� ���� ����
    public int Attack; // ���� �̸�
    public float Critical_Rate; // ���� ���ݷ�
    public int UpgradeLevel; //������ ���׷��̵� ����
    public int UpgradeAttack; // ���� ���׷��̵� ���ݷ�
    public float UpgradeCritical; // ���� ���׷��̵� ũ��Ƽ��
    public Sprite WeaponObject;

    public bool IsObatained; // ��ȹ���� ����
}

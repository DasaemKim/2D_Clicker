using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "New Weapon")]
public class WeaponData : ScriptableObject
{
    public string WeaponName; // ���� ���� ����
    public int Attack; // ���� �̸�
    public float Critical; // ���� ���ݷ�
    public int UpgradeLevel; //������ ���׷��̵� ����
    public List<int> UpgradeAttack; // ���� ���׷��̵� ���ݷ�
    public List<float> UpgradeCritical; // ���� ���׷��̵� ũ��Ƽ��

    public bool IsObatained = false; // ��ȹ���� ����
}

using System.Collections;
using System.Collections.Generic;
using System.Data;
using JetBrains.Annotations;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory inst;

    public WeaponData data; // 인벤토리에서 선택된 데이터
    public WeaponSlot selectSlot; // 장착된 장비

    private void Awake()
    {
        inst = this;
    }

    public void Select(WeaponData data)
    {
        this.data = data;
        selectSlot.data = data;

        selectSlot.UpdateUI();
    }
}

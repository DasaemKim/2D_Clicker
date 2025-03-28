using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IPoolable
{
    void Initialize(Action<GameObject> action); // ReturnObject�Լ��� �ʱ�ȭ
    void OnSpawn();
    void OnDespawn();
}

public class PoolManager : MonoBehaviour
{
    public GameObject[] Prefabs;  // Enemy ������
    private Dictionary<int, Queue<GameObject>> pools = new Dictionary<int, Queue<GameObject>>(); // Ǯ ����Ʈ

    public static PoolManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < Prefabs.Length; i++)
        {
            pools[i] = new Queue<GameObject>();
        }
    }

    public GameObject GetObject(int prefabIndex, Vector3 position, Quaternion rotation) // ������Ʈ ���� �Ǵ� Ȱ��ȭ
    {
        if (!pools.ContainsKey(prefabIndex))
        {
            Debug.LogError($"������ �ε��� {prefabIndex}�� ���� Ǯ�� �������� �ʽ��ϴ�.");
            return null;
        }

        GameObject obj;
        if (pools[prefabIndex].Count > 0)
        {
            obj = pools[prefabIndex].Dequeue();  // Ǯ���� ������Ʈ ������
        }
        else
        {
            obj = Instantiate(Prefabs[prefabIndex]);
            obj.GetComponent<IPoolable>()?.Initialize(o => ReturnObject(prefabIndex, o));
        }

        obj.transform.SetPositionAndRotation(position, rotation);
        obj.SetActive(true);
        obj.GetComponent<IPoolable>()?.OnSpawn();
        return obj;
    }

    public void ReturnObject(int prefabIndex, GameObject obj)  // ������Ʈ ��Ȱ��ȭ
    {
        if (!pools.ContainsKey(prefabIndex))  // Ǯ���� ������Ʈ ����
        {
            Destroy(obj);
            return;
        }

        obj.SetActive(false);
        pools[prefabIndex].Enqueue(obj); // ������Ʈ Ǯ�� �ű��
    }
}

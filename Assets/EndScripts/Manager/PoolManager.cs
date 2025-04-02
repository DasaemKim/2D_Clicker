using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    [SerializeField] private GameObject TakeDamageText; // Canvas�� �ڽ� ������Ʈ

    public static PoolManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < Prefabs.Length; i++)
        {
            pools[i] = new Queue<GameObject>();
        }
    }

    public GameObject GetObject(Vector3 position, Quaternion rotation, int prefabIndex) // �� ������Ʈ ���� �Ǵ� Ȱ��ȭ
    {
        if (!pools.ContainsKey(prefabIndex))
        {
            Debug.LogError($"프리팹 인덱스 {prefabIndex}에 대한 풀이 존재하지 않습니다.");
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

        if (obj.name.Contains(Prefabs[Prefabs.Length - 1].name))
        {
            obj.transform.SetParent(TakeDamageText.transform);
        }
        else
        {
            obj.transform.SetParent(transform);
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

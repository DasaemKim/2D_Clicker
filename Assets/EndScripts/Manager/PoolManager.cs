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
    public GameObject[] Prefabs2; // Text ������
    private Dictionary<int, Queue<GameObject>> pools = new Dictionary<int, Queue<GameObject>>(); // Ǯ ����Ʈ
    private Dictionary<int, Queue<GameObject>> pools2 = new Dictionary<int, Queue<GameObject>>(); // Ǯ ����Ʈ

    [SerializeField] private GameObject TakeDamageText; // Canvas�� �ڽ� ������Ʈ

    public static PoolManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < Prefabs.Length; i++)
        {
            pools[i] = new Queue<GameObject>();
        }

        for (int i = 0; i < Prefabs2.Length; i++)
        {
            pools2[i] = new Queue<GameObject>();
        }
    }

    public GameObject GetObject(Vector3 position, Quaternion rotation, int prefabIndex = 10) // �� ������Ʈ ���� �Ǵ� Ȱ��ȭ
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
            obj.GetComponent<IPoolable>()?.Initialize(o => ReturnObject(prefabIndex, o, "Enemy"));
        }

        obj.transform.SetParent(transform);

        obj.transform.SetPositionAndRotation(position, rotation);
        obj.SetActive(true);
        obj.GetComponent<IPoolable>()?.OnSpawn();
        return obj;
    }

    public GameObject GetObject2(Vector3 position, Quaternion rotation, int prefabIndex) // �ؽ�Ʈ ������Ʈ ���� �Ǵ� Ȱ��ȭ
    {
        if (!pools2.ContainsKey(prefabIndex))
        {
            Debug.LogError($"������ �ε��� {prefabIndex}�� ���� Ǯ�� �������� �ʽ��ϴ�.");
            return null;
        }

        GameObject obj;
        if (pools2[prefabIndex].Count > 0)
        {
            obj = pools2[prefabIndex].Dequeue();  // Ǯ���� ������Ʈ ������
        }
        else
        {
            obj = Instantiate(Prefabs2[prefabIndex]);
            obj.GetComponent<IPoolable>()?.Initialize(o => ReturnObject(prefabIndex, o, "Text"));
        }

        obj.transform.SetParent(TakeDamageText.transform);

        obj.transform.SetPositionAndRotation(position, rotation);
        obj.SetActive(true);
        obj.GetComponent<IPoolable>()?.OnSpawn();
        return obj;
    }

    public void ReturnObject(int prefabIndex, GameObject obj, string obType)  // ������Ʈ ��Ȱ��ȭ
    {
        if (obType == "Enemy")
        {
            if (!pools.ContainsKey(prefabIndex))  // Ǯ���� ������Ʈ ����
            {
                Destroy(obj);
                return;
            }

            obj.SetActive(false);
            pools[prefabIndex].Enqueue(obj); // ������Ʈ Ǯ�� �ű��
        }

        else if (obType == "Text")
        {
            if (!pools2.ContainsKey(prefabIndex))  // Ǯ���� ������Ʈ ����
            {
                Destroy(obj);
                return;
            }

            obj.SetActive(false);
            pools2[prefabIndex].Enqueue(obj); // ������Ʈ Ǯ�� �ű��
        }
    }
}

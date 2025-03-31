using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IPoolable
{
    void Initialize(Action<GameObject> action); // ReturnObject함수로 초기화
    void OnSpawn();
    void OnDespawn();
}

public class PoolManager : MonoBehaviour
{
    public GameObject[] Prefabs;  // Enemy 프리팹
    public GameObject[] Prefabs2; // Text 프리팹
    private Dictionary<int, Queue<GameObject>> pools = new Dictionary<int, Queue<GameObject>>(); // 풀 리스트
    private Dictionary<int, Queue<GameObject>> pools2 = new Dictionary<int, Queue<GameObject>>(); // 풀 리스트

    [SerializeField] private GameObject TakeDamageText; // Canvas의 자식 오브젝트

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

    public GameObject GetObject(Vector3 position, Quaternion rotation, int prefabIndex = 10) // 적 오브젝트 생성 또는 활성화
    {
        if (!pools.ContainsKey(prefabIndex))
        {
            Debug.LogError($"프리팹 인덱스 {prefabIndex}에 대한 풀이 존재하지 않습니다.");
            return null;
        }

        GameObject obj;
        if (pools[prefabIndex].Count > 0)
        {
            obj = pools[prefabIndex].Dequeue();  // 풀에서 오브젝트 빼오기
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

    public GameObject GetObject2(Vector3 position, Quaternion rotation, int prefabIndex) // 텍스트 오브젝트 생성 또는 활성화
    {
        if (!pools2.ContainsKey(prefabIndex))
        {
            Debug.LogError($"프리팹 인덱스 {prefabIndex}에 대한 풀이 존재하지 않습니다.");
            return null;
        }

        GameObject obj;
        if (pools2[prefabIndex].Count > 0)
        {
            obj = pools2[prefabIndex].Dequeue();  // 풀에서 오브젝트 빼오기
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

    public void ReturnObject(int prefabIndex, GameObject obj, string obType)  // 오브젝트 비활성화
    {
        if (obType == "Enemy")
        {
            if (!pools.ContainsKey(prefabIndex))  // 풀에서 오브젝트 제거
            {
                Destroy(obj);
                return;
            }

            obj.SetActive(false);
            pools[prefabIndex].Enqueue(obj); // 오브젝트 풀로 옮기기
        }

        else if (obType == "Text")
        {
            if (!pools2.ContainsKey(prefabIndex))  // 풀에서 오브젝트 제거
            {
                Destroy(obj);
                return;
            }

            obj.SetActive(false);
            pools2[prefabIndex].Enqueue(obj); // 오브젝트 풀로 옮기기
        }
    }
}

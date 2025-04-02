using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public interface IPoolable
{
    void Initialize(Action<GameObject> action); // ReturnObject메서드를 받아옴
    void OnSpawn();
    void OnDespawn();
}

public class PoolManager : MonoBehaviour
{
    public GameObject[] Prefabs;  // Enemy, 데미지 텍스트 프리팹
    
    private Dictionary<int, Queue<GameObject>> pools = new Dictionary<int, Queue<GameObject>>();

    [SerializeField] private GameObject TakeDamageText; // Canvas 자식 오브젝트

    public static PoolManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < Prefabs.Length; i++)
        {
            pools[i] = new Queue<GameObject>();
        }
    }

    public GameObject GetObject(Vector3 position, Quaternion rotation, int prefabIndex) // 오브젝트 생성
    {
        if (!pools.ContainsKey(prefabIndex))
        {
            Debug.LogError($"프리팹 인덱스 {prefabIndex}에 대한 풀이 존재하지 않습니다.");
            return null;
        }

        GameObject obj;
        if (pools[prefabIndex].Count > 0)
        {
            obj = pools[prefabIndex].Dequeue();  // 풀에서 꺼내옴
        }
        else
        {
            obj = Instantiate(Prefabs[prefabIndex]); // 오브젝트 생성

            obj.GetComponent<IPoolable>()?.Initialize(o => ReturnObject(prefabIndex, o)); // ReturnObject메서드를 받아옴
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
        obj.GetComponent<IPoolable>()?.OnSpawn(); // obj 스폰 시 실행되는 메서드
        return obj;
    }

    

    public void ReturnObject(int prefabIndex, GameObject obj)  // 오브젝트 비활성화 이후 풀에 담음
    {
        if (!pools.ContainsKey(prefabIndex))
        {
            Destroy(obj);
            return;
        }

        obj.SetActive(false);
        pools[prefabIndex].Enqueue(obj); // 풀에 오브젝트 담음
    }
}

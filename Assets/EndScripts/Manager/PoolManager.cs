using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IPoolable
{
    void Initialize(Action<GameObject> action); // ReturnObject«‘ºˆ∑Œ √ ±‚»≠
    void OnSpawn();
    void OnDespawn();
}

public class PoolManager : MonoBehaviour
{
    public GameObject[] Prefabs;  // Enemy «¡∏Æ∆’
    public GameObject[] Prefabs2; // Text «¡∏Æ∆’
    private Dictionary<int, Queue<GameObject>> pools = new Dictionary<int, Queue<GameObject>>(); // «Æ ∏ÆΩ∫∆Æ
    private Dictionary<int, Queue<GameObject>> pools2 = new Dictionary<int, Queue<GameObject>>(); // «Æ ∏ÆΩ∫∆Æ

    [SerializeField] private GameObject TakeDamageText; // Canvas¿« ¿⁄Ωƒ ø¿∫Í¡ß∆Æ

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

    public GameObject GetObject(Vector3 position, Quaternion rotation, int prefabIndex) // ¿˚ ø¿∫Í¡ß∆Æ ª˝º∫ ∂«¥¬ »∞º∫»≠
    {
        if (!pools.ContainsKey(prefabIndex))
        {
            Debug.LogError($"ÌîÑÎ¶¨Ìåπ Ïù∏Îç±Ïä§ {prefabIndex}Ïóê ÎåÄÌïú ÌíÄÏù¥ Ï°¥Ïû¨ÌïòÏßÄ ÏïäÏäµÎãàÎã§.");
            return null;
        }

        GameObject obj;
        if (pools[prefabIndex].Count > 0)
        {
            obj = pools[prefabIndex].Dequeue();  // «Æø°º≠ ø¿∫Í¡ß∆Æ ª©ø¿±‚
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

    public GameObject GetObject2(Vector3 position, Quaternion rotation, int prefabIndex) // ≈ÿΩ∫∆Æ ø¿∫Í¡ß∆Æ ª˝º∫ ∂«¥¬ »∞º∫»≠
    {
        if (!pools2.ContainsKey(prefabIndex))
        {
            Debug.LogError($"«¡∏Æ∆’ ¿Œµ¶Ω∫ {prefabIndex}ø° ¥Î«— «Æ¿Ã ¡∏¿Á«œ¡ˆ æ Ω¿¥œ¥Ÿ.");
            return null;
        }

        GameObject obj;
        if (pools2[prefabIndex].Count > 0)
        {
            obj = pools2[prefabIndex].Dequeue();  // «Æø°º≠ ø¿∫Í¡ß∆Æ ª©ø¿±‚
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

    public void ReturnObject(int prefabIndex, GameObject obj, string obType)  // ø¿∫Í¡ß∆Æ ∫Ò»∞º∫»≠
    {
        if (obType == "Enemy")
        {
            if (!pools.ContainsKey(prefabIndex))  // «Æø°º≠ ø¿∫Í¡ß∆Æ ¡¶∞≈
            {
                Destroy(obj);
                return;
            }

            obj.SetActive(false);
            pools[prefabIndex].Enqueue(obj); // ø¿∫Í¡ß∆Æ «Æ∑Œ ø≈±‚±‚
        }

        else if (obType == "Text")
        {
            if (!pools2.ContainsKey(prefabIndex))  // «Æø°º≠ ø¿∫Í¡ß∆Æ ¡¶∞≈
            {
                Destroy(obj);
                return;
            }

            obj.SetActive(false);
            pools2[prefabIndex].Enqueue(obj); // ø¿∫Í¡ß∆Æ «Æ∑Œ ø≈±‚±‚
        }
    }
}

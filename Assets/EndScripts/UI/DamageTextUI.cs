using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageTextUI : MonoBehaviour, IPoolable
{
    public Action<GameObject> returnPoolt; // 오브젝트 비활성화 액션

    public TextMeshProUGUI DamageText;

    private Rigidbody2D textRb;

    private void Start()
    {
        textRb = GetComponent<Rigidbody2D>();

        GameManager.Instance.Enemy.OnDamageText += CreateTextDamage;
    }

    private void OnEnable()
    {
        GameManager.Instance.Enemy.OnDamageText += CreateTextDamage;
    }

    private void OnDisable()
    {
        GameManager.Instance.Enemy.OnDamageText -= CreateTextDamage;
    }

    public void CreateTextDamage(int damage)
    {
        PoolManager.Instance.GetObject(EnemyManager.Instance.transform.position, Quaternion.identity);

        float randomX = (UnityEngine.Random.value < 0.5f) ? -3f : 3f; // 좌우 튕기는 값

        textRb.gravityScale = 1; // 중력값 초기화
        textRb.AddForce(new Vector2(randomX, 7f), ForceMode2D.Impulse); // 적 사망 시 좌우로 튕기기

        DamageText.color = Color.yellow;

        DamageText.text = damage.ToString();
    }

    public void Initialize(Action<GameObject> returnaction) // 풀매니저 ReturnObject함수 가져와서 액션 등록
    {
        returnPoolt = returnaction;
    }

    public void OnSpawn()
    {
        
    }

    public void OnDespawn() // 오브젝트 비활성화
    {
        textRb.gravityScale = 0;
        returnPoolt?.Invoke(gameObject);
    }
}
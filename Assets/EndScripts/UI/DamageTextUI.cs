using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageTextUI : MonoBehaviour, IPoolable
{
    public Action<GameObject> returnPoolt; // ������Ʈ ��Ȱ��ȭ �׼�

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

        float randomX = (UnityEngine.Random.value < 0.5f) ? -3f : 3f; // �¿� ƨ��� ��

        textRb.gravityScale = 1; // �߷°� �ʱ�ȭ
        textRb.AddForce(new Vector2(randomX, 7f), ForceMode2D.Impulse); // �� ��� �� �¿�� ƨ���

        DamageText.color = Color.yellow;

        DamageText.text = damage.ToString();
    }

    public void Initialize(Action<GameObject> returnaction) // Ǯ�Ŵ��� ReturnObject�Լ� �����ͼ� �׼� ���
    {
        returnPoolt = returnaction;
    }

    public void OnSpawn()
    {
        
    }

    public void OnDespawn() // ������Ʈ ��Ȱ��ȭ
    {
        textRb.gravityScale = 0;
        returnPoolt?.Invoke(gameObject);
    }
}
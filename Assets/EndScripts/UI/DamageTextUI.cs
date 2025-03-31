using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class DamageTextUI : MonoBehaviour, IPoolable
{
    public Action<GameObject> returnPoolt; // ������Ʈ ��Ȱ��ȭ �׼�

    public RectTransform bigSunMoonTimer;

    public TextMeshProUGUI DamageText;

    private Rigidbody2D textRb;

    private void Start()
    {
        StageUI.Instance.DamageTextUI = this;
    }

    private void OnEnable()
    {
        StageUI.Instance.DamageTextUI = this;

        // �ؽ�Ʈ�� Ȱ��ȭ�� ������ Rigidbody2D�� �߰�
        textRb = gameObject.GetComponent<Rigidbody2D>();
        if (textRb == null)
        {
            textRb = gameObject.AddComponent<Rigidbody2D>(); // Rigidbody2D �߰�
            textRb.gravityScale = 1; // �߷� �� ����
        }
    }

    public void DownTextDamage(int damage)
    {
        DamageText.color = Color.yellow;
        DamageText.fontSize = 25;
        DamageText.text = damage.ToString();

        float randomX = (UnityEngine.Random.value < 0.5f) ? -200f : 200f; // �¿� ���� �̵�

        float jumpPower = 150f;
        float duration = (jumpPower / 100) * 0.2f; // ���� �ð�

        float fallDuration = 1f; // ���� �ð�
        float endY = -1000f; // �� ��ġ (Y��)

        // �ؽ�Ʈ ƨ�ܿ����� + �������� ����
        transform.DOLocalMoveY(jumpPower, duration)  // ����
            .SetEase(Ease.OutQuad)  // ������ �ε巴�� �ö󰡰�
            .OnComplete(() =>
            {
                // ���� �� �ٷ� ��������
                transform.DOLocalMoveY(-1000f, fallDuration)  // �������� �Ÿ��� �ð�
                    .SetEase(Ease.InQuad)  // ������ �� �ڿ�������
                    .OnKill(() => OnDespawn());  // �������� �����
            });

        // �¿�� ƨ��� �ϱ�
        textRb.AddForce(new Vector2(randomX, 3f), ForceMode2D.Impulse);  // �¿�� ƨ���
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
        DamageText.fontSize = 25;
        returnPoolt?.Invoke(gameObject);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class DamageTextUI : MonoBehaviour, IPoolable
{
    public Action<GameObject> returnPoolt; // 오브젝트 비활성화 액션

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

        // 텍스트가 활성화될 때마다 Rigidbody2D를 추가
        textRb = gameObject.GetComponent<Rigidbody2D>();
        if (textRb == null)
        {
            textRb = gameObject.AddComponent<Rigidbody2D>(); // Rigidbody2D 추가
            textRb.gravityScale = 1; // 중력 값 설정
        }
    }

    public void DownTextDamage(int damage)
    {
        DamageText.color = Color.yellow;
        DamageText.fontSize = 25;
        DamageText.text = damage.ToString();

        float randomX = (UnityEngine.Random.value < 0.5f) ? -200f : 200f; // 좌우 랜덤 이동

        float jumpPower = 150f;
        float duration = (jumpPower / 100) * 0.2f; // 점프 시간

        float fallDuration = 1f; // 낙하 시간
        float endY = -1000f; // 끝 위치 (Y값)

        // 텍스트 튕겨오르기 + 떨어지기 연출
        transform.DOLocalMoveY(jumpPower, duration)  // 점프
            .SetEase(Ease.OutQuad)  // 점프시 부드럽게 올라가게
            .OnComplete(() =>
            {
                // 점프 후 바로 떨어지기
                transform.DOLocalMoveY(-1000f, fallDuration)  // 떨어지는 거리와 시간
                    .SetEase(Ease.InQuad)  // 떨어질 때 자연스럽게
                    .OnKill(() => OnDespawn());  // 떨어지면 사라짐
            });

        // 좌우로 튕기게 하기
        textRb.AddForce(new Vector2(randomX, 3f), ForceMode2D.Impulse);  // 좌우로 튕기기
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
        DamageText.fontSize = 25;
        returnPoolt?.Invoke(gameObject);
    }
}
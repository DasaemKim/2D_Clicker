using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    public static StageUI Instance;

    public TextMeshProUGUI NowStage;
    public TextMeshProUGUI MaxStage;
    public TextMeshProUGUI StepEnemyName;
    public TextMeshProUGUI EnemyName;

    public Image EnemyHP;
    public Image DelayedHP;

    private CreateText createText;
    private DamageTextUI damageTextUI;
    private Coroutine delayedCoroutine;

    public CreateText CreateText
    {
        get => createText;
        set => createText = value;
    }

    public DamageTextUI DamageTextUI
    {
        get => damageTextUI;
        set => damageTextUI = value;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        EnemyManager.Instance.UpdateStageNum += UpdateStage;

        EnemyManager.Instance.UpdateEnemyName += UpdateEnemyName;
    }


    public void UpdateStage()  // 스테이지 업데이트
    {
        NowStage.text = (GameManager.Instance.player.playerData.stage).ToString();
    }

    public void UpdateEnemyName() // Enemy이름 업데이트
    {
        StepEnemyName.text = GameManager.Instance.Enemy.EnemyData.Name + " " + GameManager.Instance.player.playerData.step.ToString();
        EnemyName.text = StepEnemyName.text;
    }

    public void UpdateEnemyHP()  // Enemy체력 업데이트
    {
        EnemyHP.fillAmount = GameManager.Instance.Enemy.CurrentHealth / GameManager.Instance.Enemy.MaxHealth;

        if (delayedCoroutine != null)
            StopCoroutine(delayedCoroutine);

        delayedCoroutine = StartCoroutine(SmoothUpdateEnemyHP(EnemyHP.fillAmount));
    }
    
    public IEnumerator SmoothUpdateEnemyHP(float target)  // Enemy체력 감소 연출
    {
        yield return new WaitForSeconds(-0.1f); // 딜레이 효과

        float start = DelayedHP.fillAmount;
        float elapsedTime = 0f;
        float duration = 0.1f; // 체력바가 변화하는 시간

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            DelayedHP.fillAmount = Mathf.Lerp(start, target, elapsedTime / duration);
            yield return null;
        }

        DelayedHP.fillAmount = target;
    }
}

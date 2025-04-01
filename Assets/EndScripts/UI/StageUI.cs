using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using static System.Net.Mime.MediaTypeNames;

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


    public void UpdateStage()  // �������� ������Ʈ
    {
        NowStage.text = (EnemyManager.Instance.SpawnCount - 1).ToString();
    }

    public void UpdateEnemyName() // Enemy�̸� ������Ʈ
    {
        StepEnemyName.text = EnemyManager.Instance.EnemyData.Name + " " + EnemyManager.Instance.Step.ToString();
        EnemyName.text = StepEnemyName.text;
    }

    //public void UpdateEnemyHP()  // Enemyü�� ������Ʈ
    //{
    //    EnemyHP.fillAmount = GameManager.Instance.Enemy.CurrentHealth / GameManager.Instance.Enemy.MaxHealth;
    //}

    public void UpdateEnemyHP()  // Enemyü�� ������Ʈ
    {
        EnemyHP.fillAmount = GameManager.Instance.Enemy.CurrentHealth / GameManager.Instance.Enemy.MaxHealth;

        if (delayedCoroutine != null)
            StopCoroutine(delayedCoroutine);

        delayedCoroutine = StartCoroutine(SmoothUpdateEnemyHP(EnemyHP.fillAmount));
    }
    
    public IEnumerator SmoothUpdateEnemyHP(float target)  // Enemyü�� ���� ����
    {
        yield return new WaitForSeconds(0.2f); // ������ ȿ��

        float start = DelayedHP.fillAmount;
        float elapsedTime = 0f;
        float duration = 0.7f; // ü�¹ٰ� ��ȭ�ϴ� �ð�

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            DelayedHP.fillAmount = Mathf.Lerp(start, target, elapsedTime / duration);
            yield return null;
        }

        DelayedHP.fillAmount = target;
    }
}

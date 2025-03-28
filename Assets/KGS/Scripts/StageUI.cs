using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    public static StageUI Instance;

    public TextMeshProUGUI NowStage;
    public TextMeshProUGUI MaxStage;
    public TextMeshProUGUI StepEnemyName;
    public TextMeshProUGUI Step;
    public TextMeshProUGUI EnemyName;
    public TextMeshProUGUI EnemyStep;

    public Image EnemyHP;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        EnemyManager.Instance.UpdateStageNum += UpdateStage;

        EnemyManager.Instance.UpdateStepNum += UpdateStep;

        EnemyManager.Instance.UpdateEnemyName += UpdateEnemyName;
    }

    //private void Update()
    //{
    //    UpdateEnemyHP();
    //}

    public void UpdateStage()
    {
        NowStage.text = (EnemyManager.Instance.SpawnCount - 1).ToString();
    }

    public void UpdateStep()
    {
        Step.text = EnemyManager.Instance.Step.ToString();
        EnemyStep.text = Step.text;
    }

    public void UpdateEnemyName()
    {
        StepEnemyName.text = EnemyManager.Instance.EnemyData.Name;
        EnemyName.text = StepEnemyName.text;
    }

    public void UpdateEnemyHP()
    {
        EnemyHP.fillAmount = GameManager.Instance.Enemy.CurrentHealth / GameManager.Instance.Enemy.MaxHealth;
    }

}

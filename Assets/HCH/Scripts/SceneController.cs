using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;

    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private GameObject startupUI;
    [SerializeField] private GameObject mainUI;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        fadeImage.color = new Color(0, 0, 0, 1); // ������ ���� ȭ��
        StartCoroutine(FadeAlpha(1f, 0f)); // ������ �����
    }

    public void StartNewGame()
    {
        GameManager.Instance.NewGame();
        FadeToMainUI();
    }

    public void LoadSavedGame()
    {
        GameManager.Instance.LoadGame();
        FadeToMainUI();
    }

    private void FadeToMainUI()
    {
        StartCoroutine(FadeAlpha(0f, 1f, () => {
            startupUI.SetActive(false);
            mainUI.SetActive(true);
            StartCoroutine(FadeAlpha(1f, 0f));
        }));
    }

    private IEnumerator FadeAlpha(float from, float to, System.Action onComplete = null)
    {
        Color color = fadeImage.color;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDuration);
            color.a = Mathf.Lerp(from, to, t);
            fadeImage.color = color;
            yield return null;
        }

        color.a = to;
        fadeImage.color = color;
        onComplete?.Invoke();
    }
}

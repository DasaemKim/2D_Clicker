using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;

    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 1f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        fadeImage.color = new Color(0, 0, 0, 1); // 시작은 검은 화면
        StartCoroutine(FadeAlpha(1f, 0f)); // 서서히 밝아짐
    }

    public void StartNewGame()
    {
        GameManager.Instance.NewGame();
        StartCoroutine(TransitionToScene("MainScene"));
    }

    public void LoadSavedGame()
    {
        GameManager.Instance.LoadGame();
        StartCoroutine(TransitionToScene("MainScene"));
    }

    private IEnumerator TransitionToScene(string sceneName)
    {
        Debug.Log("씬 전환 준비");
        yield return StartCoroutine(FadeAlpha(0f, 1f));
        Debug.Log("씬 전환 시도: " + sceneName);
        SceneManager.sceneLoaded += OnSceneLoaded; // 씬 로드 후 실행할 메서드 등록
        SceneManager.LoadScene(sceneName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // 이벤트 해제 (중복 실행 방지)
        Debug.Log("씬 전환 완료! NewGame() 실행");
        GameManager.Instance.NewGame();
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

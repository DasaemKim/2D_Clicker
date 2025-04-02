using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;

    [SerializeField] private Image fadeImage; //화면 전환용 검은 이미지
    [SerializeField] private float fadeDuration = 1f; // 페이드 인/아웃 시간

    private bool isNewGame; //새 게임 여부 

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        fadeImage.color = new Color(0, 0, 0, 1); // 시작은 검은 화면
        StartCoroutine(FadeAlpha(1f, 0f)); // 서서히 밝아짐
    }

    //새 게임 시작 버튼 클릭 시 호출
    public void StartNewGame()
    {
        isNewGame = true;
        StartCoroutine(TransitionToScene("MainScene"));
    }

    //저장된 게임 불러오기 버튼 클릭 시 호출
    public void LoadSavedGame()
    {
        StartCoroutine(TransitionToScene("MainScene"));
    }

    //씬 전환 및 페이드 효과 처리
    private IEnumerator TransitionToScene(string sceneName)
    {
        yield return StartCoroutine(FadeAlpha(0f, 1f)); //화면 어두워지기(페이드 아웃)
        SceneManager.sceneLoaded += OnSceneLoaded; // 씬 로드 후 실행할 메서드 등록
        SceneManager.LoadScene(sceneName); //씬 전환
    }

    //씬이 로드된 후 실행됨
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // 이벤트 해제 (중복 실행 방지)

        //세이브 파일이 없거나 새 게임을 시작한 경우
        if (!File.Exists(GameManager.Instance.savePath) || isNewGame)
        {
            GameManager.Instance.NewGame(); //데이터 초기화
            isNewGame = false;
        }
        GameManager.Instance.LoadGame(); // 저장된 데이터 불러오기
    }

    // 화면 알파값 페이드 처리
    private IEnumerator FadeAlpha(float from, float to, System.Action onComplete = null)
    {
        Color color = fadeImage.color;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDuration);
            color.a = Mathf.Lerp(from, to, t); // 알파값 점진적 변화
            fadeImage.color = color;
            yield return null;
        }

        //최종 알파값 적용
        color.a = to;
        fadeImage.color = color;
        onComplete?.Invoke(); //완료 콜백 실행
    }
}

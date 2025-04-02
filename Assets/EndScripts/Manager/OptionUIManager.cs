using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionUIManager : MonoBehaviour
{
    [Header("옵션 패널")]
    public GameObject optionPanel; //옵션 UI 패널
    public bool isOpen = false; //옵션창 열림 여부

    [Header("볼륨조절")]
    public Slider bgmSlider; //배경음악 슬라이더
    public Slider sfxSlider; //효과음 슬라이더

    [Header("배경음악")]
    public AudioSource bgmAudioSource; //배경음악 오디오 소스
    public AudioSource sfxAudioSource; //효과음 오디오 소스
    private void Start()
    {
        //저장된 BGM볼륨 불러오기
        float saveVolume = PlayerPrefs.GetFloat("BGMVolume", 0.5f);

        bgmSlider.value = saveVolume;
        bgmAudioSource.volume = saveVolume;

        // Bgm슬라이더 값 변경 시 이벤트 등록
        bgmSlider.onValueChanged.AddListener((value) =>
        {
            bgmAudioSource.volume = value;
            PlayerPrefs.SetFloat("BGMVolume", value); //볼륨 저장
        });

        // 저장된 SFX볼륨 불러오기
        float saveSfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
        sfxSlider.value = saveSfxVolume;
        sfxAudioSource.volume = saveSfxVolume;

        // SFX 슬라이더 값 변경 시 이벤트 등록
        sfxSlider.onValueChanged.AddListener((value) =>
        {
            sfxAudioSource.volume = value;
            PlayerPrefs.SetFloat("SFXVolume", value); //볼륨 저장
        });

    }

    private void Update()
    {
        //esc 키로 옵션창 토글
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleOption();
        }
    }

    //옵션창 토글(열기/닫기 전환)
    public void ToggleOption()
    {
        isOpen = !isOpen;
        optionPanel.SetActive(isOpen);
        Time.timeScale = isOpen ? 0 : 1; //게임 일시 정지
    }

    //옵션창 열기
    public void OpenOption()
    {
        isOpen = true;
        optionPanel.SetActive(true);
        Time.timeScale = 0;
    }

    //옵션창 닫기
    public void CloseOption()
    {
        isOpen = false;
        optionPanel.SetActive(false);
        Time.timeScale = 1;
    }

    //타이틀 씬으로 이동
    public void TitleScene()
    {
        isOpen = false;
        optionPanel.SetActive(false);
        Time.timeScale = 1;
        
        GameManager.Instance.SaveGame(); //게임 저장
        SceneManager.LoadScene("TitleScene");
    }
}

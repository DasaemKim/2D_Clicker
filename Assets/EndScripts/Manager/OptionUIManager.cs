using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionUIManager : MonoBehaviour
{
    [Header("옵션 패널")]
    public GameObject optionPanel;
    public bool isOpen = false;

    [Header("볼륨조절")]
    public Slider bgmSlider;
    public Slider sfxSlider;

    [Header("배경음악")]
    public AudioSource bgmAudioSource;
    public AudioSource sfxAudioSource;
    private void Start()
    {
        float saveVolume = PlayerPrefs.GetFloat("BGMVolume", 0.5f);

        bgmSlider.value = saveVolume;
        bgmAudioSource.volume = saveVolume;

        bgmSlider.onValueChanged.AddListener((value) =>
        {
            bgmAudioSource.volume = value;
            PlayerPrefs.SetFloat("BGMVolume", value);
        });

        float saveSfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
        sfxSlider.value = saveSfxVolume;
        sfxAudioSource.volume = saveSfxVolume;

        sfxSlider.onValueChanged.AddListener((value) =>
        {
            sfxAudioSource.volume = value;
            PlayerPrefs.SetFloat("SFXVolume", value);
        });

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleOption();
        }
    }

    public void ToggleOption()
    {
        isOpen = !isOpen;
        optionPanel.SetActive(isOpen);
        Time.timeScale = isOpen ? 0 : 1;
    }

    public void OpenOption()
    {
        isOpen = true;
        optionPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseOption()
    {
        isOpen = false;
        optionPanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void TitleScene()
    {
        isOpen = false;
        optionPanel.SetActive(false);
        Time.timeScale = 1;

        SceneManager.LoadScene("TitleScene");
    }
}

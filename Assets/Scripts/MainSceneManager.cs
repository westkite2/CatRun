using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    // Summary: Manage Main Scene buttons

    private AudioSource sfxAudioSource;
    private AudioSource bgmAudioSource;
    public AudioClip sfxButtonClick;
    public GameObject objSettingsWindow;
    public GameObject objInfoWindow;
    public GameObject mainCamera;
    public Slider bgmVolumnSlider;
    public Slider sfxVolumnSlider;

    private void ControlAudioVolumn()
    {
        //Update audio volumn
        bgmAudioSource.volume = bgmVolumnSlider.value;
        sfxAudioSource.volume = sfxVolumnSlider.value;

        //Save audio volumn
        PlayerPrefs.SetFloat("bgmVolumn", bgmVolumnSlider.value);
        PlayerPrefs.SetFloat("sfxVolumn", sfxVolumnSlider.value);
    }

    private void Start()
    {
        objSettingsWindow.SetActive(false);
        objInfoWindow.SetActive(false);

        bgmAudioSource = mainCamera.GetComponent<AudioSource>();
        sfxAudioSource = GetComponent<AudioSource>();

        bgmVolumnSlider.value = PlayerPrefs.GetFloat("bgmVolumn", 1f);
        sfxVolumnSlider.value = PlayerPrefs.GetFloat("sfxVolumn", 1f);
        bgmAudioSource.volume = bgmVolumnSlider.value;
        sfxAudioSource.volume = sfxVolumnSlider.value;
    }

    private void Update()
    {
        ControlAudioVolumn();
    }
    public void OnClickStartButton()
    {
        // Start game
        sfxAudioSource.PlayOneShot(sfxButtonClick);
        Time.timeScale = 1f;
        SceneManager.LoadScene("NightCity");
    }

    public void OnClickExitButton()
    {
        // Exit program
        sfxAudioSource.PlayOneShot(sfxButtonClick);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void OnClickSettingsButton()
    {
        sfxAudioSource.PlayOneShot(sfxButtonClick);
        objSettingsWindow.SetActive(true);
    }

    public void OnClickCloseWindowButton()
    {
        sfxAudioSource.PlayOneShot(sfxButtonClick);
        objSettingsWindow.SetActive(false);
        objInfoWindow.SetActive(false);
    }
    
    public void OnClickInfoButton()
    {
        sfxAudioSource.PlayOneShot(sfxButtonClick);
        objInfoWindow.SetActive(true);
    }

}

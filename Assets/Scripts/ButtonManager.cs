using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject PauseUI;
    AudioSource Audiosource;
    public AudioClip sfx_buttonClick;
    private void Awake()
    {
        this.Audiosource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        PauseUI.SetActive(false);
    }

    public void OnClickExitButton()
    {
        Audiosource.PlayOneShot(sfx_buttonClick);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnClickStartButton()
    {
        Audiosource.PlayOneShot(sfx_buttonClick);
        Time.timeScale = 1f;
        SceneManager.LoadScene("NightCity");
    }
    public void OnClickGoMainButton()
    {
        Audiosource.PlayOneShot(sfx_buttonClick);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene");
    }

    public void OnClickPauseButton()
    {
        Audiosource.PlayOneShot(sfx_buttonClick);
        Time.timeScale = 0f;
        PauseUI.SetActive(true);
    }
    public void OnClickKeepPlayingButton()
    {
        Audiosource.PlayOneShot(sfx_buttonClick);
        PauseUI.SetActive(false);
        Time.timeScale = 1f;
    }

}

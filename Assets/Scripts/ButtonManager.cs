using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    //Summary: Manage UI buttons

    private AudioSource audioSource;
    public AudioClip sfxButtonClick;
    public GameObject objPauseWindow;

    private void Start()
    {
        objPauseWindow.SetActive(false);
        this.audioSource = GetComponent<AudioSource>();
    }

    public void OnClickExitButton()
    {
        audioSource.PlayOneShot(sfxButtonClick);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnClickStartButton()
    {
        audioSource.PlayOneShot(sfxButtonClick);
        Time.timeScale = 1f;
        SceneManager.LoadScene("NightCity");
    }

    public void OnClickGoMainButton()
    {
        audioSource.PlayOneShot(sfxButtonClick);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene");
    }

    public void OnClickPauseButton()
    {
        audioSource.PlayOneShot(sfxButtonClick);
        Time.timeScale = 0f;
        objPauseWindow.SetActive(true);
    }

    public void OnClickKeepPlayingButton()
    {
        audioSource.PlayOneShot(sfxButtonClick);
        objPauseWindow.SetActive(false);
        Time.timeScale = 1f;
    }

}

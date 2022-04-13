using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    // Summary: Manage Main Scene buttons

    private AudioSource audioSource;
    public AudioClip sfxButtonClick;

    private void Awake()
    {
        this.audioSource = GetComponent<AudioSource>();
    }

    public void OnClickStartButton()
    {
        // Start game
        audioSource.PlayOneShot(sfxButtonClick);
        Time.timeScale = 1f;
        SceneManager.LoadScene("NightCity");
    }

    public void OnClickExitButton()
    {
        // Exit program
        audioSource.PlayOneShot(sfxButtonClick);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }


}

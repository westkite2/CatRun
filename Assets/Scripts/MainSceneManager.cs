using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    AudioSource Audiosource;
    public AudioClip sfx_buttonClick;
    private void Awake()
    {
        this.Audiosource = GetComponent<AudioSource>();
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
}

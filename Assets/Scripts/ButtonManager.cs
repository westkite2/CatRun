using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject PauseUI;

    private void Start()
    {
        PauseUI.SetActive(false);
    }
    public void OnClickStartButton()
    {
        //Audiosource.PlayOneShot(AC_click);
        Time.timeScale = 1f;
        SceneManager.LoadScene("NightCity");
    }
    public void OnClickGoMainButton()
    {
        //Audiosource.PlayOneShot(AC_click);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene");
    }

    public void OnClickPauseButton()
    {
        //Audiosource.PlayOneShot(AC_click);
        Time.timeScale = 0f;
        PauseUI.SetActive(true);
    }
    public void OnClickKeepPlayingButton()
    {
        //Audiosource.PlayOneShot(AC_click);
        PauseUI.SetActive(false);
        Time.timeScale = 1f;
    }
}

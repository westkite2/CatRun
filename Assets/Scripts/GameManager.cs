using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class GameManager : MonoBehaviour
{
    //Summary: Manage game score and audio

    //For audio
    private AudioSource audioSource;
    private AudioSource bgmGameScene;
    public GameObject mainCamera;
    public Slider bgmVolumn;
    public Slider sfxVolumn;
    public AudioClip sfxJump;
    public AudioClip sfxCook;
    public AudioClip sfxDamage;
    public AudioClip sfxEat;
    public AudioClip sfxGameClear;
    public AudioClip sfxGameOver;

    //For game clear
    private static int GameClearScore = 10;
    public bool isGameClear;
    public bool isBackgroundStop;
    public float currentScore;
    public GameObject objSignBoard;
    public Image imgHpFill;
    public TextMeshProUGUI txtScore;
    public GameObject objGameClear;
    


    private void Awake()
    {
        //Initialize variables
        currentScore = 1f;
        bgmVolumn.value = 1f;
        sfxVolumn.value = 1f;
        isGameClear = false;
        isBackgroundStop = false;
    }

    private void Start()
    {
        objSignBoard.SetActive(false);
        objGameClear.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        bgmGameScene = mainCamera.GetComponent<AudioSource>();
        bgmGameScene.volume = bgmVolumn.value;
        audioSource.volume = sfxVolumn.value;
    }

    private void Update()
    {
        //Update audio volumn
        bgmGameScene.volume = bgmVolumn.value;
        audioSource.volume = sfxVolumn.value;

        //Display score
        txtScore.text = (Math.Truncate(currentScore * 10) / 10).ToString();
        imgHpFill.fillAmount = currentScore / 10;

        //Show game clear on clear
        if (!isGameClear && currentScore >= GameClearScore )
        {            
            PlaySound("GAMECLEAR");
            isGameClear = true;
            objGameClear.SetActive(true);
        }

        //Show signboard on game clear
        if (isGameClear && !isBackgroundStop)
        {
            objSignBoard.SetActive(true);
            objSignBoard.transform.position += Vector3.left * 4 * Time.deltaTime;
            //Stop background movement when signboard is on the right position
            if (objSignBoard.transform.localPosition.x <= 6)
            {
                isBackgroundStop = true;
            }
        }
    }
    public void PlaySound(string soundName)
    {
        //Play sfx once
        switch (soundName)
        {
            case "JUMP":
                audioSource.PlayOneShot(sfxJump);
                break;
            case "COOK":
                audioSource.PlayOneShot(sfxCook);
                break;
            case "EAT":
                audioSource.PlayOneShot(sfxEat);
                break;
            case "DAMAGE":
                audioSource.PlayOneShot(sfxDamage);
                break;
            case "GAMEOVER":
                audioSource.PlayOneShot(sfxGameOver);
                break;
            case "GAMECLEAR":
                audioSource.PlayOneShot(sfxGameClear);
                break;
        }
    }

    public void AdminChangeCurrentScore()
    {
        //Change current score to see game clear
        currentScore = 9.8f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    //Summary: Manage player hp display and audio

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

    //For game ending
    private BackgroundController scriptBackgroundController;
    private float nextHpDecreaseTime;
    private float hpDecreaseTimeGap;
    public static float maxHp = 100f;
    public bool isGameEnd;
    public bool isEndOfRoad;
    public bool isGameSuccess;
    public float currentHp;
    public GameObject objBackRoad;
    public GameObject objSignBoard;
    public Image imgHpFill;
    public TextMeshProUGUI txtHp;
    public GameObject objGameClear;

    public void ShowGameResult()
    {
        //Show game clear on success
        if (isGameSuccess)
        {
            Debug.Log("Game Clear!");
            //PlaySound("GAMECLEAR");
            objGameClear.SetActive(true);
        }
        //Show game over on fail
        else
        {
            Debug.Log("Game Over!");
        }
    }

    private void DecreaseHp()
    {
        //Decrease hp continuously
        if (Time.time > nextHpDecreaseTime)
        {
            nextHpDecreaseTime = Time.time + hpDecreaseTimeGap;
            currentHp -= 0.01f;
        }
    }
    private void Awake()
    {
        //Initialize variables
        currentHp = 100f;
        nextHpDecreaseTime = 0.0f;
        hpDecreaseTimeGap = 0.01f;
        bgmVolumn.value = 1f;
        sfxVolumn.value = 1f;
        isGameEnd = false;
        isEndOfRoad = false;
        isGameSuccess = false;
    }

    private void Start()
    {
        objSignBoard.SetActive(false);
        objGameClear.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        bgmGameScene = mainCamera.GetComponent<AudioSource>();
        scriptBackgroundController = objBackRoad.GetComponent<BackgroundController>();
        bgmGameScene.volume = bgmVolumn.value;
        audioSource.volume = sfxVolumn.value;
    }

    private void Update()
    {
        //Update audio volumn
        bgmGameScene.volume = bgmVolumn.value;
        audioSource.volume = sfxVolumn.value;

        //Display hp
        txtHp.text = currentHp.ToString();
        imgHpFill.fillAmount = (currentHp / maxHp);

        if (!isGameEnd)
        {
            DecreaseHp();
            
            //Game over if zero hp
            if (currentHp <= 0)
            {
                isGameEnd = true;
                ShowGameResult();
            }

            //Show signboard(end point) if game end is near
            if (scriptBackgroundController.scrollCount == 49)
            {
                objSignBoard.SetActive(true);
                objSignBoard.transform.position += Vector3.left * 4 * Time.deltaTime;
            }

            //End game if player walked enough distance
            if (scriptBackgroundController.scrollCount == 50)
            {
                isGameEnd = true;
                isEndOfRoad = true;
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
        //Admin option
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Summary: Manage player hp and audio

    //For Food collection
    public int[] countFood;
    public Text[] textFood;

    //For audio
    private AudioSource sfxAudioSource;
    private AudioSource bgmAudioSource;
    private AudioSource bgmSeaAudioSource;
    public bool isEnterSeaMode;
    public bool isExitSeaMode;
    public GameObject mainCamera;
    public Slider bgmVolumnSlider;
    public Slider sfxVolumnSlider;
    public AudioClip sfxButtonClick;
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
    private bool isShowCatShark;
    public bool isGameEnd;
    public bool isEndOfRoad;
    public bool isGameSuccess;
    public float currentHp;
    public Image imgHpFill;
    public GameObject objCatShark;
    public GameObject objBackRoad;
    public GameObject objSignBoard;
    public GameObject objMainCanvas;
    public GameObject objGameClear;
    public GameObject objGameOver;
    public GameObject objSea;

    //Special modes
    private bool isIntro;
    private bool isStartCountingScroll;
    private int startScroll;
    private int currentIntroIdx;
    public bool isSeaMode;
    public bool isCarMode;
    public bool isExitCarMode;
    public GameObject objIntro;
    public GameObject[] objIntroTextArr;
    public Text objNextText;

    public void ShowGameResult()
    {
        objMainCanvas.SetActive(false);
        //Show game clear on success
        if (isGameSuccess)
        {
            //PlaySound("GAMECLEAR");
            objMainCanvas.SetActive(false);
            objGameClear.SetActive(true);
            isShowCatShark = true;
        }
        //Show game over on fail
        else
        {
            objMainCanvas.SetActive(false);
            objGameOver.SetActive(true);
        }
    }
    public void GetGameResult()
    {
        if (currentHp > 0)
        {
            for (int i = 0; i < 5; i++)
            {
                if (countFood[i] < 10)
                {
                    isGameSuccess = false;
                    break;
                }
                if (i == 4)
                {
                    isGameSuccess = true;
                }
            }
            ShowGameResult();
        }
        else
        {
            isGameSuccess = false;
            ShowGameResult();
        }
    }
    private void ControlAudioVolumn()
    {
        //Update audio volumn
        bgmAudioSource.volume = bgmVolumnSlider.value;
        bgmSeaAudioSource.volume = bgmVolumnSlider.value;
        sfxAudioSource.volume = sfxVolumnSlider.value;

        //Save audio volumn
        PlayerPrefs.SetFloat("bgmVolumn", bgmVolumnSlider.value);
        PlayerPrefs.SetFloat("sfxVolumn", sfxVolumnSlider.value);
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

    private void PlayBGM()
    {
        if(bgmAudioSource != null)
        {
            if (isEnterSeaMode)
            {
                bgmAudioSource.Pause();
                bgmSeaAudioSource.Play();
                isEnterSeaMode = false;
            }
            if (isExitSeaMode){
                bgmSeaAudioSource.Pause();
                bgmAudioSource.Play();
                isExitSeaMode = false;
            }
        }
    }

    public void onClickSkipIntro()
    {
        PlaySound("CLICK");
        objIntro.SetActive(false);
        bgmAudioSource.Play();
        Time.timeScale = 1f;
    }

    public void onClickNextIntro()
    {
        PlaySound("CLICK");

        if (currentIntroIdx < 8)
        {
            objIntroTextArr[currentIntroIdx].gameObject.SetActive(false);
            currentIntroIdx += 1;
            objIntroTextArr[currentIntroIdx].gameObject.SetActive(true);

            //Change text if last page
            if (currentIntroIdx == 8)
            {
                objNextText.text = "½ÃÀÛ!";
            }
        }
        //Start game
        else
        {
            objIntro.SetActive(false);
            bgmAudioSource.Play();
            Time.timeScale = 1f;
        }

    }

    private void ShowIntro()
    {
        Time.timeScale = 0f;
        bgmAudioSource.Pause();

        //Inactivate all Text except first
        for(int i = 1; i < 9; i++)
        {
            objIntroTextArr[i].gameObject.SetActive(false);
        }
        objIntroTextArr[0].gameObject.SetActive(true);
        currentIntroIdx = 0;

    }

    private void DisplayFoodCount()
    {
        for (int i = 0; i < 5; i++)
        {
            textFood[i].text = countFood[i].ToString();
        }
    }

    private void DisplayHp()
    {
        imgHpFill.fillAmount = (currentHp / maxHp);
    }

    private void Awake()
    {
        //Initialize variables
        for(int i=0; i<5; i++)
        {
            countFood[i] = 0;
        }
        currentHp = 100f;
        nextHpDecreaseTime = 0.0f;
        hpDecreaseTimeGap = 0.01f;
        bgmVolumnSlider.value = 1f;
        sfxVolumnSlider.value = 1f;
        isGameEnd = false;
        isEndOfRoad = false;
        isGameSuccess = false;
        isSeaMode = false;
        isEnterSeaMode = false;
        isExitSeaMode = false;
        isCarMode = false;
        isStartCountingScroll = false;
        isExitCarMode = false;
        isIntro = true;
        isShowCatShark = false;
        objIntro.SetActive(true);
    }

    private void Start()
    {
        objSea.SetActive(false);
        objSignBoard.SetActive(false);
        objGameClear.SetActive(false);
        objGameOver.SetActive(false);
        sfxAudioSource = GetComponent<AudioSource>();
        bgmAudioSource = mainCamera.GetComponent<AudioSource>();
        bgmSeaAudioSource = objSea.GetComponent<AudioSource>();
        scriptBackgroundController = objBackRoad.GetComponent<BackgroundController>();

        bgmVolumnSlider.value = PlayerPrefs.GetFloat("bgmVolumn", 1f);
        sfxVolumnSlider.value = PlayerPrefs.GetFloat("sfxVolumn", 1f);
        bgmAudioSource.volume = bgmVolumnSlider.value;
        bgmSeaAudioSource.volume = bgmVolumnSlider.value;
        sfxAudioSource.volume = sfxVolumnSlider.value;
        
        //Show intro
        if (isIntro)
        {
            ShowIntro();
        }
    }

    private void Update()
    {

        ControlAudioVolumn();

        PlayBGM();

        DisplayFoodCount();

        DisplayHp();

        if (!isGameEnd)
        {
            DecreaseHp();
            
            //Game over if zero hp
            if (currentHp <= 0)
            {
                isGameEnd = true;
                isGameSuccess = false;
                ShowGameResult();
            }

            //Show signboard(end point) if game end is near
            if (scriptBackgroundController.scrollCount == 49)
            {
                objSignBoard.SetActive(true);
                objSignBoard.transform.position += Vector3.left * 6 * Time.deltaTime;
            }

            //End game if player walked enough distance
            if (scriptBackgroundController.scrollCount == 50)
            {
                isGameEnd = true;
                isEndOfRoad = true;                
            }

            //Car mode
            if (isCarMode)
            {
                //Start Counting scroll
                if (!isStartCountingScroll)
                {
                    startScroll = scriptBackgroundController.scrollCount;
                    isStartCountingScroll = true;
                }
                //Stop Car mode
                if (scriptBackgroundController.scrollCount - startScroll >= 10)
                {
                    isCarMode = false;
                    isStartCountingScroll = false;
                    isExitCarMode = true;
                }

            }
        }

        if (isShowCatShark)
        {
            objCatShark.transform.Translate(-0.01f, 0, 0);
        }
    }

    public void PlaySound(string soundName)
    {
        //Play sfx once
        switch (soundName)
        {
            case "CLICK":
                sfxAudioSource.PlayOneShot(sfxButtonClick);
                break;
            case "JUMP":
                sfxAudioSource.PlayOneShot(sfxJump);
                break;
            case "COOK":
                sfxAudioSource.PlayOneShot(sfxCook);
                break;
            case "EAT":
                sfxAudioSource.PlayOneShot(sfxEat);
                break;
            case "DAMAGE":
                sfxAudioSource.PlayOneShot(sfxDamage);
                break;
            case "GAMEOVER":
                sfxAudioSource.PlayOneShot(sfxGameOver);
                break;
            case "GAMECLEAR":
                sfxAudioSource.PlayOneShot(sfxGameClear);
                break;
        }
    }

}

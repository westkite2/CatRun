using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class GameManager : MonoBehaviour
{
    private AudioSource Audiosource;
    private AudioSource Bgm;
    public AudioClip SFX_jump;
    public AudioClip SFX_cook;
    public AudioClip SFX_damage;
    public AudioClip SFX_eat;
    public AudioClip SFX_gameClear;
    public AudioClip SFX_gameOver;

    public float score;
    public bool flag_gameClear;
    public bool flag_backStop;
    public Image Hp_fill;
    public TextMeshProUGUI UI_Score;
    public GameObject GameClear;
    private static int CLEARSCORE = 10;
    private GameObject SignBoard;

    void Awake()
    {
        score = 1f;
        flag_gameClear = false;
        flag_backStop = false;

        SignBoard = GameObject.Find("Environment").transform.GetChild(3).gameObject;
        Audiosource = GetComponent<AudioSource>();
        Bgm = GameObject.Find("Main Camera").GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {        
        SignBoard.SetActive(false);
        GameClear.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Display score
        UI_Score.text = (Math.Truncate(score * 10) / 10).ToString();
        Hp_fill.fillAmount = score / 10;
        
        if (score >= CLEARSCORE && !flag_gameClear)
        {
            //Game clear flag up
            PlaySound("GAMECLEAR");
            flag_gameClear = true;
            GameClear.SetActive(true);
        }
        if (flag_gameClear && !flag_backStop)
        {
            //Sign board appears
            SignBoard.SetActive(true);
            SignBoard.transform.position += Vector3.left * 4 * Time.deltaTime;
            if (SignBoard.transform.localPosition.x <= 6)
            {
                //Stop background movement
                flag_backStop = true;
            }
        }
    }
    public void PlaySound(string command)
    {
        switch (command)
        {
            case "JUMP":
                Audiosource.PlayOneShot(SFX_jump);
                break;
            case "COOK":
                Audiosource.PlayOneShot(SFX_cook);
                break;
            case "EAT":
                Audiosource.PlayOneShot(SFX_eat);
                break;
            case "DAMAGE":
                Audiosource.PlayOneShot(SFX_damage);
                break;
            case "GAMEOVER":
                Audiosource.PlayOneShot(SFX_gameOver);
                break;
            case "GAMECLEAR":
                Audiosource.PlayOneShot(SFX_gameClear);
                break;

        }
    }

    public void ADMIN_changeScore()
    {
        score = 9.5f;
    }
}

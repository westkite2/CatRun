using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int score;
    public bool flag_gameClear;
    public bool flag_backStop;
    public Text UI_Score;

    private static int CLEARSCORE = 3;
    private GameObject SignBoard;

    private void Awake()
    {
        score = 0;
        flag_gameClear = false;
        flag_backStop = false;

        SignBoard = GameObject.Find("Environment").transform.GetChild(3).gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {        
        SignBoard.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Display score
        UI_Score.text = score.ToString();

        if (score >= CLEARSCORE && !flag_gameClear)
        {
            //Game clear flag up
            flag_gameClear = true;
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
}

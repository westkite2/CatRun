using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    //Summary: Create food objects in advance (object pooling), clear them when game ends

    /*SUSHI
    private GameObject[] objSalmonList;
    private GameObject[] objShrimpList;
    private GameObject[] objTunaList;*/
    private GameObject[] objEggList;
    //private GameObject[] objFishEggList;
    
    public GameManager GameManager;

    /*SUSHI
    public GameObject objSalmon;
    public GameObject objShrimp;
    public GameObject objTuna; */
    public GameObject objEgg;
    //public GameObject objFishEgg;
    
    /*SUSHI
    private int numSalmon = 8;
    private int numShrimp = 8;
    private int numTuna = 8;*/
    private int numEgg = 8;
    //private int numFishEgg = 8;
    
    /*SUSHI
    private int idxSalmon = 0;
    private int idxShrimp = 0;
    private int idxTuna = 0;*/
    private int idxEgg = 0;
    //private int idxFishEgg = 0;

    private void CreateObjects(GameObject objFood, ref GameObject[] objFoodList, int num)
    {
        //Create and store food objects
        objFoodList = new GameObject[num];
        for (int i = 0; i < num; i++)
        {
            GameObject FoodObject = Instantiate(objFood);
            FoodObject.transform.position =
                new Vector2(Random.Range(14.0f, 25.0f), Random.Range(-2.3f, 2.6f));
            objFoodList[i] = FoodObject;
            FoodObject.SetActive(false);
        }
    }

    IEnumerator ActivateFood()
    {
        //Activate food objects periodically while game playing
        yield return new WaitForSeconds(1f);
        if (!GameManager.isSeaMode)
        {
            objEggList[idxEgg++].SetActive(true);
            if (idxEgg == numEgg) idxEgg = 0;
        }

        StartCoroutine("ActivateFood");
    }

    private void InactivateFood(ref GameObject[] FoodList, int num)
    {
        for (int i = 0; i < num; i++)
        {
            FoodList[i].SetActive(false);
        }
    }

    private void Awake()
    {
        //Create food objects
        /*SUSHI
        CreateObjects(objSalmon, ref objSalmonList, numSalmon);
        CreateObjects(objShrimp, ref objShrimpList, numShrimp);
        CreateObjects(objTuna, ref objTunaList, numTuna);*/
        CreateObjects(objEgg, ref objEggList, numEgg);
        //CreateObjects(objFishEgg, ref objFishEggList, numFishEgg);
    }

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(3, 3);
        StartCoroutine("ActivateFood");
    }

    private void Update()
    {
        //Clear all foods on game clear
        if (GameManager.isGameEnd)
        {
            StopCoroutine("ActivateFood");
            /*SUSHI
            InactivateFood(ref objSalmonList, numSalmon);
            InactivateFood(ref objShrimpList, numShrimp);
            InactivateFood(ref objTunaList, numTuna);*/
            InactivateFood(ref objEggList, numEgg);
            //InactivateFood(ref objFishEggList, numFishEgg);
        }
    }
}

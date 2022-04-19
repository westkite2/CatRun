using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    //Summary: Create food objects in advance (object pooling), clear them when game ends

    private GameObject[] objSalmonList;
    private GameObject[] objShrimpList;
    private GameObject[] objTunaList;
    private GameObject[] objEggList;
    private GameObject[] objFishEggList;

    private BackgroundController scriptBackgroundController;
    public GameObject objBackRoad;
    public GameManager GameManager;

    public GameObject objSalmon;
    public GameObject objShrimp;
    public GameObject objTuna;
    public GameObject objEgg;
    public GameObject objFishEgg;
    
    private int numSalmon = 8;
    private int numShrimp = 8;
    private int numTuna = 8;
    private int numEgg = 8;
    private int numFishEgg = 8;
    
    private int idxSalmon = 0;
    private int idxShrimp = 0;
    private int idxTuna = 0;
    private int idxEgg = 0;
    private int idxFishEgg = 0;

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
        
        //Normal mode
        if (!GameManager.isCarMode)
        {
            yield return new WaitForSeconds(1f);
            if (!GameManager.isSeaMode)
            {
                objTunaList[idxTuna++].SetActive(true);
                if (idxTuna == numTuna) idxTuna = 0;

                objSalmonList[idxSalmon++].SetActive(true);
                if (idxSalmon == numSalmon) idxSalmon = 0;

            }

            yield return new WaitForSeconds(1f);
            if (!GameManager.isSeaMode)
            {
                objEggList[idxEgg++].SetActive(true);
                if (idxEgg == numEgg) idxEgg = 0;
            }

            yield return new WaitForSeconds(1f);
            if (!GameManager.isSeaMode)
            {
                objShrimpList[idxShrimp++].SetActive(true);
                if (idxShrimp == numShrimp) idxShrimp = 0;

                objFishEggList[idxFishEgg++].SetActive(true);
                if (idxFishEgg == numFishEgg) idxFishEgg = 0;
            }

        }
        //Car mode
        else
        {
            yield return new WaitForSeconds(0.8f);
            if (!GameManager.isSeaMode)
            {
                objSalmonList[idxSalmon++].SetActive(true);
                if (idxSalmon == numSalmon) idxSalmon = 0;

                objShrimpList[idxShrimp++].SetActive(true);
                if (idxShrimp == numShrimp) idxShrimp = 0;

                objTunaList[idxTuna++].SetActive(true);
                if (idxTuna == numTuna) idxTuna = 0;

                objEggList[idxEgg++].SetActive(true);
                if (idxEgg == numEgg) idxEgg = 0;

                objFishEggList[idxFishEgg++].SetActive(true);
                if (idxFishEgg == numFishEgg) idxFishEgg = 0;
            }
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
        CreateObjects(objSalmon, ref objSalmonList, numSalmon);
        CreateObjects(objShrimp, ref objShrimpList, numShrimp);
        CreateObjects(objTuna, ref objTunaList, numTuna);
        CreateObjects(objEgg, ref objEggList, numEgg);
        CreateObjects(objFishEgg, ref objFishEggList, numFishEgg);
    }

    private void Start()
    {
        scriptBackgroundController = objBackRoad.GetComponent<BackgroundController>();
        Physics2D.IgnoreLayerCollision(3, 3);
        StartCoroutine("ActivateFood");
    }

    private void Update()
    {
        //Clear all foods on game clear
        if (GameManager.isGameEnd)
        {
            StopCoroutine("ActivateFood");
            InactivateFood(ref objSalmonList, numSalmon);
            InactivateFood(ref objShrimpList, numShrimp);
            InactivateFood(ref objTunaList, numTuna);
            InactivateFood(ref objEggList, numEgg);
            InactivateFood(ref objFishEggList, numFishEgg);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    //Summary: Create food objects in advance (object pooling), clear them when game ends

    private GameObject[] objEggList;
    private GameObject[] objChickenList;
    public GameObject objEgg;
    public GameObject objChicken;
    public GameManager GameManager;

    private int numEgg = 10;
    private int numChicken = 10;
    private int idxEgg = 0;
    private int idxChicken = 0;

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
        objEggList[idxEgg++].SetActive(true);
        if (idxEgg == numEgg) idxEgg = 0;

        yield return new WaitForSeconds(1f);
        objChickenList[idxChicken++].SetActive(true);
        if (idxChicken == numChicken) idxChicken = 0;

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
        CreateObjects(objEgg, ref objEggList, numEgg);
        CreateObjects(objChicken, ref objChickenList, numChicken);
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
            InactivateFood(ref objEggList, numEgg);
            InactivateFood(ref objChickenList, numChicken);
        }
    }
}

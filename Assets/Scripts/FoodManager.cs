using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public GameObject Egg;
    public GameObject Chicken;

    private GameObject[] EggList;
    private GameObject[] ChickenList;

    private int numEgg = 10;
    private int numChicken = 10;

    private int idxEgg = 0;
    private int idxChicken = 0;

    void CreateObjects(GameObject Food, ref GameObject[] FoodList, int num)
    {
        //Create food objects on awake for object pooling
        FoodList = new GameObject[num];
        for (int i = 0; i < num; i++)
        {
            GameObject FoodObject = Instantiate(Food);
            FoodObject.transform.position =
                new Vector2(Random.Range(14.0f, 25.0f), Random.Range(-2.3f, 2.6f));
            FoodList[i] = FoodObject;
            FoodObject.SetActive(false);
        }
    }
    IEnumerator ActivateFood()
    {
        yield return new WaitForSeconds(1f);
        EggList[idxEgg++].SetActive(true);
        if (idxEgg == numEgg) idxEgg = 0;

        yield return new WaitForSeconds(1f);
        ChickenList[idxChicken++].SetActive(true);
        if (idxChicken == numChicken) idxChicken = 0;


        StartCoroutine("ActivateFood");

    }
    private void Awake()
    {
        CreateObjects(Egg, ref EggList, numEgg);
        CreateObjects(Chicken, ref ChickenList, numChicken);
        
        Physics.IgnoreLayerCollision(3, 3);
    }
    private void Start()
    {
        StartCoroutine("ActivateFood");
    }
}

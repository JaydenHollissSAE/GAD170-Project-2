using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainFunction : MonoBehaviour
{
    public string[] plantsList = new string[] { };
    public float[] plantsListCost = new float[] { };
    public int selectedPlant;
    public float funds;
    public float totalFunds;
    public float plantCost;
    public string plantName;
    public int plantAmounts;
    public int plantCapacity;
    public string[] namesList = new string[] { "Flower", "Cabbage", "Carrot", "Tulip", "Bush", "Cherry Tree", "Apple Tree", "Vine", "Rose" };
    public string[] descriptionList = new string[] {"Luscious", "Royal", "Draconic", "Carnivorous", "Frozen", "Preserved", "Lovely", "Fresh", "Tasty"};
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddPlant()
    {
        if (plantAmounts < plantCapacity)
        {
            plantName = descriptionList[Random.Range(0, descriptionList.Length)] + namesList[Random.Range(0,namesList.Length)];
            plantCost = Random.Range(1f, 10f);
            plantsList.Add(plantName);
            plantsListCost.Add(plantCost);
        }
        else
        {
            SellPlant();
        }
    }

    public void SellPlant()
    {
        selectedPlant = Random.Range(0, plantsList.Length);
        

        plantsList.RemoveAt(selectedPlant);
        funds += plantsListCost[selectedPlant];
        totalFunds += funds;
        plantsListCost.RemoveAt(selectedPlant);

    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainFunction : MonoBehaviour
{
    public List<string> plantsList = new List<string>();
    public List<float> plantsListCost = new List<float>();
    public int selectedPlant;
    public float funds;
    public float totalFunds;
    public float plantCost;
    public string plantName;
    public int plantCapacity = 5;
    public string[] namesList = new string[] { "Flower", "Cabbage", "Carrot", "Tulip", "Bush", "Cherry Tree", "Apple Tree", "Vine", "Rose" };
    public string[] descriptionList = new string[] {"Luscious", "Royal", "Draconic", "Carnivorous", "Frozen", "Preserved", "Lovely", "Fresh", "Tasty"};
    public int turnCountMax = 15;
    public int turnCount = 0;
    public List<int> plantAge = new List<int>();
    public float upgradeCost = 15;
    // Start is called before the first frame update
    void Start()
    {
        AddPlant();
    }

    // Update is called once per frame
    void Update()
    {
        AddPlant();
        if (turnCount >= turnCountMax)
        {
            selectedPlant = Random.Range(0, plantsList.Count);
            SellPlant();
            if (funds >= upgradeCost)
            {
                funds -= upgradeCost;
                upgradeCost += upgradeCost + Random.Range(15, 40);
                plantCapacity += 4;
                turnCountMax += 15;
            }
        }
    }
    public void AddPlant()
    {
        turnCount += 1;
        plantName = descriptionList[Random.Range(0, descriptionList.Length)] + " " + namesList[Random.Range(0,namesList.Length)];
        plantCost = Random.Range(1f, 10f);
        plantsList.Add(plantName);
        plantsListCost.Add(plantCost);
        plantAge.Add(0);
        if (plantsList.Count > plantCapacity) 
        {
            selectedPlant = Random.Range(0, plantsList.Count);
            SellPlant();
        }
        Debug.Log("Plants List: " + plantsList.Count);
        for (int i = 0; i < plantsList.Count; i++)
        {
            if (plantAge[i] >= 16)
            {
                plantsListCost[i] = 1;
                selectedPlant = i;
                SellPlant();
            }
            else
            {
                plantAge[i] += 1;
            }
        
        }

    }
    public void SellPlant()
    {
        Debug.Log("Selected Plant: " + selectedPlant);
        plantsList.RemoveAt(selectedPlant);
        funds += plantsListCost[selectedPlant];
        totalFunds += funds;
        plantsListCost.RemoveAt(selectedPlant);
        plantAge.RemoveAt(selectedPlant);
    }
}

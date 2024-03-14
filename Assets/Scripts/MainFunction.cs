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
    // Start is called before the first frame update
    void Start()
    {
        AddPlant();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AddPlant()
    {
        if (plantsList.Count < plantCapacity)
        {
            plantName = descriptionList[Random.Range(0, descriptionList.Length)] + " " + namesList[Random.Range(0,namesList.Length)];
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
        selectedPlant = Random.Range(0, plantsList.Count);
        

        plantsList.RemoveAt(selectedPlant);
        funds += plantsListCost[selectedPlant];
        totalFunds += funds;
        plantsListCost.RemoveAt(selectedPlant);

    }
}

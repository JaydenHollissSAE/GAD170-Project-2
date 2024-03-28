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
    public GameObject plant;
    public GameObject[] plantsObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
    public GameObject destroyBuffer;

    public void Start()
    {
        turnCount = 0;
        Debug.Log(plantsObjects.Length);
        plantsObjects[0] = plant;
        Debug.Log(plantsObjects.Length);
        AddPlant();
        //plant.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        //Debug.Log(turnCount);
        //Debug.Log(turnCountMax);
        plantsObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        //Debug.Log(plantsObjects.Length);
        if (turnCount >= turnCountMax)
        {
            if (plantsList.Count == 0)
            {
                Debug.Log("Game Over");
                Destroy(this.gameObject);
            }
            else {
                Debug.Log("Testa");
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
        else
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                AddPlant();
            }
        }
    }
    public void AddPlant()
    {
        Instantiate(plantsObjects[0], new Vector3(Random.Range(-6f, 6f), Random.Range(-6f, 6f), 0), Quaternion.identity);
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
        while (true)
        {
            destroyBuffer = plantsObjects[Random.Range(0, plantsObjects.Length)];
            if (destroyBuffer.name.Contains("plant"))
            {
                break;
            }
        }
        Destroy(destroyBuffer);
        Debug.Log("Selected Plant: " + selectedPlant);
        plantsList.RemoveAt(selectedPlant);
        funds += plantsListCost[selectedPlant];
        totalFunds += funds;
        plantsListCost.RemoveAt(selectedPlant);
        plantAge.RemoveAt(selectedPlant);
    }
}

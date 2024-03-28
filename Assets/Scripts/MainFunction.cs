using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainFunction : MonoBehaviour
{
    public List<string> plantsList;
    public List<float> plantsListCost;
    public int selectedPlant;
    public float funds;
    public float totalFunds;
    public float plantCost;
    public string plantName;
    public int plantCapacity = 5;
    public string[] namesList;
    public string[] descriptionList;
    public int turnCountMax = 15;
    public int turnCount = 0;
    public List<int> plantAge;
    public float upgradeCost = 15;
    public GameObject plant;
    public GameObject[] plantsObjects;
    public GameObject destroyBuffer;
    public Plants plantsScript;

    public void Start()
    {
        turnCount = 0;
        Debug.Log(plantsObjects.Length);
        plantsObjects[0] = plant;
        Debug.Log(plantsObjects.Length);
        plantsScript.AddPlant();
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
                plantsScript.SellPlant();
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
                plantsScript.AddPlant();
            }
        }
    }

}

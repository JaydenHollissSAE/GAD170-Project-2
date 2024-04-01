using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainFunction : MonoBehaviour
{
    public List<string> plantsList = new List<string>(); //Creates a list for the player's current plants
    public List<float> plantsListCost = new List<float>(); //Creates a list for the value of the player's current plants
    public int selectedPlant; //Creates a redefinable integer to be used to determine the plant selected by the code
    public float funds; //Creates a redefinable floating point value to store the funds held by the player
    public float totalFunds; //Creates a redefinable floating point value to store the total accumulated funds by the player
    public float plantCost; //Creates a redefinable floating point value to be used in the process of defining the value of a plant
    public string plantName; //Creates a redefinable string to be used in determining the name of a generated plant
    public int plantCapacity = 5; //Creates an integer that is used to define the amount of plants that can be held at once, by default set to 5
    public string[] namesList = new string[] { "Flower", "Cabbage", "Carrot", "Tulip", "Bush", "Cherry Tree", "Apple Tree", "Vine", "Rose" }; //Creates an array that has all the possible names used in the generating of a plant
    public string[] descriptionList = new string[] {"Luscious", "Royal", "Draconic", "Carnivorous", "Frozen", "Preserved", "Lovely", "Fresh", "Tasty"}; //Creates an array that has all the possible descriptions used in the generating of a plant
    public int turnCountMax = 15; //Creates an integer that is used to define the maximum amount of turns in a cycle, by default set to 15
    public int turnCount = 0; //Creates a redefinable integer to be used to determine the amount of turns that have passed, by default set to 0
    public List<int> plantAge = new List<int>(); //Creates a list for
    public float upgradeCost = 15; //Creates an integer that is used to define the amount of funds that are required to perform an upgrade, by default set to 15
    public GameObject plant; //Defines the plant game object for the code so it can be utilised in the code
    public GameObject[] plantsObjects = UnityEngine.Object.FindObjectsOfType<GameObject>(); //Creates a list for all the game objects within the scene
    public GameObject destroyBuffer; //Creates a redefinable game object to be used as a buffer when destroying a game object to ensure the wrong one is not destroyed

    public void Start()
        /// <summary>
        /// Begins the game
        /// </summary>
    {
        turnCount = 0; //Resets turn count to 0
        //Debug.Log(plantsObjects.Length); //Prints the length of the plantsObjects List for debugging
        plantsObjects[0] = plant; //Sets the first object in the objects list
        //Debug.Log(plantsObjects.Length); //Prints the length of the plantsObjects List for debugging
        AddPlant(); //Begins the sequence
        //plant.SetActive(false);
    }


    void Update()
        /// <summary>
        /// The main loop that continues to run every frame while it exists
        /// </summary>
    {
        //Debug.Log(turnCount); //Prints the turn count for debugging
        //Debug.Log(turnCountMax); //Prints the max turn count for debugging
        plantsObjects = UnityEngine.Object.FindObjectsOfType<GameObject>(); //Finds all the objects in the scene and adds them to the plantsObjects list
        //Debug.Log(plantsObjects.Length); //Prints the length of the plantsObjects List for debugging
        if (turnCount >= turnCountMax) //Checks if the turn count is above or equal to the max turn count
        {
            if (plantsList.Count == 0) //Checks if the amount of plants is 0
            {
                Debug.Log("Game Over"); //Prints that the game is over
                Debug.Log(totalFunds + " was raised overall"); //Prints the total amount of funds raised during the game
                Destroy(this.gameObject); //Destroys the object with the code to prevent further playing
            }
            else {
                //Debug.Log("Testa"); //Prints to check if the function is running
                selectedPlant = Random.Range(0, plantsList.Count); //Selects a random value that points to a plant in the plants lists
                SellPlant(); //Runs the SellPlant function to continue the sequence by removing a plant and increasing the player's funds
                if (funds >= upgradeCost) //Checks if the player's funds are larger or equal to the cost to upgrade
                {
                    funds -= upgradeCost; //Removes the cost to upgrade from the player's funds
                    upgradeCost += upgradeCost + Random.Range(15, 40); //Increases the cost to upgrade by itself and a random value from 15-40
                    plantCapacity += 4; //Increases the max amount of plants by 4
                    turnCountMax += 15; //Increases the max turn count by 15
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.A)) //Checks if the player is pressing the A key down on their keyboard
            {
                AddPlant(); //Runs the AddPlant function to add a plant and continue the sequence
            }
        }
    }
    public void AddPlant()         
        /// <summary>
        /// Runs the sequence for creating a new plant and adding it player's plants
        /// </summary>
    {
        Instantiate(plantsObjects[0], new Vector3(Random.Range(-6f, 6f), Random.Range(-6f, 6f), 0), Quaternion.identity); //Creates a new plant object in a random location adjacent to the first object in the objects list
        turnCount += 1; //Increases the turn count by 1
        plantName = descriptionList[Random.Range(0, descriptionList.Length)] + " " + namesList[Random.Range(0,namesList.Length)]; //Creates a new plant name by taking a random string each from the descriptionList array and the namesList array, merging them with a space between them
        plantCost = Random.Range(1f, 10f); //Creates a random cost for the generated plant from 1.0-10.0
        plantsList.Add(plantName); //Adds the plant's name to the plantsList list to store it as one of the player's plants
        plantsListCost.Add(plantCost); //Adds the plants cost to the plantsListCost list to store it as the cost of the plant in plantsList that has the same value as it
        plantAge.Add(0); //Adds the value of 0 in the plantAge list in the same position of the generated plant to store it as the age of the plant in plantsList that shares its position
        //Debug.Log("Plants List: " + plantsList.Count); //Prints the amount of plants in the plantsList for debugging
        for (int i = 0; i < plantsList.Count; i++) //Loops the contained code for every plant held within plantsList
        {
            if (plantAge[i] >= 16) //Checks if the selected plant's age is above or equal to 16
            {
                plantsListCost[i] = 1; //Sets the selected plant's cost to 1
                selectedPlant = i; //Defines selectedPlant as the selected plant value (i)
                SellPlant(); //Runs the SellPlant function to continue the sequence by removing a plant and increasing the player's funds
            }
            else
            {
                plantAge[i] += 1; //Increases the age of the selected plant by 1
            }   
        }
        if (plantsList.Count > plantCapacity) //Checks if the amount of plants in plantsList is above the maximum amount of plants defined by plantCapacity
        {
            selectedPlant = Random.Range(0, plantsList.Count); //Selects a random value that points to a plant in the plants lists
            SellPlant(); //Runs the SellPlant function to continue the sequence by removing a plant and increasing the player's funds
        }
    }
    public void SellPlant()
        /// <summary>
        /// Selects and removes a plant from the game and increases the player's funds
        /// </summary>
    {
        while (true) //Performs a loop that continues until it is canceled
        {
            destroyBuffer = plantsObjects[Random.Range(0, plantsObjects.Length)]; //Sets destroyBuffer as a random object from the scene
            if (destroyBuffer.name.Contains("plant")) //Checks if the selected object in destroyBuffer contains the word plant to ensure it doesn't remove the main game object that has the game's code
            {
                break; //Cancels the while loop
            }
        }
        Destroy(destroyBuffer); //Destroys the selected object by destroyBuffer in the scene
        //Debug.Log("Selected Plant: " + selectedPlant); //Prints the value of selectedPlant for debugging
        plantsList.RemoveAt(selectedPlant); //Removes the item from plantsList at the position of selectedPlant to remove it from play
        funds += plantsListCost[selectedPlant]; //Adds the cost of the selected plant from plantsListCost to the player's funds
        totalFunds += plantsListCost[selectedPlant]; //Adds the cost of the selected plant from plantsListCost to the player's total funds
        plantsListCost.RemoveAt(selectedPlant); //Removes the item from plantsListCost at the position of selectedPlant to remove it from play
        plantAge.RemoveAt(selectedPlant); //Removes the item from plantAge at the position of selectedPlant to remove it from play
    }
}

using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;

public class Recipemanager : MonoBehaviour
{

    public event EventHandler OnRecipeSpawn;
    public event EventHandler OnRecipeComplete;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;

    public static Recipemanager Instance { get; private set; }

    [SerializeField]
    private RecipeListSO recipeListSO;

    private List<RecipeSO> waitingRecipeSOList;

    private float recipeSpawnTimer;
    private float recipeSpawnTimeMax = 5f;
    private int recipeMaxCount = 3;
    private int recipeSuccess = 0;


    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }


    private void Update()
    {
        recipeSpawnTimer -= Time.deltaTime;
        if (recipeSpawnTimer <= 0f)
        {
            recipeSpawnTimer = recipeSpawnTimeMax;

            if (GameHandler.Instance.isGamePlaying() && waitingRecipeSOList.Count < recipeMaxCount)
            {
                RecipeSO waitingRecipeSo = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
                Debug.Log(waitingRecipeSo.recipeName);
                waitingRecipeSOList.Add(waitingRecipeSo);
                OnRecipeSpawn?.Invoke(this, EventArgs.Empty);
            }
        }


    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectsSOList().Count)
            {
                //Has same num of ingrediants
                bool plateContentMatchesRecipe = true;
                foreach (KitchenObjectsSO kitchenObjectsSO in waitingRecipeSO.kitchenObjectSOList) 
                {
                    bool found = false;
                    foreach (KitchenObjectsSO plateObjectsSO in plateKitchenObject.GetKitchenObjectsSOList())
                    {

                        Debug.Log(plateObjectsSO.name);

                        if (plateObjectsSO == kitchenObjectsSO)
                        {
                            //Match

                            found = true;
                            break;
                        }



                    }


                    if (!found)
                    {
                        //Recipe not found in plate
                        plateContentMatchesRecipe = false;

                    }

                    

                }
                if (plateContentMatchesRecipe == true)
                {
                    //Delivered correct recipe
                    Debug.Log("Delivered correct recipe");
                    waitingRecipeSOList.RemoveAt(i);
                    recipeSuccess++;
                    OnRecipeComplete?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }

        }
        //No matches found
        Debug.Log("Failed");
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);

    }

    public List<RecipeSO> GetRecipeSOList() { 
        return waitingRecipeSOList;
    }

    public int GetRecipeSucessCount() 
    {
        return recipeSuccess;
    }
}

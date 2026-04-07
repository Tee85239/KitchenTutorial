using System;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;
    [SerializeField]
    KitchenObjectsSO plateKitchenObjectSO;
    private int plateSpawnAmount;
    private int plateSpawnAmountMax = 4;
    public event EventHandler OnPlateSpawn;
    public event EventHandler OnPlateRemove;

    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if(spawnPlateTimer > spawnPlateTimerMax)
        {
            spawnPlateTimer = 0f;
            // KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, this);

            if (GameHandler.Instance.isGamePlaying() && plateSpawnAmount < plateSpawnAmountMax)
            {
                plateSpawnAmount++;
                OnPlateSpawn?.Invoke(this, EventArgs.Empty);
            }

        }
        
         
        
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            //Empty handed
            if(plateSpawnAmount > 0)
            {
                plateSpawnAmount--;

                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                OnPlateRemove?.Invoke(this, EventArgs.Empty);

            }

        }
    }


}

using UnityEngine;
using System;

public class CuttingCounter : BaseCounter, IProgressBar
{
    public static event EventHandler onAnyCut;
   new public static void ResetStaticData()
    {
        onAnyCut = null;

    }
    public event EventHandler<IProgressBar.OnProgressChangedEventArgs> onProgressChange;
  

    [SerializeField]
    private CuttingRecipeSO[] cutKitchenObjectSOArray;
    private int cutProgress;
    public override void Interact(Player player)
    {

        if (!HasKitchenObject())
        {
            //Has no kitchen object
            if (player.HasKitchenObject())
            {
                //Has Object
                if (HasRecipewithInput(player.GetKitchenObject().GetKitchenObjectSO())) 
                {
                    //Has valid item
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cutProgress = 0;
                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    onProgressChange?.Invoke(this, new IProgressBar.OnProgressChangedEventArgs() {
                        progressNormalized = (float)cutProgress / cuttingRecipeSO.cutProgressMax
                    });
                }
            }
            else
            {
                //player has nothing
            }

        }
        else
        {
            //Has Kitchen object
            if (player.HasKitchenObject())
            {
                //Player has object
                
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //Player has plate


                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }

                }
                else 
                { 
                    //Player not have plate
                    if(GetKitchenObject().TryGetPlate(out plateKitchenObject) )
                    {
                        //Counter holding plate
                       if(plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }

        }
    }

    public override void InteractAlt(Player player)
    {
        if (HasKitchenObject() && HasRecipewithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            //There is kitchen object
            cutProgress++;
            onAnyCut?.Invoke(this, EventArgs.Empty);
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
            onProgressChange?.Invoke(this, new IProgressBar.OnProgressChangedEventArgs()
            {
                progressNormalized = (float)cutProgress / cuttingRecipeSO.cutProgressMax
            });
        

        if (cutProgress >= cuttingRecipeSO.cutProgressMax)
            {
                KitchenObjectsSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());

                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
        }
    }

    private KitchenObjectsSO GetOutputForInput(KitchenObjectsSO inputKitchenObject)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObject);
        if(cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.output;
        }
        else
        {
            return null;
        }
           
    }

    private bool HasRecipewithInput(KitchenObjectsSO kitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(kitchenObjectSO);
        return cuttingRecipeSO != null;
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectsSO inputKitchenObject)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cutKitchenObjectSOArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObject)
            {
                return cuttingRecipeSO;
            }
        }
        return null;
    }
}

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClearCounter : BaseCounter
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private KitchenObjectsSO kitchenObjectSO;
    
  

    
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //Has no kitchen object
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                //player has nothing
            }

        }
        else {
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
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        //Counter holding plate
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
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

   
}

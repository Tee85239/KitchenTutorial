using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField]
    private KitchenObjectsSO kitchenObjectSO;

    public EventHandler onPlayerGrabObject;
   

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            //Player not carrying
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
            onPlayerGrabObject?.Invoke(this, EventArgs.Empty);
        }


    }

   

}

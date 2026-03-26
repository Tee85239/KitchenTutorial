using UnityEngine;

public class TrashCounter : BaseCounter
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObject().DestroySelf();
        }
    }


}

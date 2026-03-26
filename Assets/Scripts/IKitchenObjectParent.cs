using UnityEngine;

public interface IKitchenObjectParent
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform GetKitchenFollowTransform();


    public void SetKitchenObject(KitchenObject kitchenObject);

    public KitchenObject GetKitchenObject();


    public void ClearKitchenObject();


    public bool HasKitchenObject();
   
}

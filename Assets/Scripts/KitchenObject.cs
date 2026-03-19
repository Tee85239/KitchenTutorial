using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField]
    private KitchenObjectsSO kitchenObjectSO;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private ClearCounter clearCounter;
  public KitchenObjectsSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    public void SetClearCounter(ClearCounter clearCounter)
    {
        if(this.clearCounter != null)
        {
            this.clearCounter.ClearKitchenObject();
        }
        this.clearCounter = clearCounter;
        if (clearCounter.HasKitchenObject()) {
            Debug.LogError("Counter already has kitchen object");
        }
        clearCounter.SetKitchenObject(this);
        transform.parent = clearCounter.GetKitchenFollowTransform();
        transform.localPosition = Vector3.zero; 
    }

    public ClearCounter GetClearCounter() 
    {
        return clearCounter;
    }
}

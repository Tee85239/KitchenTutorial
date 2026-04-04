using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    private Transform counterTopPoint;
    private KitchenObject kitchenObject;
    public static event EventHandler OnAnyObjectPlaced;

    public virtual void Interact(Player player)
    {
        Debug.LogError("BaseCounter.Interact");
    }

    public virtual void InteractAlt(Player player)
    {
        Debug.LogError("BaseCounter.InteractAlt");
    }
    public Transform GetKitchenFollowTransform()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null) { 
        OnAnyObjectPlaced?.Invoke(this, EventArgs.Empty);
        }
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }


}

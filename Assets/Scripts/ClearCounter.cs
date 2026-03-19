using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClearCounter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private KitchenObjectsSO kitchenObjectSO;
    [SerializeField]
    private Transform counterTopPoint;
    private KitchenObject kitchenObject;
    [SerializeField]
    private ClearCounter secondClearCounter;

    [SerializeField]
    private bool test;

    private void Update()
    {
        if (test && Input.GetKeyDown(KeyCode.T))
        {
            if(kitchenObject != null)
            {
                kitchenObject.SetClearCounter(secondClearCounter);
               // Debug.Log(kitchenObject.GetClearCounter());
            }
        }
    }
    public void Interact()
    {
        if (kitchenObject == null)
        {

            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetClearCounter(this);
           // kitchenObjectTransform.localPosition = Vector3.zero;
           // kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
           // kitchenObject.SetClearCounter(this);

        }
        else {
            Debug.Log(kitchenObject.GetClearCounter());
        }

       
    }

    public Transform GetKitchenFollowTransform()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject (KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
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

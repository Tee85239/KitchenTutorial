using UnityEngine;

public class PlateIconUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private PlateKitchenObject plateKitchenObject;
    [SerializeField] 
    private Transform iconTemplate;
   


    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }
    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
        UpdateVisual();
        
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        UpdateVisual();
        Debug.Log("Spawned");
       
    }


    private void UpdateVisual()
    {

        foreach (Transform child in transform)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);

        }



        foreach (KitchenObjectsSO kitchenObjectsSO in plateKitchenObject.GetKitchenObjectsSOList())
        {
            

            Transform iconTransform = Instantiate(iconTemplate, transform);

             iconTransform.GetComponent<PlateIconSingleUI>().SetKitchenObjectSO(kitchenObjectsSO);
           
            iconTransform.gameObject.SetActive(true);

        }
        
    }
}

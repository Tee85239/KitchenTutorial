using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private TextMeshProUGUI recipeName;
    [SerializeField]
    private Transform iconContainer;
    [SerializeField]
    private Transform iconTemplate;



    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }
    public void SetRecipeSO(RecipeSO recipeSO)
    {
        recipeName.text = recipeSO.recipeName;

        foreach (Transform child in iconContainer)
        {
            if (child == iconTemplate) continue;
            
                
                Destroy(child.gameObject);
            


        }

        foreach(KitchenObjectsSO kitchenObjectsSO in recipeSO.kitchenObjectSOList)
        {
           Transform iconTransform = Instantiate(iconTemplate, iconContainer);
            iconTransform.gameObject.SetActive(true);   
            iconTransform.GetComponent<Image>().sprite = kitchenObjectsSO.sprite;
        }
    }

}

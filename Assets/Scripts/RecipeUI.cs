using UnityEngine;

public class RecipeUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private Transform container;
    [SerializeField]
    private Transform template;

    private void Awake()
    {
        template.gameObject.SetActive(false);
    }


    private void Start()
    {
        Recipemanager.Instance.OnRecipeComplete += Instance_OnRecipeComplete;
        Recipemanager.Instance.OnRecipeSpawn += Instance_OnRecipeSpawn;

        UpdateVisuals();
    }

    private void Instance_OnRecipeSpawn(object sender, System.EventArgs e)
    {
        UpdateVisuals();
    }

    private void Instance_OnRecipeComplete(object sender, System.EventArgs e)
    {
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        foreach(Transform child in container)
        {
            if (child == template) continue;
            Destroy(child.gameObject);
        }


       foreach(RecipeSO recipeSO in Recipemanager.Instance.GetRecipeSOList())
        {
            Transform recipeTransform = Instantiate(template, container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSO);

        }
    }

    
    
        
    
}

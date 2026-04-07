using UnityEngine;
using UnityEngine.UI;

public class DeliverySucessUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private Image backroundImage;
    [SerializeField]
    private Image Icon;
    [SerializeField]
    private Color sucessColor;
    [SerializeField]
    private Color failColor;
    [SerializeField]
    private Sprite SucessSprite;
    [SerializeField]
    private Sprite FailSprite;

    private Animator animator;

    private const string POPUP = "Popup";



    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private  void Start()
    {
        Recipemanager.Instance.OnRecipeComplete += RecipeManger_OnRecipeComplete;
        Recipemanager.Instance.OnRecipeFailed += RecipeManager_OnRecipeFailed;

        gameObject.SetActive(false);
        
    }

    private void RecipeManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        animator.SetTrigger(POPUP);
        backroundImage.color = failColor;
        Icon.sprite = FailSprite;
        gameObject.SetActive(true);
    }

    private void RecipeManger_OnRecipeComplete(object sender, System.EventArgs e)
    {
        animator?.SetTrigger(POPUP);
        backroundImage.color = sucessColor;
        Icon.sprite = SucessSprite;
        gameObject.SetActive(true);
    }

    // Update is called once per frame
   
}

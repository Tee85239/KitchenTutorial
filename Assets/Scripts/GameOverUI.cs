using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private TextMeshProUGUI recipiesDeliveredText;

    private void Start()
    {
        GameHandler.Instance.onStateChange += GameManager_onStateChange;
        Hide();

    }

    private void GameManager_onStateChange(object sender, System.EventArgs e)
    {
        if (GameHandler.Instance.IsGameOver())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    // Update is called once per frame
    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
       recipiesDeliveredText.text = Recipemanager.Instance.GetRecipeSucessCount().ToString();
    }



}

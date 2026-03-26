using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{


    [SerializeField]
   private Image barImage;
    [SerializeField]
    private GameObject hasProgressGameObject;
    private IProgressBar hasProgress;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
   private void Start()
    {
        hasProgress = hasProgressGameObject.GetComponent<IProgressBar>();
        if (hasProgress == null) {
            Debug.LogError(hasProgressGameObject + "Does not have IProgressBar ");
        }
        hasProgress.onProgressChange += HasProgress_onProgressChange;
        barImage.fillAmount = 0;

        Hide();
    }

    private void HasProgress_onProgressChange(object sender, IProgressBar.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized;

        if (e.progressNormalized == 0f || e.progressNormalized == 1f) { 
        Hide();
        }
        else
        {
            Show();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }


}

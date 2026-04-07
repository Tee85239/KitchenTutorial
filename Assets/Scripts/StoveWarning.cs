using UnityEngine;

public class StoveWarning : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private StoveCounter stoveCounter;


    private void Start()
    {
        stoveCounter.onProgressChange += StoveCounter_onProgressChange;

        Hide();
    }

    private void StoveCounter_onProgressChange(object sender, IProgressBar.OnProgressChangedEventArgs e)
    {
        float burnShowProgressAmount = 0.5f;
        bool show = stoveCounter.ISFried() && e.progressNormalized >= burnShowProgressAmount;

        if (show) {
            Show();
        }
        else
        {
            Hide();
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

using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    [SerializeField]
    private GameObject stove;
    [SerializeField]
    private GameObject particles;
    [SerializeField]
    StoveCounter stoveCounter;

    private void Start()
    {
        stoveCounter.OnStateChange += StoveCounter_OnStateChange;
    }

    private void StoveCounter_OnStateChange(object sender, StoveCounter.OnStateChangeEventArgs e)
    {
        bool showVisual = e.state == StoveCounter.States.Frying || e.state == StoveCounter.States.Fried;
        stove.gameObject.SetActive(showVisual);
        particles.SetActive(showVisual);    
    }
}

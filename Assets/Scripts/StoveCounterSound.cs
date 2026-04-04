using UnityEngine;

public class StoveCounterSound : MonoBehaviour

{

    private AudioSource audioSource;
    [SerializeField]
    private StoveCounter stoveCounter;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        stoveCounter.OnStateChange += StoveCounter_OnStateChange;
    }

    private void StoveCounter_OnStateChange(object sender, StoveCounter.OnStateChangeEventArgs e)
    {
        bool playSound = e.state == StoveCounter.States.Frying || e.state == StoveCounter.States.Fried;
        if (playSound) {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }
}

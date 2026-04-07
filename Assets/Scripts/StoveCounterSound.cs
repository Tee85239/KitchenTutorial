using UnityEngine;

public class StoveCounterSound : MonoBehaviour

{

    private AudioSource audioSource;
    [SerializeField]
    private StoveCounter stoveCounter;

    private float warningSoundTimer;
    private bool playWarningSound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        stoveCounter.OnStateChange += StoveCounter_OnStateChange;
        stoveCounter.onProgressChange += StoveCounter_onProgressChange;
    }

    private void StoveCounter_onProgressChange(object sender, IProgressBar.OnProgressChangedEventArgs e)
    {


        float burnShowProgressAmount = 0.5f;
         playWarningSound = stoveCounter.ISFried() && e.progressNormalized >= burnShowProgressAmount;
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


    private void Update()
    {
        if (playWarningSound)
        {
            warningSoundTimer -= Time.deltaTime;
            if (warningSoundTimer <= 0)
            {
                float warningSoundTimerMax = .5f;
                warningSoundTimer = warningSoundTimerMax;

                SoundManager.Instance.PlayWarningSound(stoveCounter.transform.position);
            }
        }
    }
}

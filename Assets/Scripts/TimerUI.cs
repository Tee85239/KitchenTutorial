using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private Image clock;


    // Update is called once per frame
    private void Update()
    {
        clock.fillAmount = GameHandler.Instance.GetPlayTime();
    }
}

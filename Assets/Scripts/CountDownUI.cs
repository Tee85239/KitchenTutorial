using TMPro;
using UnityEditor.Search;
using UnityEngine;

public class CountDownUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    TextMeshProUGUI CountDownText;
    private int prevCountdownNum;
    private const string NUMBER_POP_UP = "NumberPopup";

    private Animator animator;
    private void Start()
    {
        GameHandler.Instance.onStateChange += GameManager_onStateChange;
        Hide();
        
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void GameManager_onStateChange(object sender, System.EventArgs e)
    {
        if (GameHandler.Instance.isCountdownActive())
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
        int countdownNum = Mathf.CeilToInt(GameHandler.Instance.GetCountDownTimer());
       CountDownText.text = countdownNum.ToString();

        if(prevCountdownNum != countdownNum)
        {
            prevCountdownNum = countdownNum;
            animator.SetTrigger(NUMBER_POP_UP);
            SoundManager.Instance.PlayCountDown();
        }
        
    }
}

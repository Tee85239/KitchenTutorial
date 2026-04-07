using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private TextMeshProUGUI keyMoveUpText;
    [SerializeField]
    private TextMeshProUGUI keyMoveDownText;
    [SerializeField]
    private TextMeshProUGUI keyMoveLeftText;
    [SerializeField]
    private TextMeshProUGUI keyMoveRightText;
    [SerializeField]
    private TextMeshProUGUI keyInteractText;
    [SerializeField]
    private TextMeshProUGUI keyInteractAltText;
    [SerializeField]
    private TextMeshProUGUI keyPauseText;

    [SerializeField]
    private TextMeshProUGUI gamePadInteract;
    [SerializeField]
    private TextMeshProUGUI gamePadInteractAlt;
    [SerializeField]
    private TextMeshProUGUI gamePadPause;


    private void UpdateVisuals()
    {
        keyMoveUpText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.MoveUp);
        keyMoveDownText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.MoveDown);
        keyMoveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.MoveLeft);
        keyMoveRightText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.MoveRight);
        keyInteractText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.Interact);
        keyInteractAltText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.InteractAlt);
        keyPauseText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.Pause);
        gamePadInteract.text = GameInput.Instance.GetBindingText(GameInput.Bindings.GamePadInteract);
        gamePadInteractAlt.text = GameInput.Instance.GetBindingText(GameInput.Bindings.GamePadInteractAlt);
        gamePadPause.text = GameInput.Instance.GetBindingText(GameInput.Bindings.GamePadPause);

    }
    private void Start()
    {
        UpdateVisuals();
        GameInput.Instance.OnBindingRebind += GameInput_OnBindingRebind;
        GameHandler.Instance.onStateChange += GameHandler_onStateChange;
        Show();
    }

    private void GameHandler_onStateChange(object sender, System.EventArgs e)
    {
        if (GameHandler.Instance.isCountdownActive())
        {
            Hide();
            Debug.Log("Is hidden");
        }
    }

    private void GameInput_OnBindingRebind(object sender, System.EventArgs e)
    {
        UpdateVisuals();
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

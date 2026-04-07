using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private Button soundEffectsButton;
    [SerializeField]
    private Button musicButton;
    //[SerializeField] private Button  OptionsButton;
    [SerializeField]
    private TextMeshProUGUI soundEffectsText;
    [SerializeField]
    private TextMeshProUGUI musicText;
    [SerializeField]
    private Button ReturnButton;

    public static OptionsUI Instance { get; private set; }

    [SerializeField]
    private TextMeshProUGUI moveUpText;
    [SerializeField]
    private TextMeshProUGUI moveDownText;
    [SerializeField]
    private TextMeshProUGUI moveLeftText;
    [SerializeField]
    private TextMeshProUGUI moveRightText;
    [SerializeField]
    private TextMeshProUGUI interactText;
    [SerializeField]
    private TextMeshProUGUI interactAltText;
    [SerializeField]
    private TextMeshProUGUI pauseText;
    [SerializeField]
    private Button moveUpButton;
    [SerializeField]
    private Button moveDownButton;
    [SerializeField]
    private Button moveLeftButton;
    [SerializeField]
    private Button moveRightButton;
    [SerializeField]
    private Button interactButton;
    [SerializeField]
    private Button interactAltButton;
    [SerializeField]
    private Button pauseButton;
    [SerializeField]
    private Transform PressAnyKey;
    [SerializeField]
    private Button moveUpButtonGamePad;
    [SerializeField]
    private Button moveDownButtonGamePad;
    [SerializeField]
    private Button moveLeftButtonGamePad;
    [SerializeField]
    private Button moveRightButtonGamePad;
    [SerializeField]
    private Button interactButtonGamePad;
    [SerializeField]
    private Button interactAltButtonGamePad;
    [SerializeField]
    private Button pauseButtonGamepad;
    [SerializeField]
    private Transform PressAnyKeyGamePad;

    [SerializeField]
    private TextMeshProUGUI moveUpTextGamePad;
    [SerializeField]
    private TextMeshProUGUI moveDownTextGamePad;
    [SerializeField]
    private TextMeshProUGUI moveLeftTextGamePad;
    [SerializeField]
    private TextMeshProUGUI moveRightTextGamePad;
    [SerializeField]
    private TextMeshProUGUI interactTextGamePad;
    [SerializeField]
    private TextMeshProUGUI interactAltTextGamePad;
    [SerializeField]
    private TextMeshProUGUI pauseTextGamePad;

    private Action onClosedButtonAction;



    private void Start()
    {
        GameHandler.Instance.OnGameUnpaused += GameHandler_OnGameUnpaused;
        UpdateVisual();
        Hide();
        HideDirection();
    }

    private void GameHandler_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void Awake()
    {
        Instance = this;
        soundEffectsButton.onClick.AddListener(() => {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });

       musicButton.onClick.AddListener(() => {
           MusicManager.Instance.ChangeVolume();
           UpdateVisual();
        });

        ReturnButton.onClick.AddListener(() => { 
            Hide();
            onClosedButtonAction();
        });

        moveUpButton.onClick.AddListener(() =>
        {
           RebindLogic(GameInput.Bindings.MoveUp);
        });

        moveDownButton.onClick.AddListener(() =>
        {
            RebindLogic(GameInput.Bindings.MoveDown);
        });
        moveLeftButton.onClick.AddListener(() =>
        {
            RebindLogic(GameInput.Bindings.MoveLeft);
        });
        moveRightButton.onClick.AddListener(() =>
        {
            RebindLogic(GameInput.Bindings.MoveRight);
        });

        interactButton.onClick.AddListener(() =>
        {
            RebindLogic(GameInput.Bindings.Interact);
        });
        interactAltButton.onClick.AddListener(() =>
        {
            RebindLogic(GameInput.Bindings.InteractAlt);
        });
        pauseButton.onClick.AddListener(() =>
        {
            RebindLogic(GameInput.Bindings.Pause);
        });



        interactButtonGamePad.onClick.AddListener(() =>
        {
            RebindLogic(GameInput.Bindings.GamePadInteract);
        });
        interactAltButtonGamePad.onClick.AddListener(() =>
        {
            RebindLogic(GameInput.Bindings.GamePadInteractAlt);
        });
        pauseButtonGamepad.onClick.AddListener(() =>
        {
            RebindLogic(GameInput.Bindings.GamePadPause);
        });
    }

    private void UpdateVisual()
    {
        soundEffectsText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);

        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.MoveUp);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.MoveDown);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.MoveLeft);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.MoveRight);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.Interact);
        interactAltText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.InteractAlt);
        pauseText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.Pause);

        moveUpTextGamePad.text = GameInput.Instance.GetBindingText(GameInput.Bindings.GamePadUp);
        moveDownTextGamePad.text = GameInput.Instance.GetBindingText(GameInput.Bindings.GamePadDown);
        moveLeftTextGamePad.text = GameInput.Instance.GetBindingText(GameInput.Bindings.GamePadLeft);
        moveRightTextGamePad.text = GameInput.Instance.GetBindingText(GameInput.Bindings.GamePadRight);
        interactTextGamePad.text = GameInput.Instance.GetBindingText(GameInput.Bindings.GamePadInteract);
        interactAltTextGamePad.text = GameInput.Instance.GetBindingText(GameInput.Bindings.GamePadInteractAlt);
        pauseTextGamePad.text = GameInput.Instance.GetBindingText(GameInput.Bindings.GamePadPause);
    }

    public void Show(Action onClosedButtonAction)
    {
        this.onClosedButtonAction = onClosedButtonAction;
        gameObject.SetActive(true);
    
    }

    public void Hide() { 
    gameObject.SetActive(false);
    }

    private void ShowDirection()
    {
        PressAnyKey.gameObject.SetActive(true);
    }

    private void HideDirection()
    {
        PressAnyKey.gameObject.SetActive(false);
    }

    private void RebindLogic(GameInput.Bindings bindings)
    {
        ShowDirection();
        GameInput.Instance.RebindBinding(bindings, () =>
        {

            HideDirection();
            UpdateVisual();

        } );
    }

}

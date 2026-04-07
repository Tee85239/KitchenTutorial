using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System;

public class GameInput : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private PlayerInput playerInput;
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAltAction;
    public event EventHandler OnPause;
    public event EventHandler OnBindingRebind;
    private const string PlayerPrefBindings = "InputBindings";

    public static GameInput Instance { get; private set; }

    public enum Bindings
    {
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        Interact,
        InteractAlt,
        Pause,
        GamePadUp,
        GamePadDown,
        GamePadLeft,
        GamePadRight,
        GamePadInteract,
        GamePadInteractAlt,
        GamePadPause


    }

    private void Awake()
    {
        Instance = this;
        playerInput = new PlayerInput();
        playerInput.Player.Enable();

        if (PlayerPrefs.HasKey(PlayerPrefBindings))
        {

            playerInput.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PlayerPrefBindings));
        }
        playerInput.Player.Interact.performed += Interact_performed;
        playerInput.Player.InteractAlt.performed += InteractAlt_performed;
        playerInput.Player.Pause.performed += Pause_performed;

        
    }

    private void OnDestroy()
    {
        playerInput.Player.Interact.performed -= Interact_performed;
        playerInput.Player.InteractAlt.performed -= InteractAlt_performed;
        playerInput.Player.Pause.performed -= Pause_performed;

        playerInput.Dispose();

    }

    private void Pause_performed(InputAction.CallbackContext obj)
    {
        OnPause?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlt_performed(InputAction.CallbackContext obj)
    {
        OnInteractAltAction?.Invoke(this,EventArgs.Empty);
    }

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this,EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {

        
        //Vector2 inputVector = new Vector2(0, 0);
       Vector2 inputVector = playerInput.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;
        return inputVector;
    }

    public string GetBindingText(Bindings binding)
    {
        switch (binding) 
        {
            default:
            case Bindings.Interact:
               return playerInput.Player.Interact.bindings[0].ToDisplayString();
            case Bindings.InteractAlt:
               return playerInput.Player.InteractAlt.bindings[0].ToDisplayString();
            case Bindings.Pause:
                return playerInput.Player.Pause.bindings[0].ToDisplayString();
            case Bindings.MoveUp:
                return playerInput.Player.Move.bindings[1].ToDisplayString();
            case Bindings.MoveDown:
                return playerInput.Player.Move.bindings[2].ToDisplayString();
            case Bindings.MoveLeft:
                return playerInput.Player.Move.bindings[3].ToDisplayString();
            case Bindings.MoveRight:
                return playerInput.Player.Move.bindings[4].ToDisplayString();
            case Bindings.GamePadInteract:
                return playerInput.Player.Interact.bindings[1].ToDisplayString();
            case Bindings.GamePadInteractAlt:
                return playerInput.Player.Interact.bindings[1].ToDisplayString();
            case Bindings.GamePadPause:
                return playerInput.Player.Pause.bindings[1].ToDisplayString();
            case Bindings.GamePadUp:
                return playerInput.Player.Move.bindings[1].ToDisplayString();
            case Bindings.GamePadDown:
                return playerInput.Player.Move.bindings[1].ToDisplayString();
            case Bindings.GamePadLeft:
                return playerInput.Player.Move.bindings[1].ToDisplayString();
            case Bindings.GamePadRight:
                return playerInput.Player.Move.bindings[1].ToDisplayString();



        }
    }

    public void RebindBinding(Bindings binding, Action onActionRebound) {

        InputAction inputAction;
        int bindingIndex;
        
        switch (binding)
        {
            default:
            case Bindings.MoveUp:
                inputAction = playerInput.Player.Move;
                bindingIndex = 1;

                break;
            case Bindings.MoveDown:
                inputAction = playerInput.Player.Move;
                bindingIndex = 2;

                break;
            case Bindings.MoveLeft:
                inputAction = playerInput.Player.Move;
                bindingIndex = 3;

                break;
            case Bindings.MoveRight:
                inputAction = playerInput.Player.Move;
                bindingIndex = 4;

                break;
            case Bindings.Interact:
                inputAction = playerInput.Player.Interact;
                bindingIndex = 0;

                break;
            case Bindings.InteractAlt:
                inputAction = playerInput.Player.InteractAlt;
                bindingIndex = 0;

                break;
            case Bindings.Pause:
                inputAction = playerInput.Player.Pause;
                bindingIndex = 0;

                break;
            //----------------
            case Bindings.GamePadInteract:
                inputAction = playerInput.Player.Interact;
                bindingIndex = 1;

                break;
            case Bindings.GamePadInteractAlt:
                inputAction = playerInput.Player.InteractAlt;
                bindingIndex = 1;

                break;
            case Bindings.GamePadPause:
                inputAction = playerInput.Player.Pause;
                bindingIndex = 1;

                break;
        }


        playerInput.Player.Disable();

        inputAction.PerformInteractiveRebinding(bindingIndex).OnComplete(callback =>
        {
          
            callback.Dispose();

            playerInput.Player.Enable();
            onActionRebound();
          
            PlayerPrefs.SetString(PlayerPrefBindings, playerInput.SaveBindingOverridesAsJson());
            PlayerPrefs.Save();
            OnBindingRebind?.Invoke(this, EventArgs.Empty);
        }).Start();
        
    
    }
}

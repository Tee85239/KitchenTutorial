using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField]
    private float moveSpeed = 7f;
    private bool isWalking = false;
    [SerializeField]
    private GameInput input;
    private Vector3 lastInteractionDir;
    [SerializeField]
    private LayerMask counterLayerMask;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;
    [SerializeField]
    private Transform kitchenObjectHoldPoint;

    public event EventHandler<OnSelectedCounterChangeEventArgs> OnSelectedCounterChange;
    public event EventHandler objectGetSound;
    public class OnSelectedCounterChangeEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

   



    private void Start()
    {
        input.OnInteractAction += Input_OnInteractAction;
        input.OnInteractAltAction += Input_OnInteractAltAction;
    }

    private void Input_OnInteractAltAction(object sender, EventArgs e)
    {
        if (!GameHandler.Instance.isGamePlaying())
            return;
            if (selectedCounter != null)
        {
            selectedCounter.InteractAlt(this);
        }
    }

    public static Player Instance
    {
        get; private set;
    }
    private void Awake()
    {
        if (Instance != null) 
        
        {
            Debug.Log("Error Instance");
        }
        Instance = this;
    }

    private void Input_OnInteractAction(object sender, System.EventArgs e)
    {
        if(!GameHandler.Instance.isGamePlaying() )
            return;
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
       
    }

    private void Update()
    {
        HandleMovement();
        HandleInteractions();



    }

    private void HandleInteractions()
    {
        float interactionDistance = 2f;
        Vector2 inputVector = input.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y) * Time.deltaTime * moveSpeed;

        if(moveDir != Vector3.zero)
        {
            lastInteractionDir = moveDir;
        }
        if (Physics.Raycast(transform.position, lastInteractionDir, out RaycastHit raycastHit, interactionDistance, counterLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                //Has clear counter
                // clearCounter.Interact();
                if (baseCounter != selectedCounter)
                {
                    setSelectedCounter(baseCounter);
                }

            }
            else
            {
                setSelectedCounter(null);
            }
        }
        else 
        {
            setSelectedCounter(null);
        }
        
    }
    public bool IsWalking()
    {
        return isWalking;

    }

    private void HandleMovement()
    {

        Vector2 inputVector = input.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y) * Time.deltaTime * moveSpeed;
        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 0.2f;
        bool canMove =  !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);
        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f);
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z);
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                if (canMove)
                {
                    moveDir = moveDirZ;
                }
            }
        }
        if (canMove)
        {
            transform.position += moveDir;
        }
        isWalking = moveDir != Vector3.zero;
        float rotationSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
    }


    private void setSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChange?.Invoke(this, new OnSelectedCounterChangeEventArgs
        {
            selectedCounter = selectedCounter
        });
    }

    public Transform GetKitchenFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if(kitchenObject != null)
        {
            objectGetSound?.Invoke(this, EventArgs.Empty);
        }
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}

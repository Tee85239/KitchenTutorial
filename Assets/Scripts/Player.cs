using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 7f;
    private bool isWalking = false;
    [SerializeField]
    private GameInput input;
    private Vector3 lastInteractionDir;
    [SerializeField]
    private LayerMask counterLayerMask;
    private ClearCounter selectedCounter;

    public event EventHandler<OnSelectedCounterChangeEventArgs> OnSelectedCounterChange;
    public class OnSelectedCounterChangeEventArgs : EventArgs
    {
        public ClearCounter selectedCounter;
    }

   



    private void Start()
    {
        input.OnInteractAction += Input_OnInteractAction;
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

        if (selectedCounter != null)
        {
            selectedCounter.Interact();
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
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                //Has clear counter
                // clearCounter.Interact();
                if (clearCounter != selectedCounter)
                {
                    setSelectedCounter(clearCounter);
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
        Debug.Log(selectedCounter);
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
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);
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


    private void setSelectedCounter(ClearCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChange?.Invoke(this, new OnSelectedCounterChangeEventArgs
        {
            selectedCounter = selectedCounter
        });
    }
}

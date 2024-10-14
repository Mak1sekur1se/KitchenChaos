using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour,IKitchenObjectParent
{

    public static Player Instance{ get; private set; }

    public event EventHandler OnPickSomething;
    public event EventHandler<OnSelectCounterChange> onSelectCounterChange;
    public class OnSelectCounterChange : EventArgs
    {
        public BaseCounter selectCounter;
    }

    [SerializeField]
    private float moveSpeed = 7f;

    [SerializeField]
    private GameInput gameInput;

    [SerializeField]
    private LayerMask countersLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;

    private bool isWalking;
    private Vector3 lastInteracDir;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("this is more than one Instance");
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (!KitchenGameManager.Instance.isGamePlaying()) return;
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (!KitchenGameManager.Instance.isGamePlaying()) return;
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    // Update is called once per frame
    private void Update()
    {
       HandleMovement();
       HandleInteractions();
    }



    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleInteractions() {
        Vector2 inputVector = gameInput.GetMoveVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastInteracDir = moveDir;
        }

        float interactDistance = 2f;

        if (Physics.Raycast(transform.position, lastInteracDir, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                //Has ClearCounter
                if (baseCounter != selectedCounter)
                {

                    SetSelectCounter(baseCounter);
                }
            }
            else
            {
                SetSelectCounter(null);
            }
        }
        else
        {
            SetSelectCounter(null);
        }
        
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMoveVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 2f;

        //Debug.Log(moveDir);//0,0,1
        //No Collision and has xMove
        bool canMove =  !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);
        if (!canMove)
        {
            //No Collision or No Xmove
            //Can't Move towards moveDir

            //Attempt only X movement
            Vector3 moveDirx = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirx, moveDistance);

            if (canMove)
            {
                //Can move only Axis x

                moveDir = moveDirx;
            }
            else
            {
                Vector3 moveDirz = new Vector3(0, 0, moveDir.z).normalized;
                canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirz, moveDistance);

                if (canMove) { moveDir = moveDirz; }
            }

        }

        if (canMove)
            transform.position += moveDir * moveDistance;


        float rotateSpeed = 10f;
        isWalking = moveDir != Vector3.zero;
        if (moveDir == Vector3.zero)
            return;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    private void SetSelectCounter(BaseCounter selectCounter)
    {
        this.selectedCounter = selectCounter;
        onSelectCounterChange?.Invoke(this, new OnSelectCounterChange
        {
            selectCounter = selectedCounter
        });
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        //Player Receive KitchenObejct
        this.kitchenObject = kitchenObject;
        if (kitchenObject != null)
        {
            OnPickSomething?.Invoke(this, EventArgs.Empty);
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

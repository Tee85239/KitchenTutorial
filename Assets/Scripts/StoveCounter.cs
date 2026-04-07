using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter, IProgressBar
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private CookingRecipeSO[] cookingRecipeSOArray;
    [SerializeField]
    private BurntSO[] burntRecipeSOArray;
    private float fryingTimer;
    private CookingRecipeSO cookingRecipeSO;
    private float burningTimer;
    private BurntSO burntRecipeSO;

    public event EventHandler<OnStateChangeEventArgs> OnStateChange;
    public event EventHandler<IProgressBar.OnProgressChangedEventArgs> onProgressChange;
    public class OnStateChangeEventArgs : EventArgs
    {
        public States state;

    }
    public enum States
    {
        Idle,
        Frying,
        Fried,
        Burned
    }
    private States state;

    private void Start()
    {
        state = States.Idle;
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //Has no kitchen object
            if (player.HasKitchenObject())
            {
                //Has Object
                if (HasRecipewithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //Has valid item
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                     cookingRecipeSO = GetCookingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    state = States.Frying;
                    fryingTimer = 0f;

                    OnStateChange?.Invoke(this, new OnStateChangeEventArgs
                    {
                        state = state
                    });
                    onProgressChange?.Invoke(this, new IProgressBar.OnProgressChangedEventArgs
                    {
                        progressNormalized = fryingTimer / cookingRecipeSO.cookProgressMax
                    });
                }
            }
            else
            {
                //player has nothing
            }

        }
        else
        {
            //Has Kitchen object
            if (player.HasKitchenObject())
            {
                //Player has object
                //Player has object
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //Player has plate


                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                        state = States.Idle;

                        OnStateChange?.Invoke(this, new OnStateChangeEventArgs
                        {
                            state = state
                        });

                        onProgressChange?.Invoke(this, new IProgressBar.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });
                    }

                }


            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
                state = States.Idle;

                OnStateChange?.Invoke(this, new OnStateChangeEventArgs
                {
                    state = state
                });

                onProgressChange?.Invoke(this, new IProgressBar.OnProgressChangedEventArgs
                {
                    progressNormalized = 0f
                });
            }

        }
    }


    private KitchenObjectsSO GetOutputForInput(KitchenObjectsSO inputKitchenObject)
    {
        CookingRecipeSO cookingRecipeSO = GetCookingRecipeSOWithInput(inputKitchenObject);
        if (cookingRecipeSO != null)
        {
            return cookingRecipeSO.output;
        }
        else
        {
            return null;
        }

    }

    private bool HasRecipewithInput(KitchenObjectsSO kitchenObjectSO)
    {
        CookingRecipeSO cookingRecipeSO = GetCookingRecipeSOWithInput(kitchenObjectSO);
        return cookingRecipeSO != null;
    }

    private CookingRecipeSO GetCookingRecipeSOWithInput(KitchenObjectsSO inputKitchenObject)
    {
        foreach (CookingRecipeSO cookingRecipeSO in cookingRecipeSOArray)
        {
            if (cookingRecipeSO.input == inputKitchenObject)
            {
                return cookingRecipeSO;
            }
        }
        return null;
    }

    private BurntSO GetBurntRecipeSOWithInput(KitchenObjectsSO inputKitchenObject)
    {
        foreach (BurntSO burntRecipeSO in burntRecipeSOArray)
        {
            if (burntRecipeSO.input == inputKitchenObject)
            {
                return burntRecipeSO;
            }
        }
        return null;
    }


    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case States.Idle:

                    break;
                case States.Frying:
                    fryingTimer += Time.deltaTime;

                    onProgressChange?.Invoke(this, new IProgressBar.OnProgressChangedEventArgs
                    {
                        progressNormalized = fryingTimer / cookingRecipeSO.cookProgressMax
                    });

                    if (fryingTimer > cookingRecipeSO.cookProgressMax)
                    {
                        //finish frying
                        
                      
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(cookingRecipeSO.output, this);

                        
                       
                        state = States.Fried;

                        burningTimer = 0f;
                        burntRecipeSO = GetBurntRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                        OnStateChange?.Invoke(this, new OnStateChangeEventArgs
                        {
                            state = state
                        });
                       

                    }
                    break;
                case States.Fried:
                    burningTimer += Time.deltaTime;

                    onProgressChange?.Invoke(this, new IProgressBar.OnProgressChangedEventArgs
                    {
                        progressNormalized = burningTimer / burntRecipeSO.burnTime
                    });
                    if (burningTimer > burntRecipeSO.burnTime)
                    {
                        //finish frying


                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(burntRecipeSO.output, this);


                        Debug.Log("Burned");
                        state = States.Burned;

                        OnStateChange?.Invoke(this, new OnStateChangeEventArgs
                        {
                            state = state
                        });

                        onProgressChange?.Invoke(this, new IProgressBar.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });

                    }
                    break;
                case States.Burned:
                    break;
            }
           

        }

    }

    public bool ISFried()
    {
        return state == States.Fried;
    }

}

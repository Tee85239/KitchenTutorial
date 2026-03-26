using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;

public class BurgerVisual : MonoBehaviour
{

    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectsSO kitchenObjectsSO;
        public GameObject gameObject;

    }


    [SerializeField]
    PlateKitchenObject plateKitchenObject;
    [SerializeField]
    private List<KitchenObjectSO_GameObject> kitchenObjectSOGameObjectList;

    

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;

        foreach (KitchenObjectSO_GameObject kitchenObjectSO_GameObject in kitchenObjectSOGameObjectList)
        {
            kitchenObjectSO_GameObject.gameObject.SetActive(false);
            
        }
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        foreach (KitchenObjectSO_GameObject kitchenObjectSO_GameObject in kitchenObjectSOGameObjectList) {

            if (kitchenObjectSO_GameObject.kitchenObjectsSO == e.kitchenObjectsSO) {
                kitchenObjectSO_GameObject.gameObject.SetActive(true);
            }
        }
    }


}

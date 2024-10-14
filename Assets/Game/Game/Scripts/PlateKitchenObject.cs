using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{

    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }

    [SerializeField] private List<KitchenObjectSO> validKitchenObejctSoLists;

    private List<KitchenObjectSO> kitchenObjectSOLists;

    private void Awake()
    {
        kitchenObjectSOLists = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (!validKitchenObejctSoLists.Contains(kitchenObjectSO))
        {
            return false;
        }
        if (kitchenObjectSOLists.Contains(kitchenObjectSO))
        {
            //Already has this type
            return false;
        }
        kitchenObjectSOLists.Add(kitchenObjectSO);

        OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
        {
            kitchenObjectSO = kitchenObjectSO
        }) ;
        return true;
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return kitchenObjectSOLists;
    }
}

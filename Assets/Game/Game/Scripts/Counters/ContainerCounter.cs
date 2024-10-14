using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObejct;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;



    public override void Interact(Player player)
    {
        //only need pickup to Player
        if (!player.HasKitchenObject())
        {
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
            OnPlayerGrabbedObejct?.Invoke(this, EventArgs.Empty);
        }
    }
}

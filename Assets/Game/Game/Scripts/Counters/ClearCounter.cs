using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // no kitchenobejct
            if (player.HasKitchenObject())
            {
                //Player Carry Something
                player.GetKitchenObject().SetKitchenObejctParent(this);
            }
            else { 
            //Player Carrying Nothing 
            }

        }
        else
        {
            //has KitchenObejct
            if (player.HasKitchenObject())
            {
                //player carrying something
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //Holding plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
                else
                {
                    //Player not carry plate but something else
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject)){
                        //Counter is holding a plate
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }

                }
            }
            else {
                //Player have nothing
                GetKitchenObject().SetKitchenObejctParent(player);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent kitchenObjectParent;
    

    public KitchenObjectSO GetKitchenObjectSO() {
        return kitchenObjectSO;
    }

    public void SetKitchenObejctParent(IKitchenObjectParent kitchenObjectParant)
    {
        if (this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }
        this.kitchenObjectParent = kitchenObjectParant;
        if (kitchenObjectParant.HasKitchenObject())
        {
            Debug.LogError("Counter Has KitchenObject");
        }
            kitchenObjectParant.SetKitchenObject(this);

        transform.parent = kitchenObjectParant.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return kitchenObjectParent;
    }

    public void DestroySelf()
    {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if (this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else
        {
            plateKitchenObject = null;
            return false;
        }
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        Transform KitchenObejctTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObejctTransform.localPosition = Vector3.zero;//make sure Local
        KitchenObject kitchenObject = KitchenObejctTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObejctParent(kitchenObjectParent);
        return kitchenObject;
    }
}

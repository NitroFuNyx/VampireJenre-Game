using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour, IStoreListener
{
    private void Awake()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            
        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        throw new System.NotImplementedException();
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        throw new System.NotImplementedException();
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        throw new System.NotImplementedException();
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        throw new System.NotImplementedException();
    }
}

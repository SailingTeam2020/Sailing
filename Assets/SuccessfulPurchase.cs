using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessfulPurchase : MonoBehaviour
{
    [SerializeField] private GameObject ConfirmationSreenUI = null;
    [SerializeField] private GameObject SuccessfulPurchaseUI = null;
    [SerializeField] private GameObject PurchaseErrorScreenUI = null;

    public void OnClickSuccessfulPurchase()
    {
        ConfirmationSreenUI.SetActive(false);
        SuccessfulPurchaseUI.SetActive(false);
    }
    public void OnClickPurchaseError()
    {
        ConfirmationSreenUI.SetActive(false);
        SuccessfulPurchaseUI.SetActive(false);
        PurchaseErrorScreenUI.SetActive(false);

    }
}

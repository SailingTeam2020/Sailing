using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmationSreen : MonoBehaviour
{
    [SerializeField] private GameObject ConfirmationSreenUI = null;
    GameObject NameRegistrationUpdate;

    // Start is called before the first frame update
    public void Start()
    {
        NameRegistrationUpdate = GameObject.Find("PriceChangeObj");
    }

    public void OnClickConfirmation()
    {
        NameRegistrationUpdate.GetComponent<PriceChange>().ObjectNameRegistrationUpdate(this .name);
        ConfirmationSreenUI.SetActive(true);
    }
    public void OnClickBackButton()
    {
        ConfirmationSreenUI.SetActive(false);
    }
}

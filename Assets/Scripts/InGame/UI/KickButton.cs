using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickButton : MonoBehaviour
{

    [SerializeField]
    GameObject kickMenu;
    [SerializeField]
    GameObject roomOutButton;

    public void OnClick()
    {
        kickMenu.SetActive(true);
        roomOutButton.SetActive(false);
        this.gameObject.SetActive(false);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoButton : MonoBehaviour
{
    [SerializeField]
    GameObject kickMenu;
    [SerializeField]
    GameObject kickButton;
    [SerializeField]
    GameObject roomOutButton;

    public void OnClick()
    {
        kickButton.SetActive(true);
        roomOutButton.SetActive(true);
        kickMenu.SetActive(false);
    }
}

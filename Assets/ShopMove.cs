using Common;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sailing
{
    public class ShopMove : MonoBehaviour
    {

        [SerializeField]
        private Button shopButton;

        private void Start()
        {
            shopButton.interactable = false;
        }

        public void OnClick()
        {

            SceneManager.LoadScene("Shop");

        }

    }
}
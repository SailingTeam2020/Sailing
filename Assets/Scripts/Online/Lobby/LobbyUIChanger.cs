/*
 * 長嶋
 */

using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sailing.Online
{

    public class LobbyUIChanger : BaseNetworkObject
    {
        [SerializeField]
        private GameObject mainMenuUI;              //MenuをまとめたUI
        [SerializeField]
        private GameObject firstSubMenuUI;          //一番初めに表示されるUI
        [SerializeField]
        private Text connectStateText;              //通信状態を表示するテキスト

        private Stack<GameObject> subMenuStack;

        private void Awake()
        {

            subMenuStack = new Stack<GameObject>();

        }

        /// <summary>
        /// @brief UIの初期化を行う
        /// </summary>
        public void Init()
        {

            mainMenuUI.SetActive(true);
            firstSubMenuUI.SetActive(true);
            subMenuStack.Push(firstSubMenuUI);
            connectStateText.text = "オンラインモード";

        }

        /// <summary>
        /// @broef 次のUIに切り替える
        /// </summary>
        public void PuchNextUI(GameObject obj)
        {

            subMenuStack.Peek().SetActive(false);
            subMenuStack.Push(obj);
            subMenuStack.Peek().SetActive(true);

        }

        /// <summary>
        /// @broef 前のUIに切り替える
        /// </summary>
        public void PopPreviousUI()
        {

            subMenuStack.Peek().SetActive(false);
            subMenuStack.Pop();
            subMenuStack.Peek().SetActive(true);

        }

    }

}
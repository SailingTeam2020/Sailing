/*
 * 長嶋
 * 
 * Photonの機能で使いそうなものをまとめたクラスの予定
 * 
 */

using UnityEngine;
using Photon.Pun;
using Common;

namespace Sailing.Online
{

    public class BaseNetworkObject : MonoBehaviourPunCallbacks
    {

        /// <summary>
        /// @brief PhotonServerに接続する
        /// </summary>
        public bool Connected()
        {

            if (PhotonNetwork.IsConnected)
            {
                Debug.LogWarning("すでに接続済みです");
                return false;
            }

            return PhotonNetwork.ConnectUsingSettings();
        }

        /// <summary>
        /// @brief PhotonServerから切断する
        /// </summary>
        public void Disconnect()
        {

            if (!PhotonNetwork.IsConnected)
            {
                return;
            }

            PhotonNetwork.Disconnect();

        }

        public void SceneSwitch(string sceneName)
        {

            FadeManager.FadeOut(sceneName);

        }

    }

}
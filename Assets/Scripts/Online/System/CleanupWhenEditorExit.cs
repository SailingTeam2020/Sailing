/*
 * 
 * 長嶋
 * 
 * エディターの再生を終了したとき、Photonの接続を切るためのクラス
 * ゲームオブジェクトにアタッチすること
 * 
 */

using UnityEngine;
using Photon.Pun;

namespace Sailing.Online
{

    public class CleanupWhenEditorExit : BaseNetworkObject
    {

#if UNITY_EDITOR

        /// <summary>
        /// @brief エディターが終了した際、Photonに接続していたら切断する
        /// </summary>
        public void OnApplicationQuit()
        {

            //現在入室中なら退室する
            if (PhotonNetwork.InRoom)
            {
                PhotonNetwork.LeaveRoom();
                Debug.Log("部屋から退室しました");
            }

            Disconnect();

            Debug.Log("エディター再生を終了しました");

        }

#endif

    }

}
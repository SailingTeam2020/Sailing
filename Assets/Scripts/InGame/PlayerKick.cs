/*
 *作成者：小林凱
 *更新日：01/04
 *更新者：小林凱
 *概要　：プレイヤーを強制退出させる処理
 *      　ルームマスターのみがボタンを押すことができる
 *外部変数
 * kickMenu         ：確認画面オブジェクト
 * kickButton       ：確認画面のYesボタンオブジェクト
 * roomOutButton    ：退出ボタンオブジェクト
 * playerKickButton ：このスクリプトがアタッチされているオブジェクト
 */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
namespace Sailing.Online
{
    public class PlayerKick : MonoBehaviour
    {

        [SerializeField]
        GameObject kickMenu;
        [SerializeField]
        GameObject kickButton;
        [SerializeField]
        GameObject roomOutButton;

        Player[] kickPlayer = new Player[7];

        string Player;

        private void Awake()
        {
            Player = null;
            // ルームマスターじゃない場合はこのオブジェクトを非表示に
            if (!PhotonNetwork.IsMasterClient)
            {
                this.gameObject.SetActive(false);
            }
            // マッチングマネージャーのkickPlayerを参照し格納する
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                kickPlayer = FindObjectOfType<Sailing.Online.MatchingManager>().kickPlayer;
            }
        }

        /// <summary>
        /// ボタン押下時の処理。
        /// ルームマスターのみが操作可能。
        /// 押されたボタンのプレイヤーを強制退出させる。
        /// </summary>
        public void OnClick()
        {
            Debug.Log(kickPlayer[0]);
            // 退出させるプレイヤーは何Pかの情報を取得する
            Player = FindObjectOfType<KickButton>().returnPlayer();
            //Debug.Log(Player);

            kickButton.SetActive(true);
            roomOutButton.SetActive(true);
            kickMenu.SetActive(false);

            if (Player == "Player2")
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    if (kickPlayer[0] == null)
                    {
                        return;
                    }
                    PhotonNetwork.CloseConnection(kickPlayer[0]);
                    kickPlayer[0] = null;
                }
            }
            else if (Player == "Player3")
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    if (kickPlayer[1] == null)
                    {
                        return;
                    }
                    PhotonNetwork.CloseConnection(kickPlayer[1]);
                    kickPlayer[1] = null;
                }
            }
            else if (Player == "Player4")
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    if (kickPlayer[2] == null)
                    {
                        return;
                    }
                    PhotonNetwork.CloseConnection(kickPlayer[2]);
                    kickPlayer[2] = null;
                }
            }
            else if (Player == "Player5")
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    if (kickPlayer[3] == null)
                    {
                        return;
                    }
                    PhotonNetwork.CloseConnection(kickPlayer[3]);
                    kickPlayer[3] = null;
                }
            }
            else if (Player == "Player6")
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    if (kickPlayer[4] == null)
                    {
                        return;
                    }
                    PhotonNetwork.CloseConnection(kickPlayer[4]);
                    kickPlayer[4] = null;
                }
            }
            else if (Player == "Player7")
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    if (kickPlayer[5] == null)
                    {
                        return;
                    }
                    PhotonNetwork.CloseConnection(kickPlayer[5]);
                    kickPlayer[5] = null;
                }
            }
            else if (Player == "Player8")
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    if (kickPlayer[6] == null)
                    {
                        return;
                    }
                    PhotonNetwork.CloseConnection(kickPlayer[6]);
                    kickPlayer[6] = null;
                }
            }
            else
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        if (kickPlayer[i] == null)
                        {
                            return;
                        }
                        PhotonNetwork.CloseConnection(kickPlayer[i]);
                    }
                }
            }
            
            
        }

    }
}

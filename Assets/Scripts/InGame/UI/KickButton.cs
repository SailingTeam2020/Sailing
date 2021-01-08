/*
 *作成者：小林凱
 *更新日：01/04
 *更新者：小林凱
 *概要　：強制退出ボタンを押したときに起こる処理。
 *        不必要なUIを非表示にし、押したボタンがどのプレイヤーのものかを返す処理。
 *外部変数
 * kickMenu         ：確認画面オブジェクト
 * roomOutButton    ：退出ボタンオブジェクト
 * playerKickButton ：このスクリプトがアタッチされているオブジェクト
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;

public class KickButton : MonoBehaviour
{

    [SerializeField]
    GameObject kickMenu;
    [SerializeField]
    GameObject roomOutButton;
    [SerializeField]
    GameObject playerKickButton;

    public static string Player
    {
        get;
        private set;
    }

    private void Awake()
    {
        Player = null;
    }

    private void Start()
    {
        // ルームマスターじゃない場合はこのオブジェクトを非表示に
        if (!PhotonNetwork.IsMasterClient)
        {
            this.gameObject.SetActive(false);
        }
    }


    /// <summary>
    /// ボタン押下時の処理
    /// UIの表示設定と押されたボタンの判別情報いを返す
    /// </summary>
    public void OnClick()
    {
        //Debug.Log("(・。・)");
        kickMenu.SetActive(true);
        roomOutButton.SetActive(false);
        playerKickButton.SetActive(false);
        if (this.gameObject.tag == "Player2")
        {

            Player = "Player2";
            //Debug.Log(Player);
            //Debug.Log("Player2!!!!");
        }
        else if (this.gameObject.tag == "Player3")
        {
            Player = "Player3";
        }
        else if (this.gameObject.tag == "Player4")
        {
            Player = "Player4";
        }
        else if (this.gameObject.tag == "Player5")
        {
            Player = "Player5";
        }
        else if (this.gameObject.tag == "Player6")
        {
            Player = "Player6";
        }
        else if (this.gameObject.tag == "Player7")
        {
            Player = "Player7";
        }
        else if(this.gameObject.tag == "Player8")
        {
            Player = "Player8";
        }
        else
        {
            Player = "AllPlayer";
        }
    }

    /// <summary>
    /// 押されたボタンがどのプレイヤーのものか返す関数
    /// </summary>
    /// <returns>プレイヤー番号の情報</returns>
    public string returnPlayer()
    {
        return Player;
    }

}

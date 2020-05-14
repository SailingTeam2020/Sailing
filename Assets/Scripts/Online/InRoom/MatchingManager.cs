/*
 * 長嶋
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

    public class MatchingManager : BaseNetworkObject
    {

        [SerializeField]
        [Range(1, 8)]
        private byte startRequiredPlayerPeople = 2;
        [SerializeField]
        private Text playerCountText;
        [SerializeField]
        private Button gameStartButton;
        [SerializeField]
        private GameObject playerIconPanel;
        [SerializeField]
        private List<Sprite> playerIconSpriteList;
        [SerializeField]
        private CourseData courseData = null; // 読み込むコースデータ

        private const byte canStartPlayerCount = 2;

        private MatchingCountdown countTimer;
        private bool isLimitTimeOver;

        private void Awake()
        {

            //メッセージ処理の実行を再開する
            PhotonNetwork.IsMessageQueueRunning = true;

            if (!PhotonNetwork.IsConnected || !PhotonNetwork.InRoom)
            {
                Disconnect();
                SceneSwitch(SceneNameString.MainMenu);
                Debug.Log("ルーム内にいないためメニューに戻ります");
                return;
            }

            if (courseData == null) { 
                Debug.LogError(name + "にコースデータがアタッチされていません");
            }

            countTimer = GetComponent<MatchingCountdown>();
            isLimitTimeOver = false;

        }

        private void Start()
        {

            countTimer.Initialized();
            UpdateMatchingPlayer();

        }

        private void Update()
        {

            if (!PhotonNetwork.InRoom)
            {
                return;
            }

            if (countTimer.LeftCountTime <= 0.0f && !isLimitTimeOver)
            {
                isLimitTimeOver = true;
                countTimer.IsTimeCount = false;

                //ルーム内の人数がが指定数に満たない場合、部屋から退室する
                if (PhotonNetwork.CurrentRoom.PlayerCount < canStartPlayerCount)
                {
                    PhotonNetwork.LeaveRoom();
                }

                //自分が親の場合、ゲームシーンに移動するRPCを起動する
                if (PhotonNetwork.IsMasterClient)
                {
                    ReadyToGame();
                }

                return;
            }

        }

        /// <summary>
        /// @brief ルームの情報を更新し、それをGUIに適用させる
        /// </summary>
        private void UpdateMatchingPlayer()
        {

            //パネルの状態を更新する
            for (int count = 0; count < PhotonNetwork.CurrentRoom.MaxPlayers; count++)
            {
                Image image = playerIconPanel.transform.Find("PlayerIcon" + count).transform.Find("IconImage").GetComponent<Image>();
                Text name = playerIconPanel.transform.Find("PlayerIcon" + count).transform.Find("NameText").GetComponent<Text>();

                //プレイヤーが存在する場合は画像の変更とニックネームを設定する
                if (count < PhotonNetwork.CurrentRoom.PlayerCount)
                {
                    image.sprite = playerIconSpriteList[count];
                    name.text = PhotonNetwork.PlayerList[count].NickName;
                }
                else
                {
                    image.sprite = playerIconSpriteList[playerIconSpriteList.Count - 1];
                    name.text = "募集中";
                }


            }

            //プレイヤーの人数を確認し一定数以上いる且自身がマスターならばスタートボタンを押せるようにする
            if (PhotonNetwork.CurrentRoom.PlayerCount >= startRequiredPlayerPeople)
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    gameStartButton.interactable = true;
                }
            }
            else
            {
                gameStartButton.interactable = false;
            }

            //人数のテキストを更新する
            playerCountText.text = "人数 : " + PhotonNetwork.CurrentRoom.PlayerCount + " / " + PhotonNetwork.CurrentRoom.MaxPlayers;

        }

        /// <summary>
        /// @brief ルーム内にいる全員のGameStart関数を呼び出す
        /// </summary>
        public void ReadyToGame()
        {

            //各プレイヤーに番号を振っていく
            int number = 0;
            StringBuilder sb;
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                player.SetPlayerNumber(number);
                sb = new StringBuilder();
                sb.Append("Player").Append(number);
                player.NickName = sb.ToString();
                number++;

            }

            foreach (Player player in PhotonNetwork.PlayerList)
            {
                Debug.Log("番号:" + player.GetPlayerNumber() + ", 名前:" + player.NickName);
            }

            //これ以降の部屋入室を禁止する
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;

            photonView.RPC("GameStart", RpcTarget.AllViaServer);

        }

        /// <summary>
        /// @brief ゲームを開始するためシーンを切り替える
        /// </summary>
        [PunRPC]
        private void GameStart()
        {

            PhotonNetwork.IsMessageQueueRunning = false;

            PlayCorseData.CourseData = courseData;

            SceneSwitch(SceneNameString.InGame);

        }

        #region PhotonCallback

        /// <summary>
        /// @brief 他プレイヤーがルームに入室した時、部屋の情報を更新する
        /// </summary>
        /// <param name="newPlayer"></param>
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);

            UpdateMatchingPlayer();

        }

        /// <summary>
        /// @brief 誰かがルームを退室した時、部屋の情報を更新する
        /// </summary>
        /// <param name="otherPlayer"></param>
        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            base.OnPlayerLeftRoom(otherPlayer);

            UpdateMatchingPlayer();

        }

        /// <summary>
        /// @brief ルームを抜けられた場合、ロビーへ戻る
        /// </summary>
        public override void OnLeftRoom()
        {
            base.OnLeftRoom();

            //シーンを移動させる
            SceneSwitch(SceneNameString.Lobby);
        }

        #endregion

    }

}